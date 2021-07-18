using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AdLib.Assets.AssetReaders
{
    public class Texture2DReader : IAssetReader<Texture2D>, IDisposable
    {
        public Texture2D Dummy = new Texture2D(Main.Instance.GraphicsDevice, 0, 0);

        public Texture2D GetDefaultValue() => Dummy;

        private bool disposed = false;

        public Texture2D Load(string path)
        {
            Texture2D tex = Dummy;

            using (FileStream stream = File.OpenRead(path))
            {
                try
                {
                    tex = Texture2D.FromStream(Main.Instance.GraphicsDevice, stream);
                }
                catch (Exception e)
                {
                    throw new Exception("Issue occured while loading Texture2D from " + path + ": " + e.ToString());
                }
            }

            return tex;
        }

        public void Dispose()
        {
            Dummy.Dispose();
            disposed = true;
        }

        ~Texture2DReader()
        {
            if (!disposed)
                Dispose();
        }
    }
}
