namespace MyPlexManager.Models;

public interface IServerAccounts
{
	Account[]? Account { get; set; }
	string? identifier { get; set; }
	int size { get; set; }
}