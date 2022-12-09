namespace MyPlexManager.Models;

public interface IPlexServerIdentity
{
	ServerIdentity? MediaContainer { get; set; }
}