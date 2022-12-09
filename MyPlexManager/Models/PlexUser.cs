using System;

namespace MyPlexManager.Models;

public class PlexUserAccount : IPlexUserAccount
{
	public User? user { get; set; }
}

public class User : IUser
{
	public int id { get; set; }
	public string? uuid { get; set; }
	public string? email { get; set; }
	public DateTime joined_at { get; set; }
	public string? username { get; set; }
	public string? title { get; set; }
	public string? thumb { get; set; }
	public bool hasPassword { get; set; }
	public string? authToken { get; set; }
	public string? authentication_token { get; set; }
	public Subscription? subscription { get; set; }
	public Roles? roles { get; set; }
	public string[]? entitlements { get; set; }
	public DateTime confirmedAt { get; set; }
	public object? forumId { get; set; }
	public bool rememberMe { get; set; }
}

public class Subscription : ISubscription
{
	public bool active { get; set; }
	public string? status { get; set; }
	public string? plan { get; set; }
	public string[]? features { get; set; }
}

public class Roles : IRoles
{
	public string[]? roles { get; set; }
}
