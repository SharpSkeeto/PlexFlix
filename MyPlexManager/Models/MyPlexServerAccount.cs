namespace MyPlexManager.Models;

public class MyPlexServerAccount : IMyPlexServerAccount
{
	public Myplex? MyPlex { get; set; }
}

public class Myplex : IMyplex
{
	public string? authToken { get; set; }
	public string? username { get; set; }
	public string? mappingState { get; set; }
	public string? mappingError { get; set; }
	public string? signInState { get; set; }
	public string? publicAddress { get; set; }
	public int publicPort { get; set; }
	public string? privateAddress { get; set; }
	public int privatePort { get; set; }
	public string? subscriptionFeatures { get; set; }
	public bool subscriptionActive { get; set; }
	public string? subscriptionState { get; set; }
}

