namespace MyPlexManager.Models;

public interface IFeature
{
	Action[]? Action { get; set; }
	Directory[]? Directory { get; set; }
	string? flavor { get; set; }
	string? key { get; set; }
	string? scrobbleKey { get; set; }
	string? type { get; set; }
	string? unscrobbleKey { get; set; }
}