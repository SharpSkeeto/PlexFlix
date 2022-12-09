namespace MyPlexManager.Models;

public interface IMediaPosters
{
	string? identifier { get; set; }
	string? mediaTagPrefix { get; set; }
	int mediaTagVersion { get; set; }
	Metadata[]? Metadata { get; set; }
	int size { get; set; }
}