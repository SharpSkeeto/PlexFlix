using System;

namespace MyPlexManager.Models;

public interface IUser
{
	string? authentication_token { get; set; }
	string? authToken { get; set; }
	DateTime confirmedAt { get; set; }
	string? email { get; set; }
	string[]? entitlements { get; set; }
	object? forumId { get; set; }
	bool hasPassword { get; set; }
	int id { get; set; }
	DateTime joined_at { get; set; }
	bool rememberMe { get; set; }
	Roles? roles { get; set; }
	Subscription? subscription { get; set; }
	string? thumb { get; set; }
	string? title { get; set; }
	string? username { get; set; }
	string? uuid { get; set; }
}