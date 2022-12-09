namespace MyPlexManager.Models;

public interface IDevice
{
	string? clientIdentifier { get; set; }
	int createdAt { get; set; }
	int id { get; set; }
	string? name { get; set; }
	string? platform { get; set; }
}