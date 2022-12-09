namespace MyPlexManager.Models;

public class AppSettings : IAppSettings
{
	public string? Email { get; set; }
	public string? UserName { get; set; }
	public string? ThumbNail { get; set; }
	public string? Token { get; set; }
	public string? ServerUri { get; set; }
	public string? MachineId { get; set; }
	public string? Address { get; set; }
	public string? Port { get; set; }
	public string? Protocol { get; set; }
	public string? ServerName { get; set; }
	public string? Device { get; set; }
	public string? Platform { get; set; }
	public string? PlatformVersion { get; set;}
	public string? ProductVersion { get; set; }
	public string? ProductName { get; set; }
	public string? DeviceIdentifier { get; set; }
	public string? DeviceName { get; set;}
	public string? DeviceVersion { get; set;}
	public string? ProductDevice { get; set;}
	public string? ProductPlatform { get; set; }
}
