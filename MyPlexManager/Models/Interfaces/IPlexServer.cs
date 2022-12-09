namespace MyPlexManager.Models;

public interface IPlexServer
{
	Server[]? Server { get; set; }
	int size { get; set; }
}