// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MediaArtistAlbumPage : Page
{

	public ViewModels.MediaArtistAlbumViewModel? ArtistAlbumViewModel { get; } = Ioc.Default.GetService<ViewModels.MediaArtistAlbumViewModel>();
	private static readonly INavigationService? navigationService = Ioc.Default.GetService<INavigationService>();

	public MediaArtistAlbumPage()
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
		if (e.Parameter is Metadata)
		{
			await ArtistAlbumViewModel?.GetArtistAlbumDataAsync(e.Parameter)!;
			
		}
		progressRing.Visibility = Visibility.Collapsed;
	}

	private void albums_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is Metadata item)
		{
			if (item.type == PlexMediaType.album.ToString())
			{
				navigationService?.NavigateTo("MediaArtistAlbumSongPage", item);
			}
		}
	}
}
