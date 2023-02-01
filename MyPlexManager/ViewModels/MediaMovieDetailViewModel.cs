using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyPlexManager.Extensions;
using MyPlexManager.Helpers;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Views;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlexManager.ViewModels;

public partial class MediaMovieDetailViewModel : ObservableObject
{

	protected readonly INavigationService _navigationService;
	protected readonly IPlexApiService _plexApiClient;
	protected readonly IAppSettings? _appSettings;

	[ObservableProperty]
	private string? mediaThumbNailUri;
	[ObservableProperty]
	private string? mediaTitle;
	[ObservableProperty]
	private int mediaYear;
	[ObservableProperty]
	private string? mediaSummary;
	[ObservableProperty]
	private string? mediaDuration;
	[ObservableProperty]
	private string? mediaTagline;
	[ObservableProperty]
	private string? mediaRoles;
	[ObservableProperty]
	private string? mediaDirectors;
	[ObservableProperty]
	private string? mediaWriters;
	[ObservableProperty]
	private string? mediaStudio;
	[ObservableProperty]
	private string? mediaGenre;
	[ObservableProperty]
	private string? mediaPath;
	[ObservableProperty]
	private string? mediaRating;
	[ObservableProperty]
	private string? mediaReleaseDate;

	[RelayCommand]
	void PosterPlayButtonClick(object paramenter)
	{
		var context = (MediaMovieDetailViewModel)paramenter;
		if (context is not null)
		{
			var mediaPath = context.MediaPath;
			var newWindow = WindowHelper.CreateWindow();
			IntPtr hwnd = WindowHelper.GetHwnd(newWindow);
			newWindow.Title = $"Plex Flix - Now Playing: {context.MediaTitle}";
			newWindow.Content = new MediaPlayerPage(hwnd, BuildMediaUri(mediaPath!), newWindow.Title);
			newWindow.Activate();
			(App.Current as App)?.m_window?.Minimize();
		}
	}

	public MediaMovieDetailViewModel(INavigationService navigationService, IPlexApiService plexApiClient)
	{
		_navigationService = navigationService;
		_plexApiClient = plexApiClient;
		_appSettings = App.CurrentAppSettings;
	}

	public async Task GetMediaDetailDataAsync(object selectedItem)
	{
		var item = (Metadata)selectedItem;
		MediaThumbNailUri = item.thumb;
		MediaTitle = item.title;
		MediaYear = item.year;
		if (DateOnly.TryParse(item.originallyAvailableAt!, out var originalAvail))
		{
			MediaReleaseDate = originalAvail.ToString("MMMM dd, yyyy");
		};
		MediaSummary = item.summary!;
		MediaTagline= item.tagline;
		var duration = TimeSpan.FromMilliseconds(item.duration);
		MediaDuration = $"{duration.Hours} hr {duration.Minutes} min";
		MediaRating = string.IsNullOrWhiteSpace(item.contentRating) ? "N/A" : item.contentRating;

		if (item.Role is not null)
			MediaRoles = string.Join(", ", item.Role?.Select(t => t.tag)!);

		if (item.Director is not null)
			MediaDirectors = string.Join(", ", item.Director?.Select(t => t.tag)!);

		if(item.Writer is not null)
			MediaWriters = string.Join(", ", item.Writer?.Select(t => t.tag)!);

		if (item.Genre is not null)
			MediaGenre = string.Join(", ", item.Genre?.Select(t => t.tag)!);

		MediaStudio = item.studio;

		if (item.Media is not null)
		{
			var first = item.Media?.First();
			var firstPart = first?.Part?.FirstOrDefault();
			MediaPath = firstPart?.key;
		}

		var navItem = _navigationService.GetCurrentNavigationViewItem();
		if (navItem != null)
		{
			navItem.IsSelected = false;
		}

		await Task.Delay(0);

	}

	private string BuildMediaUri(string mediaPath)
	{
		return $"{_appSettings?.Protocol}://{_appSettings?.Address}:{_appSettings?.Port}{mediaPath!}?X-Plex-Token={_appSettings?.Token}";
	}

}
