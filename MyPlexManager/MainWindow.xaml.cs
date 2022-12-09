using CommunityToolkit.Mvvm.DependencyInjection;
using LibVLCSharp.Shared;
using MediaPlayerEngineWrapper;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		// beta nuget KlearTouch.MediaPlayer.Winui
		// public MediaPlayerElement MediaPlayerElement { get; } = new() { AreTransportControlsEnabled = false };
		// (Grid)MediaPlayerElementContainer!.Children.Add(MediaPlayerElement);
		// media player testing usage
		// if (MediaPlayerElement.MediaPlayer is null) MediaPlayerElement.SetMediaPlayer(new ());
		// MediaPlayerElement.MediaPlayer!.AutoPlay = true;
		// var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(@"D:\Media\Barbarian (2022)\Barbarian (2022).mp4");
		// MediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);

		internal IntPtr hWnd = IntPtr.Zero;
		private Microsoft.UI.Windowing.AppWindow? _apw;
		private Microsoft.UI.Windowing.OverlappedPresenter? _presenter;

		CMediaEngine? mediaEngine;

		private string tempToken = string.Empty;

		private LibVLC? LibVLC { get; set; }
		private MediaPlayer? _mediaPlayer;
		public Window? mediaWindow;
		
		public event PropertyChangedEventHandler? PropertyChanged;

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


		public MainWindow()
		{
			this.InitializeComponent();
			Title = "MyPlex Manager";
			ExtendsContentIntoTitleBar = true;
			SetTitleBar(AppTitleBar);

			//this.Closed += (_, _) => {  };

			hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
			Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
			_apw = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
			_apw.Closing += _apw_Closing;
			_presenter = _apw.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;

			Application.Current.Resources["ButtonBackgroundPressed"] = new SolidColorBrush(Microsoft.UI.Colors.RoyalBlue);
			Application.Current.Resources["ToggleSwitchFillOn"] = new SolidColorBrush(Microsoft.UI.Colors.LightBlue);
			Application.Current.Resources["ToggleSwitchFillOnPointerOver"] = new SolidColorBrush(Microsoft.UI.Colors.LightBlue);
			Application.Current.Resources["ToggleSwitchFillOnPressed"] = new SolidColorBrush(Microsoft.UI.Colors.LightBlue);

			//HRESULT hr = HRESULT.S_OK;
			mediaEngine = new CMediaEngine();
			//hr = mediaEngine.Initialize(hWnd, CMediaEngine.ME_MODE.MODE_FRAME_SERVER, ctrlVideo, Microsoft.UI.Colors.Black);


			////HRESULT hr = HRESULT.S_OK;
			//hr = mediaEngine.LoadURL("http://192.168.1.89:32469/object/bb77d26ee7e84441a95a/file.mp4");


		}

		private void _apw_Closing(Microsoft.UI.Windowing.AppWindow sender, Microsoft.UI.Windowing.AppWindowClosingEventArgs args)
		{
			var popups = VisualTreeHelper.GetOpenPopupsForXamlRoot(this.Content.XamlRoot);
			//foreach (var popup in popups)
			//{
			//	if (popup.Name == "SuggestionsPopup")
			//	{
			//		popup.IsOpen = false;
			//	}
			//}
			if (mediaEngine != null)
				mediaEngine.Dispose();
		}

		internal static void GetWindowHandle(object window, object target)
		{
			var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
			WinRT.Interop.InitializeWithWindow.Initialize(target, hwnd);
		}

		private async void btnPlexAccount_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//PlexUserAccount acct = await GetPlexAccountInfo(txtMFACode.Text);

				//if (acct is not null)
				//{
				//	tempToken = acct.user?.authentication_token!;
				//}

				//ContentDialog dialog = new()
				//{
				//	// XamlRoot must be set in the case of a ContentDialog running in a Desktop app
				//	XamlRoot = this.Content.XamlRoot,
				//	Title = "Success",
				//	CloseButtonText = "Close",
				//	DefaultButton = ContentDialogButton.Close,
				//	Content = acct is null ? "No user found!" : $"User {acct.user?.username!}, member since {acct.user!.confirmedAt.ToShortDateString()}, token: {acct.user.authentication_token}"
				//};
				//await dialog.ShowAsync();


				await GetPlexAccountServerInfo("");


				//var mediaView = new MediaViewModel();

				// C# code to create a new window
				//var newWindow = WindowHelper.CreateWindow();

				//mediaWindow = new Window();
				//var vlcPage = new VLCPage("http://192.168.1.89:32469/object/eecdd543ca651c3deda4/file.mp4");
				//mediaWindow.Content = vlcPage;
				//mediaWindow.Activate();

				//mediaWindow.Closed += (sender, args) => {

				//	var t = (Window)sender;	
				//	Dispose();
				//}; 


				//var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(newWindow);


				//var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
				//LibVLC = new LibVLC(enableDebugLogs: true);
				//MediaPlayer = new MediaPlayer(LibVLC);
				////var media = new Media(LibVLC, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
				////var media = new Media(LibVLC, new Uri("http://192.168.1.89:32469/object/bb77d26ee7e84441a95a/file.mp4"));
				////var media = new Media(LibVLC, new Uri("http://192.168.1.89:32400/video/:/transcode/universal/start.m3u8?maxVideoBitrate=5000&X-Plex-Platform=Chrome&copyts=1&offset=0&path=http://192.168.1.89:32400/library/metadata/8187&mediaIndex=0&videoResolution=1920x1080&X-Plex-Token=YCC4FF_A_XmZDuKWUYc3"));
				//var media = new Media(LibVLC, new Uri("http://192.168.1.89:32400/library/parts/11763/1441599736/file.mkv?X-Plex-Token=YCC4FF_A_XmZDuKWUYc3"));
				////http://192.168.1.89:32400/library/parts/14216/1638037886/file.mp4?X-Plex-Token=YCC4FF_A_XmZDuKWUYc3
				////MediaPlayer.Fullscreen = true;
				//MediaPlayer.Hwnd = hWnd;
				//MediaPlayer.Play(media);
				//media.Dispose();


				//HRESULT hr = HRESULT.S_OK;
				//if (mediaEngine is not null)
				//{
				//	var movieUri = string.Empty;
				//	hr = mediaEngine.Initialize(hWnd, CMediaEngine.ME_MODE.MODE_FRAME_SERVER, MediaPlayerControl, Microsoft.UI.Colors.Black);

				//	// DC super pets
				//	//movieUri = "http://192.168.1.89:32469/object/a2c4d4313f4fbe507504/file.mp4";
				//	// x-files
				//	//movieUri = "http://192.168.1.89:32469/object/bb77d26ee7e84441a95a/file.mp4";
				//	// 8-bit x-mas
				//	//movieUri = "http://192.168.1.89:32400/library/parts/14216/1638037886/file.mp4?X-Plex-Token=YCC4FF_A_XmZDuKWUYc3";
				//	// *batteries not included
				//	//http://192.168.1.89:32400/video/:/transcode/universal/start.m3u8?maxVideoBitrate=5000&X-Plex-Platform=Chrome&copyts=1&offset=0&path=http://192.168.1.89:32400/library/metadata/8187&mediaIndex=0&videoResolution=1920x1080&X-Plex-Token=YCC4FF_A_XmZDuKWUYc3
				//	//var qParams = Uri.EscapeDataString("maxVideoBitrate=5000&X-Plex-Platform=Chrome&copyts=1&offset=0&path=http://192.168.1.89:32400/library/metadata/8187&mediaIndex=0&videoResolution=1920x1080&X-Plex-Token=YCC4FF_A_XmZDuKWUYc3");
				//	//movieUri = $"http://192.168.1.89:32400/video/:/transcode/universal/start?{qParams}";
				//	movieUri = "http://192.168.1.89:32400/library/parts/11763/1441599736/file.mkv?X-Plex-Token=YCC4FF_A_XmZDuKWUYc3";

				//	hr = mediaEngine.LoadURL(movieUri);

				//}

			}
			catch (Exception ex)
			{
				ContentDialog dialog = new()
				{
					// XamlRoot must be set in the case of a ContentDialog running in a Desktop app
					XamlRoot = this.Content.XamlRoot,
					Title = "Error",
					CloseButtonText = "Close",
					DefaultButton = ContentDialogButton.Close,
					Content = ex.Message
				};
				await dialog.ShowAsync();
			}
		}

		public void Dispose()
		{
			var mediaPlayer = MediaPlayer;
			MediaPlayer = null!;
			mediaPlayer?.Dispose();
			LibVLC?.Dispose();
			LibVLC = null!;
		}

		private static async Task<PlexUserAccount> GetPlexAccountInfo(string mfaCode)
		{
			PlexUserAccount? acct = default;
			var client = Ioc.Default.GetService<IPlexApiService>();
			if (client is not null)
			{
				acct = await client.GetPlexAccountInfo(mfaCode);
			}
			return acct!;
		}

		private static async Task GetPlexAccountServerInfo(string authToken)
		{
			//yoXRw88xArb6U8zvX7-V
			var client = Ioc.Default.GetService<IPlexApiService>();
			if (client is not null)
			{

				await Task.Delay(0);

				//// PlexTV Methods
				//var ret1 = await client.GetPlexTVAccountDevices("yoXRw88xArb6U8zvX7-V");
				//var ret2 = await client.GetPlexTVRemoteServers("yoXRw88xArb6U8zvX7-V");
				//var ret3 = await client.GetPlexTVAccountUser("yoXRw88xArb6U8zvX7-V");
				//var ret4 = await client.GetPlexTVUserDevices("yoXRw88xArb6U8zvX7-V");
				//var ret5 = await client.GetPlexTVLibrariesByMachineId("yoXRw88xArb6U8zvX7-V", "c51431fda1f73b30eeff5e817d92d3fcb4cc036b");

				//// Local Plex Media Server Methods
				//var ret6 = await client.GetPlexLibraries("yoXRw88xArb6U8zvX7-V");
				//var ret7 = await client.GetPlexServerInfo("yoXRw88xArb6U8zvX7-V");
				//var ret8 = await client.GetPlexServerIdentity("yoXRw88xArb6U8zvX7-V");
				//var ret9 = await client.GetMyPlexServerAccount("yoXRw88xArb6U8zvX7-V");
				//var ret10 = await client.GetPlexServerAccounts("yoXRw88xArb6U8zvX7-V");
				//var ret11 = await client.GetPlexServerPreferences("yoXRw88xArb6U8zvX7-V");
				//var ret12 = await client.GetPlexServerSystemPreferences("yoXRw88xArb6U8zvX7-V");
				//var ret13 = await client.GetPlexServers("yoXRw88xArb6U8zvX7-V");
				//var ret14 = await client.GetPlexDevices("yoXRw88xArb6U8zvX7-V");
				//var ret15 = await client.GetPlexMediaPosters("yoXRw88xArb6U8zvX7-V", "8187");
				//var ret16 = await client.GetPlexMediaProviders("yoXRw88xArb6U8zvX7-V");
				//var ret17 = await client.GetPlexMediaLibraryItems("yoXRw88xArb6U8zvX7-V", "1");

			}

		}




		#region "old"

		//private static Task<PlexAccount> GetAccountInfoAsync(string user, string pwd)
		//{

		//	try
		//	{
		//		var plexFactory = Ioc.Default.GetService<IPlexFactory>();
		//		Log.Information("plex factory init");

		//		if (plexFactory is not null)
		//		{
		//			Log.Information("before get");
		//			//var account = plexFactory.GetPlexAccount("yo", "bro");
		//			var account = plexFactory.GetPlexAccount("Ateb", "181813B2BAC34F2805C416D5460EE211");
		//			Log.Information($"after get - {account.Username}");

		//			return Task.FromResult(account);
		//		}

		//		return Task.FromException<PlexAccount>(new Exception("No account found"));
		//	}
		//	catch (Exception ex)
		//	{

		//		return Task.FromException<PlexAccount>(new Exception(ex.Message));

		//	}

		//}
		#endregion

	}
}
