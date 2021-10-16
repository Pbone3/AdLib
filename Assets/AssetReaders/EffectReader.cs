using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AdLib.Assets.AssetReaders
{
    public class EffectReader : IAssetReader<Effect>, IDisposable
    {
        public Effect Dummy;

        public Effect GetDefaultValue() => Dummy;

        public EffectReader(GraphicsDevice graphicsDevice)
        {
            Dummy = new Effect(graphicsDevice, Array.Empty<byte>());
        }

        public Effect Load(string path, AssetManager manager)
        {
            Effect tex = GetDefaultValue();

            try
            {
                tex = new Effect(Dummy.GraphicsDevice, File.ReadAllBytes(path));
            }
            catch (Exception e)
            {
                throw new AssetLoadingException(nameof(Texture2D), path, e);
            }

            return tex;
        }

        public void Dispose()
        {
            Dummy.Dispose();
            GC.SuppressFinalize(this);
        }

        ~EffectReader()
        {
            Dispose();
        }
    }
}