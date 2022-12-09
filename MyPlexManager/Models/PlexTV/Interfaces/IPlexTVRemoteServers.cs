namespace MyPlexManager.Models
{
	public interface IPlexTVRemoteServers
	{
		string friendlyName { get; set; }
		string identifier { get; set; }
		string machineIdentifier { get; set; }
		PlexTVRemoteServer[] Server { get; set; }
		byte size { get; set; }
	}
}