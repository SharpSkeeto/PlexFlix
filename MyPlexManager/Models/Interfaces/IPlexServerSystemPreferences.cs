namespace MyPlexManager.Models;

public interface IPlexServerSystemPreferences
{
	string identifier { get; set; }
	byte noHistory { get; set; }
	byte replaceParent { get; set; }
	ServerSystemSetting[] Setting { get; set; }
	byte size { get; set; }
}