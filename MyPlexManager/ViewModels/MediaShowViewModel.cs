using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Caching.Memory;
using MyPlexManager.Extensions;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlexManager.ViewModels;

public partial class MediaShowViewModel : ObservableObject
{
	
	private readonly IPlexApiService _plexApiClient;
	private readonly IAppSettings? _appSettings;
	private readonly IMemoryCache? _memoryCache;

	[ObservableProperty]
	string? pageTitle;
	[ObservableProperty]
	int librarySize;

	public static ObservableCollection<Metadata>? MediaMetaData { get; } = new();
	public ObservableCollection<Metadata>? FilteredMediaMetaData { get; } = new();

	public MediaShowViewModel(IPlexApiService plexApiClient, IMemoryCache memoryCache)
	{
		_plexApiClient = plexApiClient;
		_appSettings = App.CurrentAppSettings;
		_memoryCache = memoryCache;
	}

	public async Task InitializeMediaDetailDataAsync(object selectedItem)
	{
		var item = (Directory)selectedItem;
		PageTitle = item.title!;
		await PopulateLibraryAsync(item.key!);
	}

	private async Task PopulateLibraryAsync(string key)
	{
		PlexMediaLibraryItems? plexMediaLibraryItems = null;
		FilteredMediaMetaData?.Clear();
		MediaMetaData?.Clear();

		if (_memoryCache?.TryGetValueExt(key, out plexMediaLibraryItems) is false)
		{
			plexMediaLibraryItems = await _plexApiClient.GetPlexMediaLibraryItems(key!, PlexMediaType.show);
			_ = _memoryCache?.Set(key, plexMediaLibraryItems);
		}

		if (plexMediaLibraryItems?.MediaContainer is not null)
		{
			LibrarySize = (plexMediaLibraryItems.MediaContainer.totalSize! == 0 ? plexMediaLibraryItems.MediaContainer.size! : plexMediaLibraryItems.MediaContainer.totalSize!);
			if (LibrarySize > 0)
			{
				var metadata = plexMediaLibraryItems.MediaContainer?.Metadata;
				foreach (var item in metadata!)
				{
					if ((bool)item.thumb?.StartsWith("/")!)
					{
						item.thumb = $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}" + item.thumb + $"?X-Plex-Token={_appSettings?.Token}";
					}
					MediaMetaData?.Add(item);
					FilteredMediaMetaData?.Add(item);
				}
			}
		}
	}

	public void OnFilterChanged(string? filter)
	{
		// This is a Linq query that selects only items that return True after being passed through
		// the Filter function, and adds all of those selected items to filtered.
		var filtered = MediaMetaData?.Where(item => Filter(item, filter!))!;
		RemoveNonMatching(filtered);
		AddMatching(filtered);
		// resort 
		var reSorted = new ObservableCollection<Metadata>(FilteredMediaMetaData!)
						   .OrderBy(item => string.IsNullOrWhiteSpace(item.titleSort) ? item.title : item.titleSort);

		FilteredMediaMetaData?.Clear();
		foreach (var item in reSorted)
		{
			FilteredMediaMetaData?.Add(item);
		}
	}

	private static bool Filter(Metadata item, string filterText)
	{
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
