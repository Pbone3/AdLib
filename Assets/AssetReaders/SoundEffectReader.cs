using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;

namespace AdLib.Assets.AssetReaders
{
    public class SoundEffectReader : IAssetReader<SoundEffect>, IDisposable
    {
        public SoundEffect Dummy;

        public SoundEffect GetDefaultValue() => Dummy;

        public SoundEffectReader()
        {
            Dummy = new SoundEffect(new byte[0] { }, 1, AudioChannels.Mono);
        }

        public SoundEffect Load(string path, AssetManager manager)
        {
            SoundEffect sfx = GetDefaultValue();

            using (FileStream stream = File.OpenRead(path))
            {
                try
                {
                    sfx = SoundEffect.FromStream(stream);
                }
                catch (Exception e)
                {
                    throw new AssetLoadingException(nameof(SoundEffect), path, e);
                }
            }

            return sfx;
        }

        public void Dispose()
        {
            Dummy.Dispose();
            GC.SuppressFinalize(this);
        }

        ~SoundEffectReader()
        {
            Dispose();
        }
    }
}
