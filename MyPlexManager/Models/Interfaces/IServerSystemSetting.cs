namespace MyPlexManager.Models;

public interface IServerSystemSetting
{
	byte @default { get; set; }
	string id { get; set; }
	string label { get; set; }
	bool secure { get; set; }
	string type { get; set; }
	byte value { get; set; }
	string values { get; set; }
}