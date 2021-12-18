using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace stikosekutilities2_Installer.Utils
{
    public static class Paths
    {
        private static string
            _path,
            _pluginFile;

        public static string Path
        {
            get
            {
                if (string.IsNullOrEmpty(_path))
                {
                    _path = FileUtilities.FindGameLocation();
                }

                return _path;
            }
        }

        public static string PluginFile
        {
            get
            {

                if (string.IsNullOrEmpty(_pluginFile))
                {
                    string folder = System.IO.Path.Combine(Path, "BepInEx", "plugins");

                    try
                    {
                        _pluginFile = Directory.
                            GetFiles(folder).
                            Where(f => AssemblyName.GetAssemblyName(f).Equals("stikosekutilities2")).
                            First();
                    }
                    catch (Exception)
                    {
                        Directory.CreateDirectory(folder);

                        _pluginFile = System.IO.Path.Combine(folder, "stikosekutilities2.dll");
                    }

                }

                return _pluginFile;
            }
        }

    }
}
