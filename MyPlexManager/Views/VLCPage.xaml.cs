using LibVLCSharp.Shared;
using Microsoft.UI.Xaml.Controls;
using MyPlexManager.Extensions;
using MyPlexManager.Helpers;
using System;
using System.ComponentModel;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class VLCPage : Page
{
	private LibVLC? LibVLC { get; set; }
	private MediaPlayer? _mediaPlayer;

	private string mediaPath { get; set; }
	private IntPtr hWnd = IntPtr.Zero;

	public event PropertyChangedEventHandler? PropertyChanged;

	public VLCPage(IntPtr exHwnd,  string stringPath)
	{
		this.InitializeComponent();
		mediaPath = stringPath;
		hWnd = exHwnd;

		var mediaPlayerWindow = WindowHelper.ActiveWindows.LastOrDefault();
		if (mediaPlayerWindow != null)
		{
			mediaPlayerWindow.ExtendsContentIntoTitleBar = true;
			mediaPlayerWindow.SetTitleBar(MediaPlayerTitleBar);
		}

		this.Loaded += (s, e) => {
			PlayMedia();
		};

		this.Unloaded += (s, e) =>
		{
			Dispose();
			(App.Current as App)?.m_window!.Restore();
		};

	}

	public MediaPlayer MediaPlayer
	{
		get => _mediaPlayer!;
		private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
	}

	private void Set<T>(string propertyName, ref T field, T value)
	{
		if (field == null && value != null || field != null && !field.Equals(value))
		{
			field = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}


	private void PlayMedia()
	{

		//var mw = (Application.Current as App)?.m_window as MainWindow;
		//var mpw = mw?.mediaWindow!;
		//var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(mpw);

		var hwnd = hWnd;

		LibVLC = new LibVLC(enableDebugLogs: true);
		MediaPlayer = new MediaPlayer(LibVLC);
		var media = new Media(LibVLC, new Uri(mediaPath));
		//MediaPlayer.Fullscreen = true;   // doesnt work if within the confines of a window or other container
		MediaPlayer.Hwnd = hwnd;
		MediaPlayer.Play(media);
		media.Dispose();
	}

	public void Dispose()
	{
		var mediaPlayer = MediaPlayer;
		MediaPlayer = null!;
		mediaPlayer?.Dispose();
		LibVLC?.Dispose();
		LibVLC = null!;
	}

}
