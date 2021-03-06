using stikosekutilities2_Installer.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace stikosekutilities2_Installer
{
    public partial class MainForm : Form
    {
        #region Variables
        private const string
            OriginalLatestVersion = "Latest Version:      {version}",
            OriginalCurrentVersion = "Current Version:   {version}";

        private readonly List<StikosekUtilities> suVersions;

        private StikosekUtilities selectedVersion;

        private string muckPath;
        private bool lockButton;
        #endregion

        public MainForm()
        {
            InitializeComponent();

            ToolTip.SetToolTip(uninstallButton, "Uninstall the current version of StikosekUtilities along with the modloader");

            muckPath = FileUtilities.FindGameLocation(false);

            #region Populate Versions
            suVersions = new List<StikosekUtilities>
            {
                new StikosekUtilities()
                {
                    EnableAutoUpdate = false,
                    FileName = "stikosekutilitites.dll",
                    Name = "StikosekUtilities(Remastered)",
                    Owner = "stikosek",
                    Repo = "stikosekutilities",
                    Version = SuVersion.Su_Remastered
                },
                new StikosekUtilities()
                {
                    EnableAutoUpdate = true,
                    FileName = "stikosekutilities2.dll",
                    Name = "StikosekUtilities2",
                    Owner = "Dev-Packet",
                    Repo = "stikosekutilities2",
                    Version = SuVersion.Su2
                }
            };

            versionSelectionBox.Items.Clear();
            foreach(string versionString in suVersions.Select(ver => ver.Name))
            {
                versionSelectionBox.Items.Add(versionString);
            }
            #endregion
            versionSelectionBox.SelectedIndex = 0;

            UpdateTexts();
        }

        private void VersionSelectionBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            selectedVersion = suVersions[versionSelectionBox.SelectedIndex];
            UpdateTexts();
        }

        #region Button Updating
        private void ResizeButtons(bool showUninstall)
        {
            uninstallButton.Visible = showUninstall;

            if(!showUninstall)
                // Bigger Button
                mainButton.Size = new Size(225, 41);
            else
                // Smaller Button
                mainButton.Size = new Size(168, 41);
            
            mainButton.Location = new Point(298, 95);
        }

        private void UpdateTexts()
        {

            string
                latestVersion = "",
                currentVersion = "";

            bool
                installed = false,
                updateAvailable = false;

            if(!selectedVersion.EnableAutoUpdate)
            {
                if(selectedVersion.Version == SuVersion.Su_Remastered)
                {
                    latestVersion = "1.2";
                    currentVersion = latestVersion;

                    installed = selectedVersion.GetInstalledVersion(muckPath) != null;
                }
            } else
            {
                Version ver;
                ver = selectedVersion.GetLatestVersion();

                if (ver != null)
                    latestVersion = ver.ToString();

                ver = selectedVersion.GetInstalledVersion(muckPath);

                if(ver != null)
                { 
                    currentVersion = ver.ToString();
                    installed = true;
                }

                updateAvailable = selectedVersion.CheckForUpdate(muckPath);
            }

            latestVersionLabel.Text = OriginalLatestVersion.Replace("{version}", string.IsNullOrEmpty(latestVersion) ? "Not found" : latestVersion);
            currentVersionLabel.Text = OriginalCurrentVersion.Replace("{version}", string.IsNullOrEmpty(currentVersion) ? "None" : currentVersion);

            if(updateAvailable)
            {
                mainButton.Text = "Update";
                lockButton = false;
                mainButton.Cursor = Cursors.Hand;
                ResizeButtons(true);
                return;
            }

            if(installed)
            {
                mainButton.Text = "Installed";
                lockButton = true;
                mainButton.Cursor = Cursors.No;
                ResizeButtons(true);
            } else
            {
                mainButton.Text = "Install";
                lockButton = false;
                mainButton.Cursor = Cursors.Hand;
                ResizeButtons(false);
            }

        }
        #endregion

        #region Install/Uninstall
        private void MainButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(muckPath))
                muckPath = FileUtilities.FindGameLocation();

            if (string.IsNullOrEmpty(muckPath))
                return;

            if (lockButton)
            {
                UpdateTexts();
                return;
            }

            selectedVersion.Install(muckPath);
            UpdateTexts();
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(muckPath))
                muckPath = FileUtilities.FindGameLocation();

            if (string.IsNullOrEmpty(muckPath))
                return;

            selectedVersion.Uninstall(muckPath);
            UpdateTexts();
        }
        #endregion

    }
}