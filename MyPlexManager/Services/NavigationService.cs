using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MyPlexManager.Helpers;
using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Views;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlexManager.Services;

public class NavigationService : INavigationService
{
	private readonly IDictionary<string, Type> _pages = new ConcurrentDictionary<string, Type>();
	public const string RootPage = "(Root)";
	public const string UnknownPage = "(Unknown)";

	//private static Frame AppFrame => (Frame)Window.Current.Content;

	private static readonly Window? mainWindow = (App.Current as App)?.m_window!;
	private static readonly Frame? appFrame = (Frame?)WindowHelper.FindElementByName(mainWindow.Content!, "ContentFrame");
	private static readonly NavigationView? NavigationViewControl = (NavigationView?)WindowHelper.FindElementByName(mainWindow.Content!, "NavigationViewControl");


	public void Configure(string page, Type type)
	{
		if (_pages.Values.Any(v => v == type))
		{
			throw new ArgumentException($"The {type.Name} view has already been registered under another name.");
		}

		_pages[page] = type;
	}

	/// <summary>
	/// Gets the name of the currently displayed page.
	/// </summary>
	public string CurrentPage
	{
		get
		{
			var frame = appFrame!;

			if (frame.BackStackDepth == 0)
				return RootPage;

			if (frame.Content == null)
				return UnknownPage;

			var type = frame.Content.GetType();

			if (_pages.Values.All(v => v != type))
				return UnknownPage;

			var item = _pages.Single(i => i.Value == type);

			return item.Key;
		}
	}

	public void NavigateTo(string page)
	{
		NavigateTo(page, null!);
	}

	public void NavigateTo(string page, object parameter, NavigationTransitionInfo transitionInfo = null!)
	{
		if (!_pages.ContainsKey(page))
		{
			throw new ArgumentException($"Unable to find a page registered with the name {page}.");
		}
		if (mainWindow is not null)
		{
			if (appFrame != null)
			{
				appFrame.Navigate(_pages[page], parameter, transitionInfo);
			}
		}
	}

	public void GoBack()
	{
		if (appFrame?.CanGoBack == true)
		{
			appFrame.GoBack();
		}
	}

	public bool AddNavigationMenuItem(Type type, string menuTitle, object obj)
	{
		NavigationViewItem item = new NavigationViewItem()
		{
			Tag = type?.FullName,
			Content = menuTitle,
			DataContext = obj
		};

		NavigationViewControl?.MenuItems.Add(item);
		return true;
	}

	public bool AddNavigationMenuItem(Type type, string menuTitle, Symbol symbol, object obj)
	{
		NavigationViewItem item = new NavigationViewItem()
		{
			Tag = type?.FullName,
			Content = menuTitle,
			Icon = new SymbolIcon(symbol),
			DataContext = obj
		};

		NavigationViewControl?.MenuItems.Add(item);
		return true;
	}

	public bool AddNavigationMenuItem(Type type, string menuTitle, Glyph glyph, object obj)
	{
		NavigationViewItem item = new NavigationViewItem()
		{
			Tag = type?.FullName,
			Content = menuTitle,
			Icon = FontIconHelper.GetFontIcon(glyph),
			DataContext = obj
		};

		NavigationViewControl?.MenuItems.Add(item);
		return true;
	}

	public NavigationViewItem GetCurrentNavigationViewItem()
	{
		return (NavigationViewItem)NavigationViewControl?.SelectedItem!;
	}

	public NavigationView GetNavigationInstance() => NavigationViewControl!;

	public List<NavigationViewItem> GetNavigationViewItems()
	{
		var result = new List<NavigationViewItem>();
		var items = NavigationViewControl?.MenuItems.Select(i => (NavigationViewItem)i).ToList();
		items?.AddRange(NavigationViewControl?.FooterMenuItems?.Select(i => (NavigationViewItem)i).Where(w => w.Tag is not null)!);
		result.AddRange(items!);
		foreach (NavigationViewItem mainItem in items!)
		{
			result.AddRange(mainItem.MenuItems.Select(i => (NavigationViewItem)i));
		}
		return result;
	}

	public List<NavigationViewItem> GetNavigationViewItems(Type type)
	{
		return GetNavigationViewItems().Where(i => i.Tag.ToString() == type.FullName).ToList();
	}

	public List<NavigationViewItem> GetNavigationViewItems(Type type, string title)
	{
		return GetNavigationViewItems(type).Where(ni => ni.Content.ToString() == title).ToList();
	}

	public void SetCurrentNavigationViewItem(NavigationViewItem item)
	{
		if (item == null)
			return;
		if (item.Tag == null)
			return;
		
		appFrame?.Navigate(Type.GetType(item.Tag.ToString()!), item.Content, new EntranceNavigationTransitionInfo());

		if(NavigationViewControl is not null)
		{
			NavigationViewControl.Header = item.Content!;
			NavigationViewControl.SelectedItem = item;
		}
	}

	public void SetCurrentPage(Type type)
	{
		appFrame?.Navigate(type);
	}

	public async Task PopulateNavigationMenu()
	{
		try
		{
			await Task.Delay(200);
			var client = Ioc.Default.GetService<IPlexApiService>();
			if (client is not null)
			{
				PlexServerLibraries PMSLibraries = await client.GetPlexLibraries();

				if (PMSLibraries is not null && PMSLibraries.Mediacontainer is not null)
				{
					foreach (var lib in PMSLibraries.Mediacontainer.Directory!.ToList().OrderBy(o => o.key))
					{
						if (lib is not null)
						{
							switch (lib.type!)
							{
								case "movie":
									AddNavigationMenuItem(typeof(MediaMoviePage), lib.title!, Glyph.Movies, lib);
									break;
								case "show":
									AddNavigationMenuItem(typeof(MediaShowPage), lib.title!, Glyph.TVMonitor, lib);
									break;
								case "artist":
									AddNavigationMenuItem(typeof(MediaArtistPage), lib.title!, Glyph.MusicNote, lib);
									break;
								case "photo":
									AddNavigationMenuItem(typeof(MediaMoviePage), lib.title!, Symbol.Camera, lib);
									break;
							}
						}
					}
				}
			}

		}
		catch (Exception ex)
		{
			Log.Error(ex.ToString());
			throw;
		}
	}

}
