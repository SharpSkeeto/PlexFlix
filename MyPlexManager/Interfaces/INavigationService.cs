using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using MyPlexManager.Helpers;
using System;
using System.Collections.Generic;

namespace MyPlexManager.Interfaces;

public interface INavigationService
{
    string CurrentPage { get; }
    void NavigateTo(string page);
    void GoBack();
    void NavigateTo(string page, object parameter, NavigationTransitionInfo transitionInfo = null!);

    bool AddNavigationMenuItem(Type type, string menuTitle, object obj);
    bool AddNavigationMenuItem(Type type, string menuTitle, Symbol symbol, object obj);
    bool AddNavigationMenuItem(Type type, string menuTitle, Glyph glyph, object obj);

    NavigationViewItem GetCurrentNavigationViewItem();
    NavigationView GetNavigationInstance();
    List<NavigationViewItem> GetNavigationViewItems();
    List<NavigationViewItem> GetNavigationViewItems(Type type);
    List<NavigationViewItem> GetNavigationViewItems(Type type, string title);

    void SetCurrentNavigationViewItem(NavigationViewItem item);
    void SetCurrentPage(Type type);

    void PopulateNavigationMenu();

}
