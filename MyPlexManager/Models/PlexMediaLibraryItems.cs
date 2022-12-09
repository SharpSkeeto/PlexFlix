using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace MyPlexManager.Models;

public class PlexMediaLibraryItems : IPlexMediaLibraryItems
{
	[JsonProperty("MediaContainer")]
	public MediaLibraryItems? MediaContainer { get; set; }
}

public class MediaLibraryItems : IMediaLibraryItems
{
	public int size { get; set; }
	public int totalSize { get; set; }
	public int offset { get; set; }
	public bool allowSync { get; set; }
	public string? art { get; set; }
	public string? identifier { get; set; }
	public int librarySectionID { get; set; }
	public string? librarySectionTitle { get; set; }
	public string? librarySectionUUID { get; set; }
	public string? mediaTagPrefix { get; set; }
	public int mediaTagVersion { get; set; }
	public string? thumb { get; set; }
	public string? title1 { get; set; }
	public string? title2 { get; set; }
	public string? viewGroup { get; set; }
	public int viewMode { get; set; }
	public Metadata[]? Metadata { get; set; }
	public Hub[]? Hub { get; set; }
}

public partial class Metadata : IMetadata
{
	//public string? guid { get; set; }
	public string? studio { get; set; }
	public string? type { get; set; }
	public string? title { get; set; }
	public string? contentRating { get; set; }
	public string? summary { get; set; }
	public float rating { get; set; }
	public float audienceRating { get; set; }
	public int year { get; set; }
	public string? tagline { get; set; }
	public string? art { get; set; }
	public int duration { get; set; }
	public string? originallyAvailableAt { get; set; }
	public int addedAt { get; set; }
	public int updatedAt { get; set; }
	public string? audienceRatingImage { get; set; }
	public string? chapterSource { get; set; }
	public string? ratingImage { get; set; }
	public Medium[]? Media { get; set; }
	//public Guids[]? Guids { get; set; }
	public Genre[]? Genre { get; set; }
	public Director[]? Director { get; set; }
	public Writer[]? Writer { get; set; }
	public Country[]? Country { get; set; }
	public Role[]? Role { get; set; }
	public Style[]? Style { get; set; }
	public string? titleSort { get; set; }
	public Collection[]? Collection { get; set; }
	public string? originalTitle { get; set; }
	public int viewCount { get; set; }
	public int lastViewedAt { get; set; }
	public int skipCount { get; set; }
	public string? smart { get; set; }
	public string? childCount { get; set; }
	public int leafCount { get; set; }
	public int viewedLeafCount { get; set; }
	public int index { get; set; }
}

public class Hub
{
	public string? key { get; set; }
	public string? title { get; set; }
	public string? type { get; set; }
	public string? hubIdentifier { get; set; }
	public string? context { get; set; }
	public int size { get; set; }
	public bool more { get; set; }
	public string? style { get; set; }
	public string? hubKey { get; set; }
	public Metadata[]? Metadata { get; set; }
}

public class Style : IStyle
{
	public int id { get; set; }
	public string? filter { get; set; }
	public string? tag { get; set; }
}

public class Medium : IMedium
{
	public int id { get; set; }
	public int duration { get; set; }
	public int bitrate { get; set; }
	public int width { get; set; }
	public int height { get; set; }
	public float aspectRatio { get; set; }
	public int audioChannels { get; set; }
	public string? audioCodec { get; set; }
	public string? videoCodec { get; set; }
	public string? videoResolution { get; set; }
	public string? container { get; set; }
	public string? videoFrameRate { get; set; }
	public string? videoProfile { get; set; }
	public Part[]? Part { get; set; }
	public int optimizedForStreaming { get; set; }
	public string? audioProfile { get; set; }
	public bool has64bitOffsets { get; set; }
}

public class Part : IPart
{
	public int id { get; set; }
	public string? key { get; set; }
	public int duration { get; set; }
	public string? file { get; set; }
	public long size { get; set; }
	public string? container { get; set; }
	public string? videoProfile { get; set; }
	public string? audioProfile { get; set; }
	public bool has64bitOffsets { get; set; }
	public bool optimizedForStreaming { get; set; }
}

public class Guids
{
	public string? id { get; set; }
}

public class Genre : IGenre
{
	public string? tag { get; set; }
}

public class Director : IDirector
{
	public string? tag { get; set; }
}

public class Writer : IWriter
{
	public string? tag { get; set; }
}

public class Country : ICountry
{
	public string? tag { get; set; }
}

public class Role : IRole
{
	public string? tag { get; set; }
}

public class Collection : ICollection
{
	public string? tag { get; set; }
}



