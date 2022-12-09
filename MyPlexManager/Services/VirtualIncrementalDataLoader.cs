using MyPlexManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyPlexManager.Services;

public class VirtualIncrementalDataLoader : IncrementalLoadingBase
{

	//protected IPlexApiService _plexApiClient;

	public VirtualIncrementalDataLoader()
	{
		//_plexApiClient = plexApiClient;

	}

	protected override bool HasMoreItemsOverride()
	{
		throw new NotImplementedException();
	}

	protected override Task<IList<object>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
	{
		throw new NotImplementedException();
	}
}
