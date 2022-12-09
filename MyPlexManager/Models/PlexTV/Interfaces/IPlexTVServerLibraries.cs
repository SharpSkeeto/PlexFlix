namespace MyPlexManager.Models.XML;

public interface IPlexTVServerLibraries
{
	string friendlyName { get; set; }
	string identifier { get; set; }
	string machineIdentifier { get; set; }
	Server Server { get; set; }
	byte size { get; set; }
}