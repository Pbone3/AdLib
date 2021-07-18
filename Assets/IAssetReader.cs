namespace AdLib.Assets
{
    public interface IAssetReader<T> where T : class
    {
        public T GetDefaultValue();

        public T Load(string path, AssetManager manager);

        public bool TryLoad(string path, AssetManager manager, out T asset)
        {
            asset = Load(path, manager);

            if (asset == GetDefaultValue())
                return false;

            return true;
        }
    }
}
