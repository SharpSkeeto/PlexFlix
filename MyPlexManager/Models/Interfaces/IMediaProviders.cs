namespace MyPlexManager.Models;

public interface IMediaProviders
{
	bool allowCameraUpload { get; set; }
	bool allowChannelAccess { get; set; }
	bool allowMediaDeletion { get; set; }
	bool allowSharing { get; set; }
	bool allowSync { get; set; }
	bool allowTuners { get; set; }
	bool backgroundProcessing { get; set; }
	bool certificate { get; set; }
	bool companionProxy { get; set; }
	string? countryCode { get; set; }
	string? diagnostics { get; set; }
	bool eventStream { get; set; }
	string? friendlyName { get; set; }
	int livetv { get; set; }
	string? machineIdentifier { get; set; }
	Mediaprovider[]? MediaProvider { get; set; }
	int musicAnalysis { get; set; }
	bool myPlex { get; set; }
	string? myPlexMappingState { get; set; }
	string? myPlexSigninState { get; set; }
	bool myPlexSubscription { get; set; }
	string? myPlexUsername { get; set; }
	int offlineTranscode { get; set; }
	string? ownerFeatures { get; set; }
	bool photoAutoTag { get; set; }
	string? platform { get; set; }
	string? platformVersion { get; set; }
	bool pluginHost { get; set; }
	bool pushNotifications { get; set; }
	bool readOnlyLibraries { get; set; }
	int size { get; set; }
	int streamingBrainABRVersion { get; set; }
	int streamingBrainVersion { get; set; }
	bool sync { get; set; }
	int transcoderActiveVideoSessions { get; set; }
	bool transcoderAudio { get; set; }
	bool transcoderLyrics { get; set; }
	bool transcoderSubtitles { get; set; }
	bool transcoderVideo { get; set; }
	string? transcoderVideoBitrates { get; set; }
	string? transcoderVideoQualities { get; set; }
	string? transcoderVideoResolutions { get; set; }
	int updatedAt { get; set; }
	bool updater { get; set; }
	string? version { get; set; }
	bool voiceSearch { get; set; }
}