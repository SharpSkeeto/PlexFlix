using CommunityToolkit.Mvvm.ComponentModel;
using MediaPlayerEngineWrapper;
using MFMMediaPlayer.GlobalStructures;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using MyPlexManager.Extensions;
using MyPlexManager.Helpers;
using Serilog;
using System;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

[ObservableObject]
public sealed partial class MediaPlayerPage : Page
{

	private readonly IntPtr hWnd = IntPtr.Zero;
	private readonly AppWindow? Apw;
	private readonly CMediaEngine? mediaEngine;
	private string MediaPath { get; set; }
	[ObservableProperty]
	private string windowTitle = string.Empty;
	
	public MediaPlayerPage(IntPtr exHwnd, string mediaPath, string mediaTitle)
	{
		this.InitializeComponent();

		MediaPath = mediaPath;
		windowTitle = mediaTitle;

		if (exHwnd == IntPtr.Zero)
		{
			Window? parentWinHandle = (App.Current as App)?.m_window!;
			hWnd = WinRT.Interop.WindowNative.GetWindowHandle(parentWinHandle);
		}
		else
		{
			hWnd = exHwnd;
		}
		
		WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
		Apw = AppWindow.GetFromWindowId(myWndId);
		Apw.Closing += Apw_Closing;

		var mediaPlayerWindow = WindowHelper.ActiveWindows.LastOrDefault();
		if (mediaPlayerWindow != null)
		{
			mediaPlayerWindow.ExtendsContentIntoTitleBar = true;
			mediaPlayerWindow.SetTitleBar(MediaPlayerTitleBar);
		}

		DisplayArea displayArea = DisplayArea.GetFromWindowId(myWndId, DisplayAreaFallback.Nearest);
		if (displayArea is not null)
		{
			var CenteredPosition = Apw.Position;
			CenteredPosition.X = ((displayArea.WorkArea.Width - Apw.Size.Width) / 2);
			CenteredPosition.Y = ((displayArea.WorkArea.Height - Apw.Size.Height) / 2);
			Apw.Move(CenteredPosition);
		}

		mediaEngine = new CMediaEngine();

		LoadPlayerView();
	}


	private void LoadPlayerView()
	{
		if (mediaEngine is not null)
		{
			HRESULT hr = mediaEngine.Initialize(hWnd, CMediaEngine.ME_MODE.MODE_FRAME_SERVER, MediaPlayerControl, Colors.Black);

			if (hr == HRESULT.S_OK)
			{
				//var qParams = Uri.EscapeDataString("maxVideoBitrate=5000&X-Plex-Platform=Chrome&copyts=1&offset=0&path=http://192.168.1.89:32400/library/metadata/8187&mediaIndex=0&videoResolution=1920x1080&X-Plex-Token=YCC4FF_A_XmZDuKWUYc3");
				//MediaPath = $"http://192.168.1.89:32400/video/:/transcode/universal/start?{qParams}";
				//MediaPath = "http://192.168.1.89:32400/video/:/transcode/universal/start.m3u8?maxVideoBitrate=5000&X-Plex-Platform=Chrome&copyts=1&offset=0&path=http://192.168.1.89:32400/library/metadata/8187&mediaIndex=0&videoResolution=1920x1080&X-Plex-Token=YCC4FF_A_XmZDuKWUYc3";

				hr = mediaEngine.LoadURL(MediaPath);

				if(hr != HRESULT.S_OK)
				{
					Log.Error($"Error loading media file {MediaPath}");
					throw new Exception($"Error loading media file {MediaPath}");
				}

			}
		}
	}

	private void Apw_Closing(Microsoft.UI.Windowing.AppWindow sender, Microsoft.UI.Windowing.AppWindowClosingEventArgs args)
	{
		//var popups = VisualTreeHelper.GetOpenPopupsForXamlRoot(this.Content.XamlRoot);
		//foreach (var popup in popups)
		//{
		//	if (popup.Name == "SuggestionsPopup")
		//	{
		//		popup.IsOpen = false;
		//	}
		//}
		mediaEngine?.Dispose();
		(App.Current as App)?.m_window!.Restore();
	}

	private void MediaPlayerControl_KeyUp(object sender, KeyRoutedEventArgs e)
	{
		switch (e.Key.ToString().ToLower())
		{
			case "f":
				mediaEngine?.FullScreenToggle();
				break;
			case "space":
				mediaEngine?.PlayToggle();
				break;
			case "right":   // for seeking ahead  (forward)
				mediaEngine?.Seek(30);
				break;
			case "left":    // for seeking backwards (rewind)
				mediaEngine?.Seek(-30);
				break;
			case "pageup":  // volume
			case "add":
			case "up":
				mediaEngine?.VolumeUp(0.05);
				break;
			case "pagedown":// volume	
			case "subtract":
			case "down":
				mediaEngine?.VolumeDown(0.05);
				break;
			case "m":       // mute
			case "s":
				mediaEngine?.MuteSoundToggle();
				break;
			case "h":		// hide transport controls
				mediaEngine?.ShowControls(false); 
				break;
			default:
				break;
		}
	}

}
