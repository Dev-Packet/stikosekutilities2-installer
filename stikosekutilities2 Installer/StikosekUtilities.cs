using Newtonsoft.Json.Linq;
using stikosekutilities2_Installer.Utils;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace stikosekutilities2_Installer
{
    public class StikosekUtilities
    {
        public string Owner;
        public string Repo;
        public string FileName;

        public bool EnableAutoUpdate = true;

        public string ApiUrl => $"https://api.github.com/repos/{Owner}/{Repo}/releases";
        public string DownloadUrl => $"https://github.com/{Owner}/{Repo}/releases/latest/download/{FileName}";

        public string Name;
        public SuVersion Version;


        public void Install(string path)
        {
            if (!File.Exists(Path.Combine(path, "BepInEx", "core", "BepInEx.dll")))
            {
                BepInEx.InstallBepInEx(path);
            }

            bool ignoreVersionCheck = false;
            bool updateAvailable = CheckForUpdate(path);

            if (GetInstalledVersion(path) == null)
                ignoreVersionCheck = true;

            if (!updateAvailable && !ignoreVersionCheck)
                return;

            string tempFile = Path.GetTempFileName();
            string filePath = Path.Combine(path, "BepInEx", "plugins", FileName);

            using WebClient client = new();
            client.DownloadFile(DownloadUrl, tempFile);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            File.Copy(tempFile, filePath);

            string text = updateAvailable ? "Updated" : "Installed";

            MessageBox.Show(null, $"{Name} Version: {GetLatestVersion()} was {text} successfully!",
                $"{text} successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Uninstall(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return;

            FileUtilities.SafeDeleteDirectory(path, "BepInEx", true);

            FileUtilities.SafeDeleteFile(path, "changelog.txt");
            FileUtilities.SafeDeleteFile(path, "winhttp.dll");
            FileUtilities.SafeDeleteFile(path, "doorstop_config.ini");


            MessageBox.Show(null, $"{Name} was uninstalled successfully!",
                "Uninstalled successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Version GetLatestVersion()
        {
            try
            {
                using WebClient client = new();

                // Some random user agent because with others it responds with 403
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
                string json = client.DownloadString(ApiUrl);

                JArray jArr = JArray.Parse(json);

                // Get version tag
                string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();

                return new(stringVersion);
            } catch(Exception)
            {
                return null;
            }
        }

        public Version GetInstalledVersion(string path)
        {
            string filePath = Path.Combine(path, "BepInEx", "plugins", FileName);

            if (string.IsNullOrEmpty(path) || !File.Exists(filePath))
                return null;

            return AssemblyName.GetAssemblyName(filePath).Version;
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
