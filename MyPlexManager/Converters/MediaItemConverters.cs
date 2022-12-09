using Microsoft.UI.Xaml.Data;
using System;

namespace MyPlexManager.Converters;

public class MediaCollectionItemCountConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		// The value parameter is the data from the source object.
		_ = int.TryParse((string)value, out var thisValue);
		string movie = thisValue < 2 ? "movie" : "movies";
		return string.Format($"{value} {movie}");
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}
}

public class MediaShowSeasonCountConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		// The value parameter is the data from the source object.
		_ = int.TryParse((string)value, out var thisValue);
		string season = thisValue < 2 ? "season" : "seasons";
		return string.Format($"{value} {season}");
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}
}

public class MediaShowEpisodeCountConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		// The value parameter is the data from the source object.
		string episode = (int)value < 2 ? "episode" : "episodes";
		return string.Format($"{value} {episode}");
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}
}

public class MediaShowEpisodeNumberConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		// The value parameter is the data from the source object.
		return string.Format($"E{value}");
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}
}

public class MediaShowEpisodeDurationConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if(double.TryParse(value.ToString(), out var durationValue))
		{
			var duration = TimeSpan.FromMilliseconds(durationValue);
			if (duration.Hours < 1)
			{
				return $"{duration.Minutes} min";
			}
			return $"{duration.Hours} hr {duration.Minutes} min";
		}

		return "0";
		
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}

}


public class MediaSongDurationConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (double.TryParse(value.ToString(), out var durationValue))
		{
			var duration = TimeSpan.FromMilliseconds(durationValue);

			if (duration.Hours < 1)
			{
				return $"{duration.Minutes}:{duration.Seconds:00}";
			}
			return $"{duration.Hours}:{duration.Minutes}:{duration.Seconds:00}";
		}

		return "0";
	}

	// ConvertBack is not implemented for a OneWay binding.
	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return string.Empty;
	}

}
