namespace MyPlexManager.Models;

public interface IMediaLibraryItems
{
	bool allowSync { get; set; }
	string? art { get; set; }
	string? identifier { get; set; }
	int librarySectionID { get; set; }
	string? librarySectionTitle { get; set; }
	string? librarySectionUUID { get; set; }
	string? mediaTagPrefix { get; set; }
	int mediaTagVersion { get; set; }
	Metadata[]? Metadata { get; set; }
	int offset { get; set; }
	int size { get; set; }
	string? thumb { get; set; }
	string? title1 { get; set; }
	string? title2 { get; set; }
	int totalSize { get; set; }
	string? viewGroup { get; set; }
	int viewMode { get; set; }
}