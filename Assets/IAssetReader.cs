namespace AdLib.Assets
{
    public interface IAssetReader<T> where T : class
    {
        public T GetDefaultValue();

        public T Load( string path);

        public bool TryLoad(string path, out T asset)
        {
            asset = Load(path);

            if (asset == GetDefaultValue())
                return false;

            return true;
        }
    }
}
