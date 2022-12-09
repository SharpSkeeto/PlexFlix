// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.Models;
using MyPlexManager.Services;
using System;
using System.Linq;
using Windows.Media.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MediaArtistAlbumSongPage : Page
{

	public ViewModels.MediaArtistAlbumSongViewModel? ArtistAlbumSongViewModel { get; } = Ioc.Default.GetService<ViewModels.MediaArtistAlbumSongViewModel>();
	
	public MediaArtistAlbumSongPage()
	{
		this.InitializeComponent();
		this.Loaded += (s, e) =>
		{
		};

		this.Unloaded += (s, e) =>
		{
			songPlayer.Source = null!;
			songPlayer.AutoPlay= false;
		};
	}

	protected override async void OnNavigatedTo(NavigationEventArgs e)
	{
		base.OnNavigatedTo(e);
		if (e.Parameter is Metadata)
		{
			await ArtistAlbumSongViewModel?.GetArtistAlbumSongDataAsync(e.Parameter)!;

		}
		progressRing.Visibility = Visibility.Collapsed;
	}

	private void MediaArtistAlbumSongDataView_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is Metadata item)
		{
			if (item.type == PlexMediaType.track.ToString())
			{
				if (item.Media!.Any())
				{
					var first = item.Media!.First();
					var firstPart = first?.Part?.FirstOrDefault();
					string? mediaPath = firstPart?.key;

					songPlayer.AutoPlay = true;
					songPlayer.Source = MediaSource.CreateFromUri(new Uri(ArtistAlbumSongViewModel?.BuildMediaUri(mediaPath!)!));
				}
			}
		}
	}

	//private void PlayButton_Click(object sender, RoutedEventArgs e)
	//{
	//	if (e.OriginalSource is Metadata item)
	//	{
	//		if (item.type == PlexMediaType.track.ToString())
	//		{
	//			if (item.Media!.Any())
	//			{
	//				var first = item.Media!.First();
	//				var firstPart = first?.Part?.FirstOrDefault();
	//				string? mediaPath = firstPart?.key;

	//				MediaArtistAlbumSongDataView.SelectedIndex = item.index - 1;
	//				songPlayer.AutoPlay = true;
	//				songPlayer.Source = MediaSource.CreateFromUri(new Uri(ArtistAlbumSongViewModel?.BuildMediaUri(mediaPath!)!));
	//			}
	//		}
	//	}
	//}
}