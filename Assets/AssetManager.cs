using AdLib.DataStructures;

namespace AdLib.Assets
{
    /* AssetManager distributes AssetLoader instances and actually loads Assets
     * AssetLoader instances are basically a buffer between calling for an Asset load and actually loading it,
     * and do things like turn a part of the path ("Items/IronSword") to a full path ("C:/BlahBlahBlah/Etc/Content/Vanilla/Items/IronSword")
     */
    public class AssetManager
    {
        public class AssetReaderRegistry : TypeRegistry<object>
        {
            public IAssetReader<T> GetAssetReader<T>() where T : class => Get<T>() as IAssetReader<T>;
        }

        public AssetReaderRegistry ReaderRegistry;

        public AssetManager()
        {
            ReaderRegistry = new AssetReaderRegistry();
        }

        public AssetLoader GetLoader(AssetSource source) => new AssetLoader(this, source);

        public bool TryLoadAsset<T>(string path, out T asset) where T : class => GetAssetReader<T>().TryLoad(path, out asset);
        public T LoadAsset<T>(string path) where T : class => GetAssetReader<T>().Load(path);

        public IAssetReader<T> GetAssetReader<T>() where T : class => ReaderRegistry.GetAssetReader<T>();
    }
}
