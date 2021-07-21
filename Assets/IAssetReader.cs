namespace AdLib.Assets
{
    public interface IAssetReader<T>
    {
        public T GetDefaultValue();

        public T Load(string path, AssetManager manager);

        public bool TryLoad(string path, AssetManager manager, out T asset)
        {
            asset = Load(path, manager);

            if (asset.Equals(GetDefaultValue()))
                return false;

            return true;
        }
    }
}
