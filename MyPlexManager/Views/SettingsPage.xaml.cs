using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyPlexManager.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyPlexManager.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsPage : Page
{

	public SettingsViewModel? SettingsViewModel { get; } = Ioc.Default.GetService<SettingsViewModel>();
	
	public SettingsPage()
	{
		this.InitializeComponent();

		this.Loaded += (s, e) =>
		{
		};

		this.Unloaded += (s, e) =>
		{
		};

	}

	protected override void OnNavigatedTo(NavigationEventArgs e)
	{
		base.OnNavigatedTo(e);
		SettingsViewModel?.InitializeSettingsData();
		progressRing.Visibility = Visibility.Collapsed;
	}
}
