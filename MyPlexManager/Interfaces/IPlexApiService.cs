using MyPlexManager.Models;
using MyPlexManager.Models.XML;
using MyPlexManager.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPlexManager.Interfaces;

public interface IPlexApiService
{
    Task<PlexUserAccount> GetPlexAccountInfo(string user, string pass, string mfaCode);
    Task<PlexUserAccount> GetPlexTVAccountUser();
    Task<List<PlexTVAccountDevice>> GetPlexTVAccountDevices();
    Task<List<PlexTVRemoteServer>> GetPlexTVRemoteServers();
    Task<List<PlexTVUserDevice>> GetPlexTVUserDevices();
    Task<PlexTVServerLibraries> GetPlexTVLibrariesByMachineId();
    Task<PlexServerLibraries> GetPlexLibraries();
    Task<PlexServerInfo> GetPlexServerInfo();
    Task<PlexServerIdentity> GetPlexServerIdentity();
    Task<MyPlexServerAccount> GetMyPlexServerAccount();
    Task<PlexServerAccounts> GetPlexServerAccounts();
    Task<PlexServerPreferences> GetPlexServerPreferences();
    Task<PlexServerSystemPreferences> GetPlexServerSystemPreferences();
    Task<PlexServers> GetPlexServers();
    Task<PlexDevices> GetPlexDevices();
    Task<PlexMediaPosters> GetPlexMediaPosters(string ratingKey);
    Task<PlexMediaProviders> GetPlexMediaProviders();
    Task<PlexMediaLibraryItems> GetPlexMediaLibraryItems(string libraryId, Enum @enum);
	Task<PlexMediaLibraryItems> GetPlexMediaCollectionItems(string key);
	Task<PlexMediaLibraryItems> GetPlexChildrenData(string key);
	Task<PlexMediaLibraryItems> GetPlexMediaMetaData(string key);
	Task<PlexMediaLibraryItems> GetPlexMediaMetaRelatedData(string key);
}
