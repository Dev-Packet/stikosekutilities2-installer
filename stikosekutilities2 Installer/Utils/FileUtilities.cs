using Ookii.Dialogs.WinForms;
using System.IO;
using System.Windows.Forms;

namespace stikosekutilities2_Installer.Utils
{
    public static class FileUtilities
    {

        private static string BrowseGame()
        {
            // Create BrowseFolder Dialog
            VistaFolderBrowserDialog dialog = new()
            {
                Description = "Select Muck folder",
                ShowNewFolderButton = false
            };

            // Show dialog and return Path
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }

            return null;
        }

        public static string FindGameLocation(bool enableBrowse = true)
        {
            // Try to get the Game Folder through steam
            string location = SteamUtils.GetAppLocation(1782210, "Muck");

            // Fallback to browse the folder
            if ((string.IsNullOrEmpty(location) || !File.Exists(Path.Combine(location, "Muck.exe"))) && enableBrowse)
            {
                location = BrowseGame();
            }

            return location;
        }

        public static void SafeDeleteFile(string basePath, string path)
        {
            path = Path.Combine(basePath, path);
            if (File.Exists(path))
                File.Delete(path);
        }

        public static void SafeDeleteDirectory(string basePath, string directory, bool recursive)
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

    }
}
