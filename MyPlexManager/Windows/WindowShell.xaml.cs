using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using MyPlexManager.Extensions;
using MyPlexManager.Helpers;
using MyPlexManager.Interfaces;
using MyPlexManager.Views;
using System;
using System.Threading.Tasks;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class WindowShell : Window
{
	readonly INavigationService? navigationService = null!;

	public WindowShell()
	{
		this.InitializeComponent();
		//ExtendsContentIntoTitleBar = true;
		SetTitleBar(AppTitleBar);
		Title = this.GetAppTitleFromSystem();

		navigationService = Ioc.Default.GetService<INavigationService>();

		// TODO: if appsettings is null / or DBfile not present then we need to goto the setting page for initial setup before trying to build the navigation menu

		navigationService?.PopulateNavigationMenu()
			.ContinueWith(t => {
				BackGroundImage.Visibility= Visibility.Visible;
				progressRing.Visibility = Visibility.Collapsed;
				progressText.Visibility= Visibility.Collapsed;
				NavigationViewControl.IsPaneOpen= true;
			}, TaskScheduler.FromCurrentSynchronizationContext());
	}

	public string GetAppTitleFromSystem()
	{
		return "Plex Flix";
	}

	private void MediaPlayer_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		var newWindow = WindowHelper.CreateWindow();
		IntPtr hwnd = WindowHelper.GetHwnd(newWindow);
		newWindow.Content = new MediaPlayerPage(hwnd, "http://192.168.1.89:32400/library/parts/14216/1638037886/file.mp4?X-Plex-Token=YCC4FF_A_XmZDuKWUYc3", "MFM-Test");
		newWindow.Title = "Media Player";
		newWindow.Activate();
		this.Minimize();
	}

	private void VLCPlayer_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		var newWindow = WindowHelper.CreateWindow();
		IntPtr hwnd = WindowHelper.GetHwnd(newWindow);
		newWindow.Content = new VLCPage(hwnd, "http://192.168.1.89:32469/object/230318c7e5ecf681dd6f/file.mp4");
		//newWindow.Content = new VLCPage(hwnd, "http://192.168.1.89:32400/video/:/transcode/universal/start.m3u8?maxVideoBitrate=5000&X-Plex-Platform=Chrome&copyts=1&offset=0&path=http://192.168.1.89:32400/library/metadata/8187&mediaIndex=0&videoResolution=1920x1080&X-Plex-Token=YCC4FF_A_XmZDuKWUYc3");
		//newWindow.Content = new VLCPage(hwnd, "http://192.168.1.89:32400/library/parts/14216/1638037886/file.mp4?X-Plex-Token=YCC4FF_A_XmZDuKWUYc3");
		newWindow.Title = "VLC Media Player";
		newWindow.Activate();
		this.Minimize();
	}

}
