namespace MyPlexManager.Models;

public interface IServer
{
	string? address { get; set; }
	string? host { get; set; }
	string? machineIdentifier { get; set; }
	string? name { get; set; }
	int port { get; set; }
	string? version { get; set; }
}