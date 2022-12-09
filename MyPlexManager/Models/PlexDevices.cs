using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexDevices : IPlexDevices
{
	[JsonProperty("MediaContainer")]
	public Devices? MediaContainer { get; set; }
}

public class Devices : IDevices
{
	public int size { get; set; }
	public string? identifier { get; set; }
	public Device[]? Device { get; set; }
}

public class Device : IDevice
{
	public int id { get; set; }
	public string? name { get; set; }
	public string? platform { get; set; }
	public string? clientIdentifier { get; set; }
	public int createdAt { get; set; }
}

