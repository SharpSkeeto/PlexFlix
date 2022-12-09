using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyPlexManager.Helpers;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;
using MyPlexManager.ViewModels;
using MyPlexManager.Views;
using MyPlexManager.Windows;
using Serilog;
using System;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public partial class App : Application
	{
		
		//internal Window? m_window;
		internal WindowShell? m_window;
		internal static readonly string STORAGE_FOLDER = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.myplexmanager";

		private WindowsSystemDispatcherQueueHelper? m_wsqdHelper;
		private Microsoft.UI.Composition.SystemBackdrops.MicaController? m_micaController;
		private Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration? m_configurationSource;
		private AppWindow? appWindow;

		internal static IAppSettings? CurrentAppSettings;

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();

			UnhandledException += App_UnhandledException;

			var appSettingsService = new AppSettingsService();
			CurrentAppSettings = appSettingsService.GetApplicationSettings();

			// Setup Dependency Injection
			var navigationService = new NavigationService();
			navigationService.Configure(nameof(HomePage), typeof(HomePage));
			navigationService.Configure(nameof(AboutPage), typeof(AboutPage));
			navigationService.Configure(nameof(MediaMoviePage), typeof(MediaMoviePage));
			navigationService.Configure(nameof(MediaMovieDetailPage), typeof(MediaMovieDetailPage));
			navigationService.Configure(nameof(MediaShowPage), typeof(MediaShowPage));
			navigationService.Configure(nameof(MediaShowSeasonPage), typeof(MediaShowSeasonPage));
			navigationService.Configure(nameof(MediaShowSeasonEpisodePage), typeof(MediaShowSeasonEpisodePage));
			navigationService.Configure(nameof(MediaArtistPage), typeof(MediaArtistPage));
			navigationService.Configure(nameof(MediaArtistAlbumPage), typeof(MediaArtistAlbumPage));
			navigationService.Configure(nameof(MediaArtistAlbumSongPage), typeof(MediaArtistAlbumSongPage));
			navigationService.Configure(nameof(SettingsPage), typeof(SettingsPage));

			Ioc.Default.ConfigureServices(new ServiceCollection()
				.AddMemoryCache()
				.AddTransient<MediaMovieViewModel>()
				.AddTransient<MediaMovieDetailViewModel>()
				.AddTransient<MediaShowViewModel>()
				.AddTransient<MediaShowSeasonViewModel>()
				.AddTransient<MediaShowSeasonEpisodeViewModel>()
				.AddTransient<MediaArtistViewModel>()
				.AddTransient<MediaArtistAlbumViewModel>()
				.AddTransient<MediaArtistAlbumSongViewModel>()
				.AddTransient<SettingsViewModel>()
				//.AddSingleton<IAppSettingsFactory>(new AppSettingsFactory { AppSettings = (AppSettings)appSettingsService.GetApplicationSettings() })
				//.AddSingleton<IAppSettings>(p => p.GetService<IAppSettingsFactory>()?.AppSettings!)
				.AddSingleton<INavigationService>(navigationService)
				.AddTransient<IPlexApiService, PlexApiService>()
				.AddLogging(logger => { logger.AddSerilog(); })
				.BuildServiceProvider());

		}

		private async void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
		{
			Log.Fatal($"Fatal exception::app crash {e.Exception}");

			ContentDialog dialog = new()
			{
				// XamlRoot must be set in the case of a ContentDialog running in a Desktop app
				XamlRoot = m_window?.Content.XamlRoot,
				Title = "Error",
				CloseButtonText = "Close",
				DefaultButton = ContentDialogButton.Close,
				Content = e.Message
			};
			await dialog.ShowAsync();
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			m_window = new WindowShell();
			appWindow = WindowHelper.GetAppWindow(m_window);
			
			if (AppWindowTitleBar.IsCustomizationSupported())           //Run only on Windows 11
			{
				m_window.SizeChanged += SizeChanged;                    //Register handler for setting draggable rects
				appWindow.TitleBar.ExtendsContentIntoTitleBar = true;   //Set ExtendsContentIntoTitleBar for the AppWindow not the window
				appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
				appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
			}

			appWindow.Resize(new SizeInt32(1500, 800));

			DisplayArea displayArea = DisplayArea.GetFromWindowId(appWindow.Id, DisplayAreaFallback.Nearest);
			if (displayArea is not null)
			{
				var CenteredPosition = appWindow.Position;
				CenteredPosition.X = ((displayArea.WorkArea.Width - appWindow.Size.Width) / 2);
				CenteredPosition.Y = ((displayArea.WorkArea.Height - appWindow.Size.Height) / 2);
				appWindow.Move(CenteredPosition);
			}

			m_window.Activated += Window_Activated;
			m_window.Closed += Window_Closed;
			
			m_window.Activate();
			ConfigureSerilog();
			TrySetMicaBackdrop();
			WindowHelper.TrackWindow(m_window);

		}

		private void SizeChanged(object sender, WindowSizeChangedEventArgs args)
		{
			//Update the title bar draggable region. We need to indent from the left both for the nav back button and to avoid the system menu
			RectInt32[] rects = new RectInt32[] { new RectInt32(48, 0, (int)args.Size.Width - 48, 48) };
			appWindow?.TitleBar.SetDragRectangles(rects);
		}

		bool TrySetMicaBackdrop()
		{
			if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
			{
				m_wsqdHelper = new WindowsSystemDispatcherQueueHelper();
				m_wsqdHelper.EnsureWindowsSystemDispatcherQueueController();

				// Hooking up the policy object
				m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();

				if (m_window is not null)
				{
					m_window.Activated += Window_Activated;
					m_window.Closed += Window_Closed;
					((FrameworkElement)m_window.Content).ActualThemeChanged += Window_ThemeChanged;
					// Initial configuration state.
					m_configurationSource.IsInputActive = true;
					SetConfigurationSourceTheme();

					m_micaController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

					// Enable the system backdrop.
					// Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
					//m_micaController.AddSystemBackdropTarget(m_window.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
					m_micaController.AddSystemBackdropTarget(Window.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
					m_micaController.SetSystemBackdropConfiguration(m_configurationSource);
					return true; // succeeded
				}
			}
			return false; // Mica is not supported on this system
		}

		private void Window_Activated(object sender, WindowActivatedEventArgs args)
		{
			if (m_configurationSource is not null)
				m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
		}

		private void Window_Closed(object sender, WindowEventArgs args)
		{
			// Make sure any Mica/Acrylic controller is disposed so it doesn't try to
			// use this closed window.
			if (m_micaController != null)
			{
				m_micaController.Dispose();
				m_micaController = null;
			}
			if (m_window is not null)
				m_window.Activated -= Window_Activated;
			m_configurationSource = null;
		}

		private void Window_ThemeChanged(FrameworkElement sender, object args)
		{
			if (m_configurationSource != null)
			{
				SetConfigurationSourceTheme();
			}
		}

		private void SetConfigurationSourceTheme()
		{
			if(m_window is not null)
			{
				switch (((FrameworkElement)m_window.Content).ActualTheme)
				{
					case ElementTheme.Dark: m_configurationSource!.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
					case ElementTheme.Light: m_configurationSource!.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
					case ElementTheme.Default: m_configurationSource!.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
				}
			}
		}

		private static void ConfigureSerilog()
		{
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File(path: $"{STORAGE_FOLDER}/log-.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
		}

	}
}
