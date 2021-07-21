using AdLib.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AdLib.Assets
{
    /* AssetManager distributes AssetLoader instances and actually loads Assets
     * AssetLoader instances are basically a buffer between calling for an Asset load and actually loading it,
     * and do things like turn a part of the path ("Items/IronSword") to a full path ("C:/BlahBlahBlah/Etc/Content/Vanilla/Items/IronSword")
     */
    public class AssetManager : IDisposable
    {
        public class AssetReaderRegistry : TypeRegistry<object>
        {
            public IAssetReader<T> GetAssetReader<T>() => Get<T>() as IAssetReader<T>;
        }

        public AssetReaderRegistry ReaderRegistry;
        public GraphicsDevice GraphicsDevice;

        public AssetManager(GraphicsDevice graphicsDevice)
        {
            ReaderRegistry = new AssetReaderRegistry();
            GraphicsDevice = graphicsDevice;
        }

        public AssetLoader GetLoader(AssetSource source) => new AssetLoader(this, source);

        public bool TryLoadAsset<T>(string path, out T asset) => GetAssetReader<T>().TryLoad(path, this, out asset);
        public T LoadAsset<T>(string path) => GetAssetReader<T>().Load(path, this);

        public IAssetReader<T> GetAssetReader<T>()  => ReaderRegistry.GetAssetReader<T>();

        public void Dispose()
        {
            foreach (KeyValuePair<Type, object> kvp in ReaderRegistry)
            {
                if (kvp.Value is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
