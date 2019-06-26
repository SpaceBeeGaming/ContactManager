using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerUI
{
    internal static class Utilities
    {
        internal static string AddVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;

        }

        internal static async Task CheckForUpdates()
        {
            using (UpdateManager manager = new UpdateManager(@"C:\Temp\Releases"))
            {
                await manager.UpdateApp();
            }
        }
    }
}
