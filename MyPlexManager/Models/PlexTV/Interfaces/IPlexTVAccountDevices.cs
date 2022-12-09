namespace MyPlexManager.Models
{
	public interface IPlexTVAccountDevices
	{
		PlexTVAccountDevice[] Device { get; set; }
		byte size { get; set; }
	}
}