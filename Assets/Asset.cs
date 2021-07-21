using System;

namespace AdLib.Assets
{
    public class Asset<T> : IDisposable
    {
        public bool Loaded => value.Equals(Loader.GetAssetReader<T>().GetDefaultValue());
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
        private bool disposed = false;

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

        public void Dispose()
        {
            if (Value is IDisposable disposable)
                disposable.Dispose();

            disposed = true;
        }

        ~Asset()
        {
            if (!disposed)
                Dispose();
        }

        public static implicit operator T(Asset<T> instance) => instance.Value;
    }
}
