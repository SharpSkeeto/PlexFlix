using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Caching.Memory;
using MyPlexManager.Extensions;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Services;

namespace MyPlexManager.ViewModels;


[ObservableObject]
public partial class SettingsViewModel
{
	private readonly IAppSettings? _appSettings;
	private readonly IMemoryCache? _memoryCache;

	[ObservableProperty]
	string? pageTitle;
	[ObservableProperty]
	string? userId;
	[ObservableProperty]
	string? userToken;
	[ObservableProperty]
	string? userEmail;
	[ObservableProperty]
	string? userThumbNail;
	[ObservableProperty]
	string? serverName;
	[ObservableProperty]
	string? serverProtocol;
	[ObservableProperty]
	string? serverAddress;
	[ObservableProperty]
	string? serverPort;
	[ObservableProperty]
	string? serverUri;
	[ObservableProperty]
	string? machineId;
	[ObservableProperty]
	string? device;
	[ObservableProperty]
	string? platform;
	[ObservableProperty]
	string? platformVersion;
	[ObservableProperty]
	string? productVersion;
	[ObservableProperty]
	string? productName;

	[RelayCommand]
	void RefreshCacheButtonClick(object paramenter)
	{
		_memoryCache?.Clear();
	}

	[RelayCommand]
	void UpdateSettingsButtonClick(object paramenter)
	{
		IAppSettings appSettings = UpdateSettings();
		App.CurrentAppSettings = (IAppSettings?)appSettings;
	}

	public SettingsViewModel(IMemoryCache memoryCache)
	{
		_memoryCache = memoryCache;
		_appSettings = App.CurrentAppSettings;
	}

	public void InitializeSettingsData()
	{
		pageTitle = "Settings";
		userId = _appSettings?.UserName;
		userToken = _appSettings?.Token;
		userEmail = _appSettings?.Email;
		userThumbNail = _appSettings?.ThumbNail;
		serverName = _appSettings?.ServerName;
		serverProtocol = _appSettings?.Protocol;
		serverAddress = _appSettings?.Address;
		serverPort = _appSettings?.Port;
		serverUri = _appSettings?.ServerUri;
		machineId = _appSettings?.MachineId;
		device = _appSettings?.Device;
		platform = _appSettings?.Platform;
		platformVersion = _appSettings?.PlatformVersion;
		productVersion = _appSettings?.ProductVersion;
		productName = _appSettings?.ProductName;
	}

	private IAppSettings UpdateSettings()
	{

		var appSettingsService = new AppSettingsService();

		var appSettings = new AppSettings()
		{
			Address = serverAddress,
			Port = serverPort,
			Email = userEmail,
			ThumbNail = userThumbNail,
			MachineId = machineId,
			Protocol = serverProtocol,
			ServerUri = $"{serverProtocol}://{serverAddress}:{serverPort}",
			Token = userToken,
			UserName = userId
		};

		return appSettingsService.UpdateApplicationSettings(appSettings);

	}
}
