using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.Models;
using MyPlexManager.Views;
using System.Linq;
using Action = System.Action;

namespace MyPlexManager.Windows;

public sealed partial class WindowShell : Window
{

	public Action? NavigationViewLoaded { get; set; }

	private void NavigationViewControl_Loaded(object sender, RoutedEventArgs e)
	{
		// Navigates, but does not update the Menu.
		navigationService?.SetCurrentNavigationViewItem(navigationService?.GetNavigationViewItems(typeof(HomePage)).First()!);
	}

	private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
	{
		if (ContentFrame.CanGoBack) ContentFrame.GoBack();
	}

	private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
	{
		if (args.IsSettingsInvoked == true)
		{
			if (ContentFrame.CurrentSourcePageType != typeof(SettingsPage))
			{
				ContentFrame.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
			}
		}
		else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.DataContext != null))
		{
			var selectedItem = args.InvokedItemContainer;
			if (selectedItem.DataContext is Directory context)
			{
				switch (context.type)
				{
					case "movie":
						navigationService?.NavigateTo("MediaMoviePage", context!, args.RecommendedNavigationTransitionInfo);
						break;
					case "show":
						navigationService?.NavigateTo("MediaShowPage", context!, args.RecommendedNavigationTransitionInfo);
						break;
					case "artist":
						navigationService?.NavigateTo("MediaArtistPage", context!, args.RecommendedNavigationTransitionInfo);
						break;
					default:
						navigationService?.NavigateTo("HomePage", null!, args.RecommendedNavigationTransitionInfo);
						break;
				}
			}
		}
		else
		{
			if (args.InvokedItemContainer is not null)
			{
				if ((string)args.InvokedItemContainer.Content == "Home")
				{
					navigationService?.NavigateTo("HomePage", null!, args.RecommendedNavigationTransitionInfo);
				}
			}

		}
	}

	private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
	{
		NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

		if (ContentFrame.SourcePageType == typeof(SettingsPage))
		{
			// SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
			NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
		}
		else if (ContentFrame.SourcePageType != null)
		{

			NavigationViewItem? selectedItem;

			if (e.Parameter is Directory)
			{
				var p = (Directory)e.Parameter!;

				var title = p.title;

				selectedItem = NavigationViewControl?.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(f => f.Content.ToString() == title);

				if (NavigationViewControl is not null)
					NavigationViewControl.SelectedItem = selectedItem!;
			}

			if (ContentFrame.Content is HomePage)
			{
				selectedItem = NavigationViewControl?.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(f => f.Content.ToString() == "Home");

				if (NavigationViewControl is not null)
					NavigationViewControl.SelectedItem = selectedItem!;
			}

			//NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
			//	.OfType<NavigationViewItem>()
			//	.First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName!.ToString()));
		}

		if (NavigationViewControl is not null)
			NavigationViewControl.Header = ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
	}

	//public void EnsureNavigationSelection(string id)
	//{
	//	foreach (object rawGroup in this.NavigationViewControl.MenuItems)
	//	{
	//		if (rawGroup is NavigationViewItem group)
	//		{
	//			foreach (object rawItem in group.MenuItems)
	//			{
	//				if (rawItem is NavigationViewItem item)
	//				{
	//					if ((string)item.Tag == id)
	//					{
	//						group.IsExpanded = true;
	//						NavigationViewControl.SelectedItem = item;
	//						item.IsSelected = true;
	//						return;
	//					}
	//				}
	//			}
	//		}
	//	}
	//}

}
