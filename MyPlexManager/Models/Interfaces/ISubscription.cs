namespace MyPlexManager.Models;

public interface ISubscription
{
	bool active { get; set; }
	string[]? features { get; set; }
	string? plan { get; set; }
	string? status { get; set; }
}