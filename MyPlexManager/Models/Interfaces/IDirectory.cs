namespace MyPlexManager.Models;

public interface IDirectory
{
	string? agent { get; set; }
	bool allowSync { get; set; }
	string? art { get; set; }
	string? composite { get; set; }
	int count { get; set; }
	bool content { get; set; }
	int contentChangedAt { get; set; }
	int createdAt { get; set; }
	bool directory { get; set; }
	bool filters { get; set; }
	int hidden { get; set; }
	string? key { get; set; }
	string? language { get; set; }
	Location[]? Location { get; set; }
	bool refreshing { get; set; }
	int scannedAt { get; set; }
	string? scanner { get; set; }
	string? thumb { get; set; }
	string? title { get; set; }
	string? type { get; set; }
	int updatedAt { get; set; }
	string? uuid { get; set; }
}