using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexServerPreferences : IPlexServerPreferences
{
	[JsonProperty("MediaContainer")]
	public ServerPreferences? MediaContainer { get; set; }
}

public class ServerPreferences : IServerPreferences
{
	public int size { get; set; }
	public Setting[]? Setting { get; set; }
}

public class Setting : ISetting
{
	public string? id { get; set; }
	public string? label { get; set; }
	public string? summary { get; set; }
	public string? type { get; set; }
	public object? _default { get; set; }
	public object? value { get; set; }
	public bool hidden { get; set; }
	public bool advanced { get; set; }
	public string? group { get; set; }
	public string? enumValues { get; set; }
}

