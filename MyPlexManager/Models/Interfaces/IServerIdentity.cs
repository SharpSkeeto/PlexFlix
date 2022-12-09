namespace MyPlexManager.Models;

public interface IServerIdentity
{
	bool claimed { get; set; }
	string? machineIdentifier { get; set; }
	int size { get; set; }
	string? version { get; set; }
}