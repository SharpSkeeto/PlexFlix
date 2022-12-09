using MyPlexManager.Interfaces;
using MyPlexManager.Models;

namespace MyPlexManager.Services;


public class AppSettingsService : IAppSettingsService
{

	public IAppSettings GetApplicationSettings()
	{
		if (!RepositoryService.DbExists(RepositoryService.DbFile))
		{
			RepositoryService.CreateDatabase();
		}
		return RepositoryService.GetAppSettings();
	}

	public IAppSettings UpdateApplicationSettings(IAppSettings appSettings)
	{
		if (!RepositoryService.DbExists(RepositoryService.DbFile))
		{
			RepositoryService.CreateDatabase();
		}

		if(RepositoryService.UpdateDatabase(appSettings))
		{
			return GetApplicationSettings();
		}
		return null!;
	}

}
