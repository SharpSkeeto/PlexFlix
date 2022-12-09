namespace MyPlexManager.Models;

public interface IPlexLoginModel
{
	string? login { get; set; }
	string? password { get; set; }
}