using Newtonsoft.Json;

namespace MyPlexManager.Models;

public class PlexMediaProviders : IPlexMediaProviders
{
	[JsonProperty("MediaContainer")]
	public MediaProviders? MediaContainer { get; set; }
}

public class MediaProviders : IMediaProviders
{
	public int size { get; set; }
	public bool allowCameraUpload { get; set; }
	public bool allowChannelAccess { get; set; }
	public bool allowMediaDeletion { get; set; }
	public bool allowSharing { get; set; }
	public bool allowSync { get; set; }
	public bool allowTuners { get; set; }
	public bool backgroundProcessing { get; set; }
	public bool certificate { get; set; }
	public bool companionProxy { get; set; }
	public string? countryCode { get; set; }
	public string? diagnostics { get; set; }
	public bool eventStream { get; set; }
	public string? friendlyName { get; set; }
	public int livetv { get; set; }
	public string? machineIdentifier { get; set; }
	public int musicAnalysis { get; set; }
	public bool myPlex { get; set; }
	public string? myPlexMappingState { get; set; }
	public string? myPlexSigninState { get; set; }
	public bool myPlexSubscription { get; set; }
	public string? myPlexUsername { get; set; }
	public int offlineTranscode { get; set; }
	public string? ownerFeatures { get; set; }
	public bool photoAutoTag { get; set; }
	public string? platform { get; set; }
	public string? platformVersion { get; set; }
	public bool pluginHost { get; set; }
	public bool pushNotifications { get; set; }
	public bool readOnlyLibraries { get; set; }
	public int streamingBrainABRVersion { get; set; }
	public int streamingBrainVersion { get; set; }
	public bool sync { get; set; }
	public int transcoderActiveVideoSessions { get; set; }
	public bool transcoderAudio { get; set; }
	public bool transcoderLyrics { get; set; }
	public bool transcoderSubtitles { get; set; }
	public bool transcoderVideo { get; set; }
	public string? transcoderVideoBitrates { get; set; }
	public string? transcoderVideoQualities { get; set; }
	public string? transcoderVideoResolutions { get; set; }
	public int updatedAt { get; set; }
	public bool updater { get; set; }
	public string? version { get; set; }
	public bool voiceSearch { get; set; }
	public Mediaprovider[]? MediaProvider { get; set; }
}

public class Mediaprovider : IMediaprovider
{
	public string? identifier { get; set; }
	public string? title { get; set; }
	public string? types { get; set; }
	public string? protocols { get; set; }
	public Feature[]? Feature { get; set; }
}

public class Feature : IFeature
{
	public string? key { get; set; }
	public string? type { get; set; }
	public Directory[]? Directory { get; set; }
	public Action[]? Action { get; set; }
	public string? flavor { get; set; }
	public string? scrobbleKey { get; set; }
	public string? unscrobbleKey { get; set; }
}

public partial class Directory : IDirectory
{
	public string? hubKey { get; set; }
	//public string title { get; set; }
	//public string agent { get; set; }
	//public string language { get; set; }
	//public bool refreshing { get; set; }
	//public string scanner { get; set; }
	//public string uuid { get; set; }
	//public string id { get; set; }
	//public string key { get; set; }
	//public string type { get; set; }
	//public int updatedAt { get; set; }
	//public int scannedAt { get; set; }
	public Pivot[]? Pivot { get; set; }
}

public class Pivot : IPivot
{
	public string? id { get; set; }
	public string? key { get; set; }
	public string? type { get; set; }
	public string? title { get; set; }
	public string? context { get; set; }
	public string? symbol { get; set; }
}

public class Action : IAction
{
	public string? id { get; set; }
	public string? key { get; set; }
}
