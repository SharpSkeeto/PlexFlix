using Dapper;
using Microsoft.Data.Sqlite;
using MyPlexManager.Models;
using System;
using System.IO;

namespace MyPlexManager.Services;

internal class RepositoryService
{

	public static IAppSettings GetAppSettings()
	{
		if (!File.Exists(DbFile)) return null!;

		using var cnn = Sqlite3DbConnection();
		cnn.Open();
		AppSettings result = cnn.QueryFirstOrDefault<AppSettings>(
			@"SELECT Email, 
					 Token,
					 Thumb as ThumbNail, 
					 userId as UserName,
					 uri as ServerUri,
					 clientIdentifier as MachineId,
					 address as Address,
					 port as Port, 
					 protocol as Protocol,
				     name as ServerName,
					 product as ProductName,
					 productVersion as ProductVersion,
					 platform as Platform,
					 platformVersion as PlatformVersion,
					 device as Device,
					 deviceIdentifier as DeviceIdentifier,
                     deviceName as DeviceName,
                     deviceVersion as DeviceVersion,
                     productDevice as ProductDevice,
                     productPlatform as ProductPlatform
			from v_baseConfig");
		return result;
	}

	public static string DbFile
	{
		get { return App.STORAGE_FOLDER + "\\myplex.db"; }
	}

	public static SqliteConnection Sqlite3DbConnection()
	{
		return new SqliteConnection("Data Source=" + DbFile);
	}

	public static bool DbExists(string path)
	{
		return File.Exists(path);
	}

	public static bool CreateDatabase()
	{
		using var cnn = Sqlite3DbConnection();
		cnn.Open();
		cnn.Execute(
			@"CREATE TABLE User (
					userId TEXT (100) PRIMARY KEY ON CONFLICT FAIL
									  NOT NULL ON CONFLICT FAIL,
					token  TEXT (100) UNIQUE ON CONFLICT FAIL
									  NOT NULL ON CONFLICT FAIL,
					email  TEXT (100),
					thumb  TEXT (100) 
				);

				CREATE TABLE Server (
					userId           TEXT (100) REFERENCES User (userId),
					name             TEXT (100) NOT NULL,
					product          TEXT (100),
					productVersion   TEXT (100),
					platform         TEXT (100),
					platformVersion  TEXT (100),
					device           TEXT (100),
					clientIdentifier TEXT (100),
					protocol         TEXT (10),
					address          TEXT (100),
					port             TEXT (100),
					uri              TEXT (100),
					UNIQUE (
						userId,
						name
					)
				);

				CREATE TABLE Device (
					userId           TEXT (100) REFERENCES User (userId),
					deviceIdentifier TEXT (100) UNIQUE
												NOT NULL,
					deviceName       TEXT (100) NOT NULL,
					deviceVersion    TEXT (100) NOT NULL,
					productDevice    TEXT (100) NOT NULL
												DEFAULT Windows,
					productPlatform  TEXT (100) NOT NULL
												DEFAULT Desktop
				);

				CREATE VIEW v_baseConfig AS
					SELECT u.email,
						   u.token,
						   u.thumb,
						   s.*,
						   d.deviceIdentifier,
						   d.deviceName,
						   d.deviceVersion,
						   d.productDevice,
						   d.productPlatform
					  FROM User u
						   INNER JOIN
						   Server s ON s.userId = u.userId
						   LEFT JOIN
						   Device d ON d.userId = u.userId;
			");

		return true;
	}

	public static bool UpdateDatabase(IAppSettings appSettings) 
	{
		try
		{
			using var cnn = Sqlite3DbConnection();
			cnn.Open();

			using (var transaction = cnn.BeginTransaction())
			{

				try
				{
					var updateUserTable = cnn.CreateCommand();

					updateUserTable.CommandText = @$"UPDATE User SET 
														email = '{appSettings.Email}',
														token = '{appSettings.Token}'
													 WHERE userId = '{appSettings.UserName}'";

					var result = updateUserTable.ExecuteNonQuery();

					if (result > 0)
					{
						var updateServerTable = cnn.CreateCommand();

						updateServerTable.CommandText = @$"UPDATE Server SET 
															protocol = '{appSettings.Protocol}',
															address = '{appSettings.Address}',
															port = '{appSettings.Port}',
															uri = '{appSettings.ServerUri}'
														  WHERE userId = '{appSettings.UserName}'";


						result = updateServerTable.ExecuteNonQuery();

						if (result > 0)
						{
							transaction.Commit();
							return true;
						}
					}

					transaction.Rollback();
					return false;
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}

		}
		catch (Exception)
		{
			throw;
		}

	}

}