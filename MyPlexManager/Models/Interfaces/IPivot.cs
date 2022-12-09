namespace MyPlexManager.Models;

public interface IPivot
{
	string? context { get; set; }
	string? id { get; set; }
	string? key { get; set; }
	string? symbol { get; set; }
	string? title { get; set; }
	string? type { get; set; }
}