using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexServerIdentity : IPlexServerIdentity
{
	[JsonProperty("MediaContainer")]
	public ServerIdentity? MediaContainer { get; set; }
}

public class ServerIdentity : IServerIdentity
{
	public int size { get; set; }
	public bool claimed { get; set; }
	public string? machineIdentifier { get; set; }
	public string? version { get; set; }
}
