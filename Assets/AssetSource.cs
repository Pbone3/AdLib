using System.IO;

namespace AdLib.Assets
{
    public class AssetSource
    {
        public bool HasOverride => OverrideFolder != null;

        public string BaseFolder;
        public string OverrideFolder;

        public AssetSource(string baseFolder, string overrideFolder = null)
        {
            BaseFolder = baseFolder;
            OverrideFolder = overrideFolder;
        }

        public string GetBaseFolder() => Path.Combine(GameInfo.GetContentPath(), BaseFolder);
        public string GetOverrideFolder() => Path.Combine(GameInfo.GetContentPath(), ".overrides", OverrideFolder);

        public bool AssetExists(string path) => File.Exists(GetAssetPath(path));
        // This should return the absolute path (C:/blahblahblah/asset.png)
        public string GetAssetPath(string path)
        {
            string folder;
            if (HasOverride && AssetOverrideExists(path))
                folder = GetOverrideFolder();
            else
                folder = GetBaseFolder();

            return Path.Combine(folder, path);
        }

        public bool AssetOverrideExists(string path) => File.Exists(Path.Combine(GetOverrideFolder(), path));
    }
}
