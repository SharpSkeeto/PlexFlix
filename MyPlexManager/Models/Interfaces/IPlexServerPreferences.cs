namespace MyPlexManager.Models;

public interface IPlexServerPreferences
{
	ServerPreferences? MediaContainer { get; set; }
}