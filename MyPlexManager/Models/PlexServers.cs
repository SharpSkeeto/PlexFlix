namespace MyPlexManager.Models;

public class PlexServers : IPlexServers
{
	public Servers? MediaContainer { get; set; }
}

public class Servers : IServers
{
	public int size { get; set; }
	public Server[]? Server { get; set; }
}

public partial class Server : IServer
{
	public string? name { get; set; }
	public string? host { get; set; }
	public string? address { get; set; }
	public int port { get; set; }
	public string? machineIdentifier { get; set; }
	public string? version { get; set; }
}

