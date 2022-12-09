namespace MyPlexManager.Models;

public interface IPart
{
	string? audioProfile { get; set; }
	string? container { get; set; }
	int duration { get; set; }
	string? file { get; set; }
	bool has64bitOffsets { get; set; }
	int id { get; set; }
	string? key { get; set; }
	bool optimizedForStreaming { get; set; }
	long size { get; set; }
	string? videoProfile { get; set; }
}