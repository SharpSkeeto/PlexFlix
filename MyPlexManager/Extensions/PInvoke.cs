using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace MyPlexManager.Extensions;

public static class WindowExtensions
{
	public static void Maximize(this Window window)
	{
		var windowHandle = WindowNative.GetWindowHandle(window);
		PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_MAXIMIZE);
	}

	public static void Minimize(this Window window)
	{
		var windowHandle = WindowNative.GetWindowHandle(window);
		PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_MINIMIZE);
	}

	public static void Restore(this Window window)
	{
		var windowHandle = WindowNative.GetWindowHandle(window);
		PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_RESTORE);
	}
}


