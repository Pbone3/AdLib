using System;

namespace AdLib.Assets
{
    public class Asset<T> where T : class
    {
        public bool Loaded => value != Loader.GetAssetReader<T>().GetDefaultValue();
        public T Value
        {
            get {
                if (!Loaded)
                    Load();

                return value;
            }
        }

        public AssetLoader Loader;
        public string Path;

        private T value;

        public Asset(AssetLoader loader, string path, bool preLoad = false)
        {
            Loader = loader;
            Path = path;

            value = Loader.GetAssetReader<T>().GetDefaultValue();

            if (preLoad)
                Load();
        }

        public void Load()
        {
            if (!Loader.Source.AssetExists(Path))
                throw new Exception("Asset at " + Loader.Source.GetAssetPath(Path) + " does not exist.");

            value = Loader.LoadAsset<T>(Path);
        }

        public static implicit operator T(Asset<T> instance) => instance.Value;
    }
}
