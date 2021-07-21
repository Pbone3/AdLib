using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AdLib.Helpers
{
    public static class AssemblyFixerUpper
    {
        public static byte[] GetFNALib(string fnalib)
        {
            string path = Path.Combine(IOHelper.GetBaseDirectory(), "FNALibs", GetFNALibsKeyword(), fnalib + ".dll");
            return File.ReadAllBytes(path);
        }

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
