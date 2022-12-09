using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using WinRT.Interop;

namespace MyPlexManager.Helpers;

public class WindowHelper
{
	static public Window CreateWindow()
	{
		Window window = new();
		TrackWindow(window);
		return window;
	}

	static public void TrackWindow(Window window)
	{
		window.Closed += (sender, args) => {
			_activeWindows.Remove(window);
		};
		_activeWindows.Add(window);
	}

	static public Window GetWindowForElement(UIElement element)
	{
		if (element.XamlRoot != null)
		{
			foreach (Window window in _activeWindows)
			{
				if (element.XamlRoot == window.Content.XamlRoot)
				{
					return window;
				}
			}
		}
		return null!;
	}

	static public UIElement? FindElementByName(UIElement element, string name)
	{
		if (element.XamlRoot != null && element.XamlRoot.Content != null)
		{
			var ele = (element.XamlRoot.Content as FrameworkElement)!.FindName(name);
			if (ele != null)
			{
				return ele as UIElement;
			}
		}
		return null;
	}

	static public AppWindow GetAppWindow(Window window)
	{
		IntPtr hwnd = WindowNative.GetWindowHandle(window);
		WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
		return AppWindow.GetFromWindowId(windowId);
	}

	static public IntPtr GetHwnd(Window newWindow)
	{
		return WindowNative.GetWindowHandle(newWindow);
	}

	static public List<Window> ActiveWindows { get { return _activeWindows; } }

	private static readonly List<Window> _activeWindows = new();
}
