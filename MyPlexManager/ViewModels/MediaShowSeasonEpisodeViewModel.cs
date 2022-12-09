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
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlexManager.ViewModels;

[ObservableObject]
public partial class MediaShowSeasonEpisodeViewModel
{

	private readonly INavigationService _navigationService;
	private readonly IPlexApiService _plexApiClient;
	private readonly IAppSettings? _appSettings;
	private readonly IMemoryCache? _memoryCache;

	[ObservableProperty]
	string? showTitle;
	[ObservableProperty]
	string? showSubtitle;
	[ObservableProperty]
	int episodeCount;
	[ObservableProperty]
	int episodeNumber;
	[ObservableProperty]
	int episodeDuration;
	[ObservableProperty]
	int episodeAirDate;

	public ObservableCollection<Metadata>? MediaMetaData { get; } = new();

	[RelayCommand]
	void PosterPlayButtonClick(object paramenter)
	{
		var context = (Metadata)paramenter;

		if (context is not null && context.Media is not null)
		{
			if (context.type == PlexMediaType.episode.ToString())
			{
				if (context.Media.Any())
				{
					var first = context.Media.First();
					var firstPart = first?.Part?.FirstOrDefault();
					string? mediaPath = firstPart?.key;
					var newWindow = WindowHelper.CreateWindow();
					IntPtr hwnd = WindowHelper.GetHwnd(newWindow);
					newWindow.Title = $"Moore Flix - Now Playing: E{context.index} - {context.title}";
					newWindow.Content = new MediaPlayerPage(hwnd, BuildMediaUri(mediaPath!), newWindow.Title);
					newWindow.Activate();
					(App.Current as App)?.m_window?.Minimize();
				}
			}
		}
	}

	public MediaShowSeasonEpisodeViewModel(INavigationService navigationService, IPlexApiService plexApiClient, IMemoryCache memoryCache)
	{
		_navigationService = navigationService;
		_plexApiClient = plexApiClient;
		_appSettings = App.CurrentAppSettings;
		_memoryCache = memoryCache;
	}

	public async Task GetShowSeasonEpisodeDataAsync(object selectedItem)
	{
		var item = (Metadata)selectedItem;
		
		PlexMediaLibraryItems? plexMediaLibraryItems = null;
		MediaMetaData?.Clear();

		if (_memoryCache?.TryGetValueExt(item.key!, out plexMediaLibraryItems) is false)
		{
			plexMediaLibraryItems = await _plexApiClient.GetPlexChildrenData(item.key!);
			_ = _memoryCache?.Set(item.key!, plexMediaLibraryItems);
		}

		if (plexMediaLibraryItems?.MediaContainer is not null)
		{
			ShowTitle = plexMediaLibraryItems.MediaContainer?.title1;
			ShowSubtitle = plexMediaLibraryItems.MediaContainer?.title2;
			EpisodeCount = (int)plexMediaLibraryItems.MediaContainer?.size!;
			var metadata = plexMediaLibraryItems.MediaContainer?.Metadata;
			foreach (var data in metadata!)
			{
				if ((bool)data.thumb?.StartsWith("/")!)
				{
					data.thumb = $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}" + data.thumb + $"?X-Plex-Token={_appSettings?.Token}";
				}
				MediaMetaData?.Add(data);
			}
		}

		var navItem = _navigationService.GetCurrentNavigationViewItem();
		if (navItem != null)
		{
			navItem.IsSelected = false;
		}

	}

	private string BuildMediaUri(string mediaPath)
	{
		return $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}{mediaPath!}?X-Plex-Token={_appSettings?.Token}";
	}


}
