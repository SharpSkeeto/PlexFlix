namespace MyPlexManager.Models;

public interface IMyplex
{
	string? authToken { get; set; }
	string? mappingError { get; set; }
	string? mappingState { get; set; }
	string? privateAddress { get; set; }
	int privatePort { get; set; }
	string? publicAddress { get; set; }
	int publicPort { get; set; }
	string? signInState { get; set; }
	bool subscriptionActive { get; set; }
	string? subscriptionFeatures { get; set; }
	string? subscriptionState { get; set; }
	string? username { get; set; }
}