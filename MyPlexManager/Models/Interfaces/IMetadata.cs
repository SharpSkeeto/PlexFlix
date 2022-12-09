namespace MyPlexManager.Models;

public interface IMetadata
{
	string? key { get; set; }
	string? provider { get; set; }
	string? ratingKey { get; set; }
	bool selected { get; set; }
	string? thumb { get; set; }
}