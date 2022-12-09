using MyPlexManager.Helpers;
using System.Globalization;

namespace MyPlexManager.Extensions;

public static class EnumExtensions
{
	public static string ToGlyphHex(this Glyph g)
	{
		int @enum = (int)g;
		var hex = @enum.ToString("X4");
		var hexChar = (char)short.Parse(hex, NumberStyles.AllowHexSpecifier);
		return hexChar.ToString();
	}
}


