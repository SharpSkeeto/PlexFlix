using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Caching.Memory;
using MyPlexManager.Extensions;
using MyPlexManager.Helpers;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;
using MyPlexManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlexManager.ViewModels;

[ObservableObject]
public partial class MediaMovieViewModel
{

	private readonly INavigationService _navigationService;
	private readonly IPlexApiService _plexApiClient;
	private readonly IAppSettings? _appSettings;
	private readonly IMemoryCache? _memoryCache;

	[ObservableProperty]
	string? pageTitle;
	[ObservableProperty]
	int librarySize;
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(MyTitleFilter))]
	private string? filterText;

	[RelayCommand]
	void PosterPlayButtonClick(object paramenter)
	{
		var context = (Metadata)paramenter;

		if (context is not null && context.Media is not null)
		{
			if (context.type == PlexMediaType.movie.ToString())
			{
				if (context.Media.Any())
				{
					var first = context.Media.First();
					var firstPart = first?.Part?.FirstOrDefault();
					string? mediaPath = firstPart?.key;
					var newWindow = WindowHelper.CreateWindow();
					IntPtr hwnd = WindowHelper.GetHwnd(newWindow);
					newWindow.Title = $"Moore Flix - Now Playing: {context.title}";
					newWindow.Content = new MediaPlayerPage(hwnd, BuildMediaUri(mediaPath!), newWindow.Title);
					newWindow.Activate();
					(App.Current as App)?.m_window?.Minimize();
				}
			}
		}
	}

	public string? MyTitleFilter => filterText;

	public static ObservableCollection<Metadata>? MediaMetaData { get; } = new();
	public ObservableCollection<Metadata>? FilteredMediaMetaData { get; } = new();


	public MediaMovieViewModel(INavigationService navigationService, IPlexApiService plexApiClient, IMemoryCache memoryCache)
	{
		_navigationService = navigationService;
		_plexApiClient = plexApiClient;
		_memoryCache = memoryCache;
		_appSettings = App.CurrentAppSettings;
	}
	
	public async Task InitializeMediaDetailDataAsync(object selectedItem)
	{
		var item = (Directory)selectedItem;
		pageTitle = item.title!;
		await PopulateLibraryAsync(item.key!);
	}

	public async Task InitializeMediaCollectionAsync(object selectedItem)
	{
		var item = (Metadata)selectedItem;
		pageTitle = item.title!;
		await PopulateCollectionAsync(item.key!);
		var navItem = _navigationService.GetCurrentNavigationViewItem();
		if(navItem != null)
		{
			navItem.IsSelected = false;
		}
	}

	private async Task PopulateCollectionAsync(string key)
	{
		PlexMediaLibraryItems? plexMediaLibraryItems = null;
		FilteredMediaMetaData?.Clear();
		MediaMetaData?.Clear();

		if (_memoryCache?.TryGetValueExt(key, out plexMediaLibraryItems) is false)
		{
			plexMediaLibraryItems = await _plexApiClient.GetPlexMediaCollectionItems(key!);
			_ = _memoryCache?.Set(key, plexMediaLibraryItems);
		}

		if (plexMediaLibraryItems?.MediaContainer is not null)
		{
			LibrarySize = (plexMediaLibraryItems.MediaContainer.totalSize! == 0 ? plexMediaLibraryItems.MediaContainer.size! : plexMediaLibraryItems.MediaContainer.totalSize!);
			var metadata = plexMediaLibraryItems.MediaContainer?.Metadata;
			foreach (var item in metadata!)
			{
				if ((bool)item.thumb?.StartsWith("/")!)
				{
					var isCollectionThumb = item.thumb?.Contains("collections");
					item.thumb = $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}" + item.thumb + ((bool)isCollectionThumb! ? "&" : "?") + $"X-Plex-Token={_appSettings?.Token}";
				}
				MediaMetaData?.Add(item);
				FilteredMediaMetaData?.Add(item);
			}
		}
	}

	private async Task PopulateLibraryAsync(string key)
	{
		PlexMediaLibraryItems? plexMediaLibraryItems = null;
		FilteredMediaMetaData?.Clear();
		MediaMetaData?.Clear();

		if (_memoryCache?.TryGetValueExt(key, out plexMediaLibraryItems) is false)
		{
			plexMediaLibraryItems = await _plexApiClient.GetPlexMediaLibraryItems(key!, PlexMediaType.movie);
			_ = _memoryCache?.Set(key, plexMediaLibraryItems);
		}

		if (plexMediaLibraryItems?.MediaContainer is not null)
		{
			LibrarySize = (plexMediaLibraryItems.MediaContainer.totalSize! == 0 ? plexMediaLibraryItems.MediaContainer.size! : plexMediaLibraryItems.MediaContainer.totalSize!);
			var metadata = plexMediaLibraryItems.MediaContainer?.Metadata;
			foreach (var item in metadata!)
			{
				if(!(bool)item.thumb?.StartsWith("http")!)
				{
					var isCollectionThumb = item.thumb?.Contains("collections");
					item.thumb = $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}" + item.thumb + ((bool)isCollectionThumb! ? "&" : "?") + $"X-Plex-Token={_appSettings?.Token}";
				}
				MediaMetaData?.Add(item);
				FilteredMediaMetaData?.Add(item);
			}
		}

	}

	private string BuildMediaUri(string mediaPath)
	{
		return $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}{mediaPath!}?X-Plex-Token={_appSettings?.Token}";
	}

	//protected void OnFilterChanged(object sender, TextChangedEventArgs args)
	//[RelayCommand]
	public void OnFilterChanged(string? filter)
	{
		// This is a Linq query that selects only items that return True after being passed through
		// the Filter function, and adds all of those selected items to filtered.
		var filtered = MediaMetaData?.Where(item => Filter(item, filter!))!;
		RemoveNonMatching(filtered);
		AddMatching(filtered);
		// resort 
		var reSorted = new ObservableCollection<Metadata>(FilteredMediaMetaData!)
							//.GroupBy(item => item.ratingKey)
							//.Select(item => item.First())
							.OrderBy(item => string.IsNullOrWhiteSpace(item.titleSort) ? item.title : item.titleSort);

		FilteredMediaMetaData?.Clear();
		foreach (var item in reSorted)
		{
			FilteredMediaMetaData?.Add(item);
		}
	}

	private static bool Filter(Metadata item, string filterText)
	{
		//var test = filterText!;
		//return item.title!.Contains(this.MyTitleFilter!, StringComparison.InvariantCultureIgnoreCase)!;
		return item.title!.Contains(filterText!, StringComparison.InvariantCultureIgnoreCase)!;
	}

	private void RemoveNonMatching(IEnumerable<Metadata> filteredData)
	{
		for (int i = FilteredMediaMetaData!.Count - 1; i >= 0; i--)
		{
			var item = FilteredMediaMetaData[i];
			// If not in the filtered list, then remove it from the source.
			if (!filteredData.Contains(item))
			{
				FilteredMediaMetaData.Remove(item);
			}
		}
	}

	private void AddMatching(IEnumerable<Metadata> filteredData)
	{
		foreach (var item in filteredData)
		{
			// If in filtered list but not currently in the source collection, add it
			if (!FilteredMediaMetaData!.Contains(item))
			{
				FilteredMediaMetaData.Add(item);
			}
		}
	}

}