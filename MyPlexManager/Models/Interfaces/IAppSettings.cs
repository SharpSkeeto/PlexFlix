namespace MyPlexManager.Models;

public interface IAppSettingsFactory
{
	AppSettings? AppSettings { get; }
}

public class AppSettingsFactory : IAppSettingsFactory
{
	public AppSettings? AppSettings { get; set; }
}


public interface IAppSettings
{
	string? Token { get; set; }
	string? Email { get; set; }
	string? ServerUri { get; set; }
	string? ThumbNail { get; set; }
	string? UserName { get; set; }
	string? MachineId { get; set; }
	string? Address { get; set; }
	string? Port { get; set; }
	string? Protocol { get; set; }
	string? ServerName { get; set; }
	string? Device { get; set; }
	string? Platform { get; set; }
	string? PlatformVersion { get; set; }
	string? ProductVersion { get; set; }
	string? ProductName { get; set; }
	string? DeviceIdentifier { get; set; }
	string? DeviceName { get; set; }
	string? DeviceVersion { get; set; }
	string? ProductDevice { get; set; }
	string? ProductPlatform { get; set; }
}