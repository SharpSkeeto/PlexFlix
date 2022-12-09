using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.Helpers;
using MyPlexManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
{
	public HomePage()
	{
		this.InitializeComponent();


	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		var navigation = Ioc.Default.GetService<INavigationService>();

		//var navigation = (App.Current as App)!.Navigation;
		navigation?.AddNavigationMenuItem(typeof(AboutPage), "About", Glyph.Movies, null!);
	}

}
