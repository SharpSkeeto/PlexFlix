using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.UI.Xaml.Controls;
using MyPlexManager.Extensions;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;
using MyPlexManager.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace MyPlexManager.ViewModels;

public partial class MediaArtistAlbumSongViewModel : ObservableObject
{

	private readonly INavigationService _navigationService;
	private readonly IPlexApiService _plexApiClient;
	private readonly IAppSettings? _appSettings;
	private readonly IMemoryCache? _memoryCache;

	[ObservableProperty]
	private string? mediaThumbNailUri;
	[ObservableProperty]
	private string? mediaTitle;
	[ObservableProperty]
	private int mediaYear;
	[ObservableProperty]
	private string? mediaSummary;
	[ObservableProperty]
	private string? mediaCountry;
	[ObservableProperty]
	private string? mediaGenre;
	[ObservableProperty]
	private string? mediaStudio;

	public ObservableCollection<Metadata>? MediaMetaData { get; } = new();

	public MediaArtistAlbumSongViewModel(INavigationService navigationService, IPlexApiService plexApiClient, IMemoryCache memoryCache)
	{
		_navigationService = navigationService;
		_plexApiClient = plexApiClient;
		_appSettings = App.CurrentAppSettings;
		_memoryCache = memoryCache;
	}

	public async Task GetArtistAlbumSongDataAsync(object selectedItem)
	{
		var item = (Metadata)selectedItem;
		MediaThumbNailUri = item.thumb;
		MediaTitle = item.title;
		MediaYear = item.year;
		MediaSummary = item.summary?.Replace(System.Environment.NewLine, System.Environment.NewLine + System.Environment.NewLine)!;
		MediaStudio = item.studio;

		if (item.Genre is not null)
			MediaGenre = string.Join(", ", item.Genre?.Select(t => t.tag)!);

		PlexMediaLibraryItems? plexMediaLibraryItems = null;
		MediaMetaData?.Clear();

		// get artist albums
		if (_memoryCache?.TryGetValueExt(item.key!, out plexMediaLibraryItems) is false)
		{
			plexMediaLibraryItems = await _plexApiClient.GetPlexChildrenData(item.key!);
			_ = _memoryCache?.Set(item.key!, plexMediaLibraryItems);
		}

		if (plexMediaLibraryItems?.MediaContainer is not null)
		{
			var metadata = plexMediaLibraryItems.MediaContainer?.Metadata;
			if (metadata != null)
			{
				foreach (var data in metadata!)
				{
					if (data.thumb is not null)
					{
						if ((bool)data.thumb?.StartsWith("/")!)
						{
							data.thumb = $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}" + data.thumb + $"?X-Plex-Token={_appSettings?.Token}";
						}
					}
					MediaMetaData?.Add(data!);
				}
			}
		}

		var navItem = _navigationService.GetCurrentNavigationViewItem();
		if (navItem != null)
		{
			navItem.IsSelected = false;
		}
	}

	[RelayCommand]
	void PlayButton(object paramenter)
	{
		var context = (Metadata)paramenter;

		if (context is not null && context.Media is not null)
		{
			if (context.type == PlexMediaType.track.ToString())
			{
				if (context.Media.Any())
				{
					var first = context.Media.First();
					var firstPart = first?.Part?.FirstOrDefault();
					string? mediaPath = firstPart?.key;
					var win = (App.Current as App)?.m_window!;
					var nv = _navigationService.GetNavigationInstance();
					
					if (nv != null)
					{
						var page = (MediaArtistAlbumSongPage)(nv.Content! as Frame)!.Content;
						var grid = (Grid)page.Content;
						
						if (grid != null)
						{
							var lv = grid.Children.Where(w => w.GetType() == typeof(ListView)).First();
							if (lv != null)
							{
								(lv as ListView)!.SelectedIndex = context.index-1;
							}

							var mpe = grid.Children.Where(w => w.GetType() == typeof(MediaPlayerElement)).First();
							if(mpe != null)
							{
								(mpe as MediaPlayerElement)!.AutoPlay = true;
								(mpe as MediaPlayerElement)!.Source = MediaSource.CreateFromUri(new Uri(BuildMediaUri(mediaPath!)));
							}
						}
					}
				}
			}
		}
	}

	internal string BuildMediaUri(string mediaPath)
	{
		return $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}{mediaPath!}?X-Plex-Token={_appSettings?.Token}";
	}


}
