namespace MyPlexManager.Models;

public interface ISetting
{
	object? _default { get; set; }
	bool advanced { get; set; }
	string? enumValues { get; set; }
	string? group { get; set; }
	bool hidden { get; set; }
	string? id { get; set; }
	string? label { get; set; }
	string? summary { get; set; }
	string? type { get; set; }
	object? value { get; set; }
}