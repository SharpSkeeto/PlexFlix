using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Caching.Memory;
using MyPlexManager.Extensions;
using MyPlexManager.Models;
using MyPlexManager.Services;

namespace MyPlexManager.ViewModels;

public partial class SettingsViewModel : ObservableObject
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
		PageTitle = "Settings";
		UserId = _appSettings?.UserName;
		UserToken = _appSettings?.Token;
		UserEmail = _appSettings?.Email;
		UserThumbNail = _appSettings?.ThumbNail;
		ServerName = _appSettings?.ServerName;
		ServerProtocol = _appSettings?.Protocol;
		ServerAddress = _appSettings?.Address;
		ServerPort = _appSettings?.Port;
		ServerUri = _appSettings?.ServerUri;
		MachineId = _appSettings?.MachineId;
		Device = _appSettings?.Device;
		Platform = _appSettings?.Platform;
		PlatformVersion = _appSettings?.PlatformVersion;
		ProductVersion = _appSettings?.ProductVersion;
		ProductName = _appSettings?.ProductName;
	}

	private IAppSettings UpdateSettings()
	{

		var appSettingsService = new AppSettingsService();

		var appSettings = new AppSettings()
		{
			Address = ServerAddress,
			Port = ServerPort,
			Email = UserEmail,
			ThumbNail = UserThumbNail,
			MachineId = MachineId,
			Protocol = ServerProtocol,
			ServerUri = $"{ServerProtocol}://{ServerAddress}:{ServerPort}",
			Token = UserToken,
			UserName = UserId
		};

		return appSettingsService.UpdateApplicationSettings(appSettings);

	}
}
