using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MminoWindows.Module
{
    internal static class GlobalPath
    {
        public const string UPDATE_FOLDER = "Update";
        public static string CurrentDirectory = Environment.CurrentDirectory;

        public static string UpdateDirectory = Path.Combine("../", UPDATE_FOLDER);

        static GlobalPath()
        {
            CreateDirectory(UpdateDirectory);
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
