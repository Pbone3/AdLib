using System;
using System.IO;

namespace AdLib.IO.Logging
{
    public class Log
    {
        public string ArchivePath => Path.Combine(LogPath, "Archive");
        public string OldLogPath => LogPath.Replace(".txt", ".old.txt");
        public string LogPath;

        public Log(string path, string name, bool archive = true)
        {
            LogPath = Path.Combine(path, name);

            if (archive && File.Exists(LogPath))
            {
                string oldLogText = File.ReadAllText(LogPath);

                File.WriteAllText(OldLogPath, oldLogText);
            }

            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(path);

            File.WriteAllText(LogPath, "");
        }

        public void WriteLine(string message, string type, bool newLine = true) =>
            File.AppendAllText(LogPath, 
                (newLine ? "\n" : "") + $"[{DateTime.Now:T}] [{type}]: {message}");

        // NewLine-less overloads for FNALoggerEXT
        public void WriteInfo(string message) => WriteLine(message, "INFO");
        public void WriteWarning(string message) => WriteLine(message, "WARNING");
        public void WriteError(string message) => WriteLine(message, "ERROR");

        public void WriteInfo(string message, bool newLine = true) => WriteLine(message, "INFO", newLine);
        public void WriteWarning(string message, bool newLine = true) => WriteLine(message, "WARNING", newLine);
        public void WriteError(string message, bool newLine = true) => WriteLine(message, "ERROR", newLine);
    }
}
