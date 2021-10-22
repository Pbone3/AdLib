using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AdLib.Helpers
{
    [Obsolete]
    public static class AssemblyFixerUpper
    {
        public static string GetFNALibsPath(string fnalib) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FNALibs", GetFNALibsKeyword(), fnalib + ".dll");

        public static string GetFNALibsKeyword()
        {
            string osId = null;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (Environment.Is64BitProcess)
                    osId = "x64";
                else
                    osId = "x86";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                osId = "osx";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                osId = "libx64";
            }

            return osId;
        }
    }
}
