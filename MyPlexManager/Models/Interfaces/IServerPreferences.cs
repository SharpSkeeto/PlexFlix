namespace MyPlexManager.Models;

public interface IServerPreferences
{
	Setting[]? Setting { get; set; }
	int size { get; set; }
}