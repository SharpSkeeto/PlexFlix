using Microsoft.UI.Xaml.Data;
using System;

namespace MyPlexManager.Converters;

public class DateToMonthStringConverter : IValueConverter
{
	// Define the Convert method to change a DateOnly object to a month string.
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		// The value parameter is the data from the source object.
		if (DateOnly.TryParse((string)value, out var thisDate))
		{
			return thisDate.ToString("MMMM");
		}
		return string.Empty;
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}

}

// more converters could go here if date related
public class ReleaseDateStringConverter : IValueConverter
{
	// Define the Convert method to change a DateOnly object to a string.
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		// The value parameter is the data from the source object.
		if(DateOnly.TryParse((string)value, out var thisDate))
		{
			return thisDate.ToString("MMMM dd, yyyy");
		}
		return string.Empty;
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}

}