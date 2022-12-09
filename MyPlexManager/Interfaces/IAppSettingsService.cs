using MyPlexManager.Models;
using System.Threading.Tasks;

namespace MyPlexManager.Interfaces;

public interface IAppSettingsService
{
	IAppSettings GetApplicationSettings();
	IAppSettings UpdateApplicationSettings(IAppSettings appSettings);
}