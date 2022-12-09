using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace MyPlexManager.Extensions;

public static class IMemoryCacheExtensions
{
	static readonly List<string> entries = new();

	public static void Clear(this IMemoryCache cache)
	{
		for (int i = 0; i < entries.Count; i++)
		{
			cache.Remove(entries[i]);
		}
		entries.Clear();
	}

	public static bool TryGetValueExt<TItem>(this IMemoryCache cache, string key, out TItem value)
	{
		if (!entries.Contains(key))
		{
			entries.Add(key);
		}

		if (cache.TryGetValue(key, out object? result))
		{
			if (result == null)
			{
				value = default!;
				return false;
			}

			if (result is TItem item)
			{
				value = item;
				return true;
			}
		}

		value = default!;
		return false;
	}
}
