using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexServerAccounts : IPlexServerAccounts
{
	[JsonProperty("MediaContainer")]
	public ServerAccounts? MediaContainer { get; set; }
}

public class ServerAccounts : IServerAccounts
{
	public int size { get; set; }
	public string? identifier { get; set; }
	public Account[]? Account { get; set; }
}

public class Account : IAccount
{
	public int id { get; set; }
	public string? key { get; set; }
	public string? name { get; set; }
	public string? defaultAudioLanguage { get; set; }
	public bool autoSelectAudio { get; set; }
	public string? defaultSubtitleLanguage { get; set; }
	public int subtitleMode { get; set; }
	public string? thumb { get; set; }
}
