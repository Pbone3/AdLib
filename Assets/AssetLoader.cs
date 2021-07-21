namespace AdLib.Assets
{
    public class AssetLoader
    {
        public AssetManager Manager;
        public AssetSource Source;

        public AssetLoader(AssetManager manager, AssetSource source)
        {
            Manager = manager;
            Source = source;
        }

        // Getting assets doesn't use the full path, loading them does
        public Asset<T> GetAsset<T>(string path)=> new Asset<T>(this, path);

        public bool TryLoadAsset<T>(string path, out T asset) => Manager.TryLoadAsset(Source.GetAssetPath(path), out asset);
        public T LoadAsset<T>(string path) => Manager.LoadAsset<T>(Source.GetAssetPath(path));

        public IAssetReader<T> GetAssetReader<T>() => Manager.GetAssetReader<T>();
    }
}
