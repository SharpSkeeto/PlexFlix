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
public sealed partial class MediaShowPage : Page
{

	public ViewModels.MediaShowViewModel? ShowViewModel { get; } = Ioc.Default.GetService<ViewModels.MediaShowViewModel>();
	private static readonly INavigationService? navigationService = Ioc.Default.GetService<INavigationService>();

	public MediaShowPage()
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
		if (e.Parameter is Models.Directory)
		{
			await ShowViewModel?.InitializeMediaDetailDataAsync(e.Parameter)!;
		}
		progressRing.Visibility = Visibility.Collapsed;
	}

	private void MediaShowsMetaDataView_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is Metadata item)
		{
			if (item.type == PlexMediaType.show.ToString())
			{
				navigationService?.NavigateTo("MediaShowSeasonPage", item);
			}
		}
	}

	private void FilterTitle_TextChanged(object sender, TextChangedEventArgs e)
	{
		var textBox = (TextBox)sender;
		if (textBox is not null)
		{
			var text = textBox.Text.Trim();
			ShowViewModel?.OnFilterChanged(text);
		}
	}

}
