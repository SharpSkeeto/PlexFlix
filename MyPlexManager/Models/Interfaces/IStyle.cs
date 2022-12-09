namespace MyPlexManager.Models;

public interface IStyle
{
	string? filter { get; set; }
	int id { get; set; }
	string? tag { get; set; }
}