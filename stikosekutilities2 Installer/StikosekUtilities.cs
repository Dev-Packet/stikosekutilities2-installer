using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;

namespace stikosekutilities2_Installer
{
	public class StikosekUtilities
	{
		public string Owner;
		public string Repo;
		public string FileName;

		public string ApiUrl => GetApiUrl(Owner, Repo);
		public string DownloadUrl => $"https://github.com/{Owner}/{Repo}/releases/latest/download/{FileName}";
		
		private const string UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0";

		private Version cachedVersion = null;

		private static string GetApiUrl(string owner, string repo)
		{
			return $"https://api.github.com/repos/{owner}/{repo}/releases";
		}

		private void InstallBepInEx(string path)
		{
			// Find newest BepInEx download
			string bepinexDownload = "https://github.com/BepInEx/BepInEx/releases/download/v5.4.21/BepInEx_x64_5.4.21.0.zip";

			try
			{
				using WebClient bepClient = new();
				bepClient.Headers.Add(UserAgent);

				string json = bepClient.DownloadString(GetApiUrl("Bepinex", "Bepinex"));

				JArray jArr = JArray.Parse(json);

				JObject latestRelease = jArr.Select(t => t.ToObject<JObject>())
					.Where(obj => obj.TryGetValue("prerelease", out JToken token) && !token.ToObject<bool>())
					.First();

				JObject asset = latestRelease.GetValue("assets")
					.ToObject<JArray>()
					.Select(t => t.ToObject<JObject>())
					.Where(obj => obj.TryGetValue("name", out JToken token) && token.ToObject<string>().Contains("x64"))
					.First();

				bepinexDownload = asset.GetValue("browser_download_url").ToObject<string>();

			} catch (Exception) { }

			string tempFile = Path.GetTempFileName();
			string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

			Directory.CreateDirectory(tempPath);

			using WebClient client = new();
			client.Headers.Add(UserAgent);
			client.DownloadFile(bepinexDownload, tempFile);

			ZipFile.ExtractToDirectory(tempFile, tempPath);

			CopyDir(tempPath, path);	
		}

		public string Install(string path)
		{
			if (!File.Exists(Path.Combine(path, "BepInEx", "core", "BepInEx.dll")))
			{
				InstallBepInEx(path);
			}

			bool ignoreVersionCheck = false;
			bool updateAvailable = CheckForUpdate(path);

			if (GetInstalledVersion(path) == null)
				ignoreVersionCheck = true;

			if (!updateAvailable && !ignoreVersionCheck)
				return null;

			string tempFile = Path.GetTempFileName();
			string filePath = Path.Combine(path, "BepInEx", "plugins", FileName);

			using WebClient client = new();
			client.DownloadFile(DownloadUrl, tempFile);

			Directory.CreateDirectory(Path.GetDirectoryName(filePath));

			if (File.Exists(filePath))
				File.Delete(filePath);

			File.Copy(tempFile, filePath);

			return updateAvailable ? "updated" : "installed";
		}

		private static void DeleteFile(string basePath, string file)
		{
			file = Path.Combine(basePath, file);

			if (File.Exists(file))
				File.Delete(file);
		}

		private static void DeleteDirectory(string basePath, string directory, bool recursive)
		{
			directory = Path.Combine(basePath, directory);

			if (Directory.Exists(directory))
				Directory.Delete(directory, recursive);
		}

		public static void CopyDir(string sourceFolder, string destFolder)
		{
			if (!Directory.Exists(destFolder))
				Directory.CreateDirectory(destFolder);

			// Get Files & Copy
			string[] files = Directory.GetFiles(sourceFolder);
			foreach (string file in files)
			{
				string name = Path.GetFileName(file);

				string dest = Path.Combine(destFolder, name);
				File.Copy(file, dest, true);
			}

			// Get dirs recursively and copy files
			string[] folders = Directory.GetDirectories(sourceFolder);
			foreach (string folder in folders)
			{
				string name = Path.GetFileName(folder);
				string dest = Path.Combine(destFolder, name);
				CopyDir(folder, dest);
			}
		}

		public void Uninstall(string path)
		{
			if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
				return;

			DeleteDirectory(path, "BepInEx", true);

			DeleteFile(path, "changelog.txt");
			DeleteFile(path, "winhttp.dll");
			DeleteFile(path, "doorstop_config.ini");
		}

		public Version GetLatestVersion()
		{
			if (cachedVersion != null)
                return cachedVersion;

			try
			{
				using WebClient client = new();

				// Some random user agent because with others it responds with 403
				client.Headers.Add(UserAgent);
				string json = client.DownloadString(ApiUrl);

				JArray jArr = JArray.Parse(json);

				// Get version tag
				string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();

				return cachedVersion = new(stringVersion);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public Version GetInstalledVersion(string path)
		{
			string filePath = Path.Combine(path, "BepInEx", "plugins", FileName);

			if (string.IsNullOrEmpty(path) || !File.Exists(filePath))
				return null;

			var ver = AssemblyName.GetAssemblyName(filePath).Version;

			return ver;
		}

		public bool CheckForUpdate(string path)
		{
			Version git = GetLatestVersion();

			if (git == null)
				return false;

			Version current = GetInstalledVersion(path);

			if (current == null)
				return false;

			int result = current.CompareTo(git);

			return result < 0;
		}
	}
}
