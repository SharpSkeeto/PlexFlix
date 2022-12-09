using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexServerLibraries : IPlexServerLibraries
{
	[JsonProperty("MediaContainer")]
	public PlexLibraries? Mediacontainer { get; set; }
}

public class PlexLibraries : IPlexLibraries
{
	public int size { get; set; }
	public bool allowSync { get; set; }
	public string? title1 { get; set; }
	public Directory[]? Directory { get; set; }
}

public partial class Directory : IDirectory
{
	public bool allowSync { get; set; }
	public string? art { get; set; }
	public string? composite { get; set; }
	public int count { get; set; }
	public bool filters { get; set; }
	public bool refreshing { get; set; }
	public string? thumb { get; set; }
	public string? key { get; set; }
	public string? type { get; set; }
	public string? title { get; set; }
	public string? agent { get; set; }
	public string? scanner { get; set; }
	public string? language { get; set; }
	public string? uuid { get; set; }
	public int updatedAt { get; set; }
	public int createdAt { get; set; }
	public int scannedAt { get; set; }
	public bool content { get; set; }
	public bool directory { get; set; }
	public int contentChangedAt { get; set; }
	public int hidden { get; set; }
	public Location[]? Location { get; set; }
}

public class Location : ILocation
{
	public int id { get; set; }
	public string? path { get; set; }
}
