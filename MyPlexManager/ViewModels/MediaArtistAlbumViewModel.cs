using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Caching.Memory;
using MyPlexManager.Extensions;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlexManager.ViewModels;

public partial class MediaArtistAlbumViewModel : ObservableObject
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
	
	public ObservableCollection<Metadata>? MediaMetaData { get; } = new();

	public MediaArtistAlbumViewModel(INavigationService navigationService, IPlexApiService plexApiClient, IMemoryCache memoryCache)
	{
		_navigationService = navigationService;
		_plexApiClient = plexApiClient;
		_appSettings = App.CurrentAppSettings;
		_memoryCache = memoryCache;
	}

	public async Task GetArtistAlbumDataAsync(object selectedItem)
	{
		var item = (Metadata)selectedItem;
		MediaThumbNailUri = item.thumb;
		MediaTitle = item.title;
		MediaYear = item.year;
		MediaSummary = item.summary?.Replace(System.Environment.NewLine, System.Environment.NewLine+System.Environment.NewLine)!;
		if (item.Country is not null)
			MediaCountry = string.Join(", ", item.Country?.Select(t => t.tag)!);

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
			if(metadata != null)
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

		// get artist related ablum info: live, soundtracks, compilations, etc.
		if (_memoryCache?.TryGetValueExt(item.key! + "related", out plexMediaLibraryItems) is false)
		{
			plexMediaLibraryItems = await _plexApiClient.GetPlexMediaMetaRelatedData(item.ratingKey!);
			_ = _memoryCache?.Set(item.key! + "related", plexMediaLibraryItems);
		}

		if (plexMediaLibraryItems?.MediaContainer is not null)
		{
			var hubs = plexMediaLibraryItems.MediaContainer?.Hub;
			if(hubs is not null)
			{
				var hubList = hubs?.Where(h => h.hubIdentifier != "artist.similar")
								   .Where(m => m.Metadata is not null)
								   .ToList();
				foreach (var hub in hubList!)
				{
					var metadata = hub.Metadata!;
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
			}
		}

		var navItem = _navigationService.GetCurrentNavigationViewItem();
		if (navItem != null)
		{
			navItem.IsSelected = false;
		}

	}

}
