namespace MyPlexManager.Models
{
	public interface IPlexTVUserDevices
	{
		PlexTVUserDevice[] Device { get; set; }
		string publicAddress { get; set; }
	}
}