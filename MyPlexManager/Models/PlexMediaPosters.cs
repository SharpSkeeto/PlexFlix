using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexMediaPosters : IPlexMediaPosters
{
	[JsonProperty("MediaContainer")]
	public MediaPosters? MediaContainer { get; set; }
}

public class MediaPosters : IMediaPosters
{
	public int size { get; set; }
	public string? identifier { get; set; }
	public string? mediaTagPrefix { get; set; }
	public int mediaTagVersion { get; set; }
	public Metadata[]? Metadata { get; set; }
}

public partial class Metadata : IMetadata
{
	public string? key { get; set; }
	public string? ratingKey { get; set; }
	public string? thumb { get; set; }
	public bool selected { get; set; }
	public string? provider { get; set; }
}
