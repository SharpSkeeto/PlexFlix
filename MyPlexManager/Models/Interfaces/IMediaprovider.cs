namespace MyPlexManager.Models;

public interface IMediaprovider
{
	Feature[]? Feature { get; set; }
	string? identifier { get; set; }
	string? protocols { get; set; }
	string? title { get; set; }
	string? types { get; set; }
}