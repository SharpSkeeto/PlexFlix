using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyPlexManager.Models;

namespace MyPlexManager.Views;

public class MetadataTemplateSelector : DataTemplateSelector
{
	public DataTemplate? Single { get; set; }
	public DataTemplate? Collection { get; set; }

	protected override DataTemplate SelectTemplateCore(object item)
	{
		var thisItem = (Metadata)item;
		if (thisItem.year == 0)
		{
			return Collection!;
		}
		else
		{
			return Single!;
		}
	}
}