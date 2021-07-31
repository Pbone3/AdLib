using AdLib.IO.Logging;
using System;

namespace AdLib
{
    public static class GameInfo
    {
        public static Func<string> GetContentPath;
        public static Func<Log> GetLog;

        public static int VirtualWidth;
        public static int VirtualHeight;
    }
}
