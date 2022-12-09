using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using MyPlexManager.Extensions;

namespace MyPlexManager.Helpers;

internal static class FontIconHelper
{
	static public FontIcon GetFontIcon(Glyph glyph) => new() 
	{ 
		FontFamily = new FontFamily("Segoe MDL2 Assets"), 
		Glyph = glyph.ToGlyphHex() 
	};
}

/// <summary>
/// https://learn.microsoft.com/en-us/windows/apps/design/style/segoe-ui-symbol-font
/// </summary>
public enum Glyph
{
	TVMonitor = 0xE7F4,
	Movies = 0xE8B2,
	Wifi = 0xE701,
	MusicNote = 0xEC4F,
}