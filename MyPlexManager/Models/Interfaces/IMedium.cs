namespace MyPlexManager.Models;

public interface IMedium
{
	float aspectRatio { get; set; }
	int audioChannels { get; set; }
	string? audioCodec { get; set; }
	string? audioProfile { get; set; }
	int bitrate { get; set; }
	string? container { get; set; }
	int duration { get; set; }
	bool has64bitOffsets { get; set; }
	int height { get; set; }
	int id { get; set; }
	int optimizedForStreaming { get; set; }
	Part[]? Part { get; set; }
	string? videoCodec { get; set; }
	string? videoFrameRate { get; set; }
	string? videoProfile { get; set; }
	string? videoResolution { get; set; }
	int width { get; set; }
}