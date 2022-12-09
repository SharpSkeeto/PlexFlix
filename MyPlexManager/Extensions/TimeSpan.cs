using System;

namespace MyPlexManager.Extensions;

public static class TimeSpanExtensions
{
	public static string ToReadableAgoString(this TimeSpan span, double justNowSecondsThreshold)
	{
		if (span.TotalSeconds < justNowSecondsThreshold)
			return "just now";

		return span switch
		{
			TimeSpan { TotalDays: > 1 } => string.Format("{0:0} day{1} ago", span.Days, span.Days == 1 ? string.Empty : "s"),
			TimeSpan { TotalHours: > 1 } => string.Format("{0:0} hour{1} ago", span.Hours, span.Hours == 1 ? string.Empty : "s"),
			TimeSpan { TotalMinutes: > 1 } => string.Format("{0:0} minute{1} ago", span.Minutes, span.Minutes == 1 ? string.Empty : "s"),
			_ => string.Format("{0:0} second{1} ago", span.Seconds, span.Seconds == 1 ? string.Empty : "s")
		};
	}
}
