using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;
using MyPlexManager.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MediaMoviePage : Page
{

	public MediaMovieViewModel? MovieViewModel { get; } = Ioc.Default.GetService<MediaMovieViewModel>();
	private static readonly INavigationService? navService = Ioc.Default.GetService<INavigationService>();
	
	public MediaMoviePage()
	{
		this.InitializeComponent();

		this.Loaded += (s, e) =>
		{
		};

		this.Unloaded += (s, e) =>
		{
		};
	}

	protected override async void OnNavigatedTo(NavigationEventArgs e)
	{
		base.OnNavigatedTo(e);
		if(e.Parameter is Directory)
		{
			await MovieViewModel?.InitializeMediaDetailDataAsync(e.Parameter)!;
		}
		else if(e.Parameter is Metadata)
		{
			await MovieViewModel?.InitializeMediaCollectionAsync(e.Parameter)!;
		}
		progressRing.Visibility = Visibility.Collapsed;
	}

	//1
	private void MediaMetaDataView_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is Metadata item)
		{
			if (item.type == PlexMediaType.collection.ToString())
			{
				navService?.NavigateTo("MediaMoviePage", item);
			}
			if(item.type == PlexMediaType.movie.ToString())
			{
				navService?.NavigateTo("MediaMovieDetailPage", item);
			}
		}
	}

	private void FilterTitle_TextChanged(object sender, TextChangedEventArgs e)
	{
		var textBox = (TextBox)sender;
		if (textBox is not null)
		{
			var text = textBox.Text.Trim();
			MovieViewModel?.OnFilterChanged(text);
		}

	}

}