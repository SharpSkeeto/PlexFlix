namespace MyPlexManager.Models;

public interface IPlexLibraries
{
	bool allowSync { get; set; }
	Directory[]? Directory { get; set; }
	int size { get; set; }
	string? title1 { get; set; }
}