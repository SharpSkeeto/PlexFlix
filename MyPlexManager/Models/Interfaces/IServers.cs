namespace MyPlexManager.Models;

public interface IServers
{
	Server[]? Server { get; set; }
	int size { get; set; }
}