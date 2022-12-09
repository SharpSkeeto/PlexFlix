namespace MyPlexManager.Models;

public interface IDevices
{
	Device[]? Device { get; set; }
	string? identifier { get; set; }
	int size { get; set; }
}