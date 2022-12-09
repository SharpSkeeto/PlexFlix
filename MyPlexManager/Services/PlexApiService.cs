using MyPlexManager.Interfaces;
using MyPlexManager.Models;
using MyPlexManager.Models.XML;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using RestSharp.Serializers.Xml;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyPlexManager.Services;

internal class PlexApiService : IPlexApiService, IDisposable
{
	// PlexTV remote api
	private const string PlexTVSignInUri = "https://plex.tv/users/sign_in.json";
	private const string PlexTVFriendsUri = "https://plex.tv/pms/friends/all";
	private const string PlexTVGetAccountUri = "https://plex.tv/users/account.json";
	private const string PlexTVServersUri = "https://plex.tv/pms/servers.xml";
	private const string PlexTVWatchlistUri = "https://metadata.provider.plex.tv/";
	private const string PlexTVDevicesUri = "https://plex.tv/devices.xml";
	private const string PlexTVResourcesUri = "https://plex.tv/api/resources";
	private const string PlexTVLibrariesByMachineIdUri = "https://plex.tv/api/servers/{machineIdentifier}";

	// Local Plex server api
	// get server info / health check
	private const string ServerBaseUri = "/";
	// get libraries
	private const string GetLibrariesUri = "/library/sections";
	// get library items by id
	private const string GetMediaLibraryItemsUri = "/library/sections/{libraryId}/all";
	// get media meta data by ratingKey (id)
	private const string GetMediaMetaDataUri = "/library/metadata/{ratingKey}";
	// get media meta related data by ratingKey (id)
	private const string GetMediaMetaRelatedDataUri = "/library/metadata/{ratingKey}/related";
	// get library collection items
	private const string GetMediaCollectionsItemsUri = "/library/collections/{ratingKey}/children";
	// get server identity / get client/machine id
	private const string GetServerIdentityUri = "/identity";
	// get MyPlex server account
	private const string GetMyPlexServerAccountUri = "/myplex/account";
	// get server accounts
	private const string GetPlexServerAccountsUri = "/accounts";
	// get server preferences
	private const string GetPlexServerPreferencesUri = "/:/prefs";
	// get server system preferences
	private const string GetPlexServerSystemPreferencesUri = "/system/:/prefs";
	// get servers
	private const string GetPlexServersUri = "/servers";
	// get devices
	private const string GetPlexDevicesUri = "/devices";
	// get media posters
	private const string GetPlexMediaPostersUri = "/library/metadata/{ratingKey}/posters";
	// get media providers
	private const string GetMediaProvidersUri = "/media/providers";


	private static readonly RestClient restClient = new();
	private static readonly RestClient xmlRestClient = new();
	private readonly IAppSettings? appSettings;
	private bool disposedValue;


	public PlexApiService()
	{
		restClient.UseNewtonsoftJson();
		xmlRestClient.UseDotNetXmlSerializer();
		appSettings = App.CurrentAppSettings;
	}

	public async Task<PlexServerLibraries> GetPlexLibraries()
	{
		try
		{

			var request = new RestRequest(BuildUrl(GetLibrariesUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexServerLibraries>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaLibraryItems> GetPlexMediaLibraryItems(string libraryId, Enum mediaType)
	{
		try
		{
			var type = GetMediaTypeEnumValue((PlexMediaType)mediaType);
			//var type = (int)Enum.Parse(typeof(PlexMediaType), mediaType.ToString());
			var request = new RestRequest(BuildUrl(GetMediaLibraryItemsUri), Method.Get)
							 .AddUrlSegment("libraryId", libraryId)
							 .AddQueryParameter("includeGuids", "0")                // 1 = true, 0 = false
							 .AddQueryParameter("type", $"{type}")                  // 1 = movie, 2 = show, 8 = artist/music
							 .AddQueryParameter("sort", "titleSort")                // need all sort actions (to make enums), desc: titleSort:desc
							 .AddQueryParameter("includeCollections", "1")          // 1 = true, 0 = false
							 .AddQueryParameter("includeExternalMedia", "1")        // 1 = true, 0 = false
							 .AddQueryParameter("includeAdvanced", "1")             // 1 = true, 0 = false
							 .AddQueryParameter("includeMeta", "1")                 // 1 = true, 0 = false
							 //.AddHeader("X-Plex-Container-Start", "0")			// can get offsets from response header: X-Plex-Container-Start  
							 //.AddHeader("X-Plex-Container-Size", "50")			// 50, number of records to be returned.
							 .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaLibraryItems>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				//var offset = apiResponse.Headers?.FirstOrDefault(i => i.Name == "X-Plex-Container-Start")?.Value;
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaLibraryItems> GetPlexMediaCollectionItems(string key)
	{
		try
		{
			var request = new RestRequest(BuildUrl(key), Method.Get)
							 .AddQueryParameter("excludeAllLeaves", "1")	// 1 = true, 0 = false
							 //.AddHeader("X-Plex-Container-Start", "0")    // can get offsets from response header: X-Plex-Container-Start  
							 //.AddHeader("X-Plex-Container-Size", "50")    // 50, number of records to be returned.
							 .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaLibraryItems>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaLibraryItems> GetPlexChildrenData(string key)
	{
		try
		{
			var request = new RestRequest(BuildUrl(key), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaLibraryItems>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaLibraryItems> GetPlexMediaMetaData(string key)
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetMediaMetaDataUri), Method.Get)
								.AddUrlSegment("ratingKey", key)
								.AddQueryParameter("includeGuids", "0")
								.AddQueryParameter("includeConcerts", "1")
								.AddQueryParameter("includeExtras", "1")
								.AddQueryParameter("includeOnDeck", "1")
								.AddQueryParameter("includePopularLeaves", "1")
								.AddQueryParameter("includePreferences", "1")
								.AddQueryParameter("includeReviews", "1")
								.AddQueryParameter("includeChapters", "1")
								.AddQueryParameter("includeStations", "1")
								.AddQueryParameter("includeExternalMedia", "1")
								.AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaLibraryItems>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaLibraryItems> GetPlexMediaMetaRelatedData(string key)
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetMediaMetaRelatedDataUri), Method.Get)
							  .AddUrlSegment("ratingKey", key)
							  .AddQueryParameter("includeGuids", "0")
							  .AddQueryParameter("includeAugmentations", "1")
							  .AddQueryParameter("includeExternalMedia", "1")
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaLibraryItems>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexUserAccount> GetPlexAccountInfo(string user, string pass, string mfaCode)
	{
		try
		{
			PlexLoginModel login = new() { login = $"{user}", password = $"{pass}{mfaCode}" };
			PlexUserLogin loginRequest = new() { User = login };
			PlexUserAccount? authenticatedPlexUserAccount = default;

			var request = new RestRequest(PlexTVSignInUri, Method.Post)
			.AddJsonBody(loginRequest)
			.AddHeaders(await AddPlexHeaders());

			var apiResponse = await restClient.ExecuteAsync<PlexUserAccount>(request);

			if (apiResponse.StatusCode == HttpStatusCode.Created)
			{
				authenticatedPlexUserAccount = apiResponse.Data!;
			}
			if (apiResponse.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw apiResponse?.ErrorException ?? new Exception("Unauthorized HTTP status code returned.");
			}

			return authenticatedPlexUserAccount!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<List<PlexTVAccountDevice>> GetPlexTVAccountDevices()
	{
		try
		{
			var request = new RestRequest(PlexTVResourcesUri, Method.Get)
							 .AddQueryParameter("includeHttps", "1")
							 .AddQueryParameter("includeRelay", "1")
							 .AddQueryParameter("includeIPv6", "1")
							 .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await xmlRestClient.ExecuteAsync<PlexTVAccountDevices>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!.Device.ToList();
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<List<PlexTVRemoteServer>> GetPlexTVRemoteServers()
	{
		try
		{
			var request = new RestRequest(PlexTVServersUri, Method.Get)
							 .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await xmlRestClient.ExecuteAsync<PlexTVRemoteServers>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!.Server.ToList();
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexUserAccount> GetPlexTVAccountUser()
	{
		try
		{
			var request = new RestRequest(PlexTVGetAccountUri, Method.Get)
			.AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexUserAccount>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<List<PlexTVUserDevice>> GetPlexTVUserDevices()
	{
		try
		{
			var request = new RestRequest(PlexTVDevicesUri, Method.Get)
							 .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await xmlRestClient.ExecuteAsync<PlexTVUserDevices>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!.Device.ToList();
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexTVServerLibraries> GetPlexTVLibrariesByMachineId()
	{
		try
		{
			var request = new RestRequest(PlexTVLibrariesByMachineIdUri, Method.Get)
							 .AddUrlSegment("machineIdentifier", appSettings?.MachineId!)
							 .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await xmlRestClient.ExecuteAsync<PlexTVServerLibraries>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexServerInfo> GetPlexServerInfo()
	{
		try
		{
			var request = new RestRequest(BuildUrl(ServerBaseUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexServerInfo>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexServerIdentity> GetPlexServerIdentity()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetServerIdentityUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexServerIdentity>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<MyPlexServerAccount> GetMyPlexServerAccount()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetMyPlexServerAccountUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<MyPlexServerAccount>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexServerAccounts> GetPlexServerAccounts()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetPlexServerAccountsUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexServerAccounts>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexServerPreferences> GetPlexServerPreferences()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetPlexServerPreferencesUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexServerPreferences>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexServerSystemPreferences> GetPlexServerSystemPreferences()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetPlexServerSystemPreferencesUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await xmlRestClient.ExecuteAsync<PlexServerSystemPreferences>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexServers> GetPlexServers()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetPlexServersUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexServers>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexDevices> GetPlexDevices()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetPlexDevicesUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexDevices>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaPosters> GetPlexMediaPosters(string ratingKey)
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetPlexMediaPostersUri), Method.Get)
							  .AddUrlSegment("ratingKey", ratingKey)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaPosters>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	public async Task<PlexMediaProviders> GetPlexMediaProviders()
	{
		try
		{
			var request = new RestRequest(BuildUrl(GetMediaProvidersUri), Method.Get)
							  .AddHeaders(await AddPlexHeaders(appSettings?.Token!));

			var apiResponse = await restClient.ExecuteAsync<PlexMediaProviders>(request);

			if (apiResponse.StatusCode == HttpStatusCode.OK)
			{
				return apiResponse.Data!;
			}

			return null!;

		}
		catch (Exception ex)
		{
			Log.Error($"{ex}");
			throw;
		}
	}

	private async Task<Dictionary<string, string>> AddPlexHeaders(string authToken)
	{
		var headers = await AddPlexHeaders();
		headers.Add("X-Plex-Token", authToken);
		return headers;
	}

	private Task<Dictionary<string, string>> AddPlexHeaders()
	{
		var headers = new Dictionary<string, string>
		{
			{ "X-Plex-Client-Identifier", $"{appSettings?.DeviceIdentifier}" },
			{ "X-Plex-Product", $"{appSettings?.DeviceName}" },
			{ "X-Plex-Version", $"{appSettings?.DeviceVersion}" },
			{ "X-Plex-Device", $"{appSettings?.ProductDevice}" },
			{ "X-Plex-Platform", $"{appSettings?.ProductPlatform}" }
		};
		return Task.FromResult(headers);
	}

	private string BuildUrl(string resource)
	{
		return $"{appSettings?.Protocol}://{appSettings?.Address}:{appSettings?.Port}{resource}";
	}

	public static int GetMediaTypeEnumValue(PlexMediaType plexMediaType)
	{
		int myReturn = plexMediaType switch
		{
			PlexMediaType.movie => 1,
			PlexMediaType.show => 2,
			PlexMediaType.artist => 8,
			PlexMediaType.album => 10,
			_ => 0,
		};

		return myReturn;
	}


	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects)
				restClient.Dispose();
				xmlRestClient.Dispose();
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		}
	}

	// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
	// ~MyPlexManagerApiClient()
	// {
	//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
	//     Dispose(disposing: false);
	// }

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	#endregion

}

internal enum PlexMediaType
{
	unknown = 0,
	movie = 1,
	show = 2,
	artist = 8,
	album = 10,
	track = 15,
	collection = 20,
	season = 40,
	episode = 60
}


