using stikosekutilities2_Installer.Utils;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace stikosekutilities2_Installer
{
    public static class BepInEx
    {

        private const string Url = "https://github.com/BepInEx/BepInEx/releases/download/v5.4.19/BepInEx_x64_5.4.19.0.zip";

        public static void InstallBepInEx(string path)
        {
            string tempFile = Path.GetTempFileName();
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempPath);

            using WebClient client = new();
            client.DownloadFile(Url, tempFile);

            ZipFile.ExtractToDirectory(tempFile, tempPath);

            FileUtilities.CopyDir(tempPath, path);
        }

    }
}
