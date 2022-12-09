namespace MyPlexManager.Models;

public interface IAccount
{
	bool autoSelectAudio { get; set; }
	string? defaultAudioLanguage { get; set; }
	string? defaultSubtitleLanguage { get; set; }
	int id { get; set; }
	string? key { get; set; }
	string? name { get; set; }
	int subtitleMode { get; set; }
	string? thumb { get; set; }
}