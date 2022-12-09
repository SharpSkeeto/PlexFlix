// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MediaShowSeasonEpisodePage : Page
    {
	public ViewModels.MediaShowSeasonEpisodeViewModel? ShowSeasonEpisodeViewModel { get; } = Ioc.Default.GetService<ViewModels.MediaShowSeasonEpisodeViewModel>();
	
	public MediaShowSeasonEpisodePage()
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
			await ShowSeasonEpisodeViewModel?.GetShowSeasonEpisodeDataAsync(e.Parameter)!;
		}
		progressRing.Visibility = Visibility.Collapsed;
	}

}
