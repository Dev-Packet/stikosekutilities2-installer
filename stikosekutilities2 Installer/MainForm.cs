using Newtonsoft.Json.Linq;
using stikosekutilities2_Installer.Utils;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace stikosekutilities2_Installer
{
    public partial class MainForm : Form
    {
        #region Variables
        public const string
            RepoOwner = "DevPacket",
            Repository = "stikosekutilities2",
            DownloadURL = $"https://github.com/{RepoOwner}/{Repository}/releases/latest/download/stikosekutilities2.dll",
            ReleasesAPI = $"https://api.github.com/repos/{RepoOwner}/{Repository}/releases",
            BepInExURL = "https://github.com/BepInEx/BepInEx/releases/download/v5.4.17/BepInEx_x64_5.4.17.0.zip";

        private const string
            OriginalLatestVersion = "Latest Version:      {version}",
            OriginalCurrentVersion = "Current Version:   {version}";

        private string
            latestVersion,
            currentVersion;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(Paths.PluginFile))
                currentVersion = AssemblyName.GetAssemblyName(Paths.PluginFile).Version.ToString();

            ChangeButton();

            latestVersionLabel.Text = OriginalLatestVersion.Replace("{version}", latestVersion ?? "Not found");
            currentVersionLabel.Text = OriginalCurrentVersion.Replace("{version}", currentVersion ?? "None");
        }

        private void ChangeButton()
        {
            if (UpdateAvailable())
            {
                mainButton.Text = "Update";
                return;
            }

            mainButton.Enabled = !IsModInstalled();

            if (!mainButton.Enabled)
            {
                mainButton.Text = "Installed";
                mainButton.Cursor = Cursors.No;
            }
            else
            {
                mainButton.Text = "Install";
                mainButton.Cursor = Cursors.Hand;
            }
        }

        private bool UpdateAvailable()
        {
            try
            {
                string plugin = Paths.PluginFile;

                if (!File.Exists(plugin))
                    return false;

                using var client = new WebClient();
                // Some random user agent because with others it responds with 403
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
                string json = client.DownloadString(ReleasesAPI);

                JArray jArr = JArray.Parse(json);

                latestVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();

                // Compare GitHub and Local Version
                Version git = new(latestVersion);

                Version current = new(currentVersion);

                int result = current.CompareTo(git);

                return result < 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void DownloadFile(string url, string file)
        {
            using var client = new WebClient();

            byte[] rawFile = client.DownloadData(url);

            Directory.CreateDirectory(Path.GetDirectoryName(file));

            File.WriteAllBytes(file, rawFile);
        }

        private void DownloadMod() => DownloadFile(DownloadURL, Paths.PluginFile);

        private void InstallBepInEx()
        {
            // File variables
            string
                tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()),
                zipExtract = Path.Combine(tempFolder, "Extracted"),
                zipFile = Path.Combine(tempFolder + "BepInEx.zip");

            // Create TempFolders
            Directory.CreateDirectory(tempFolder);
            Directory.CreateDirectory(zipExtract);

            // Download BepInEx
            DownloadFile(BepInExURL, zipFile);

            // Extract BepInEx
            ZipFile.ExtractToDirectory(zipFile, zipExtract);

            // Copy BepInEx to Game Folder
            FileUtilities.CopyDir(zipExtract, Paths.Path);
        }

        private bool IsModInstalled() => File.Exists(Paths.PluginFile);

        private bool IsBepInExInstalled() => File.Exists(Path.Combine(Paths.Path, "BepInEx", "Core", "BepInEx.IL2CPP.dll"));

        private void mainButton_Click(object sender, EventArgs e)
        {
            if (!IsBepInExInstalled())
            {
                InstallBepInEx();
            }

            var updateAvailable = UpdateAvailable();

            if (updateAvailable || !IsModInstalled())
            {
                DownloadMod();

                MessageBox.Show(null, "stikosekutilities2 " + (updateAvailable ? "Updated" : "Installed") + " sucessfully!",
                    "Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ChangeButton();
        }
    }
}