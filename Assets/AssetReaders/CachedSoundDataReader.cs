using AdLib.Audio;
using AdLib.Audio.Reading;
using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AdLib.Assets.AssetReaders
{
    public class CachedSoundDataReader : IAssetReader<CachedSoundData>
    {
        public SoundReaderManager SoundReaderManager;
        public CachedSoundData Dummy;

        public CachedSoundData GetDefaultValue() => Dummy;

        public CachedSoundDataReader(SoundReaderManager soundReaderManager)
        {
            SoundReaderManager = soundReaderManager;
            Dummy = new CachedSoundData(Identifier.Default, 0, Array.Empty<byte>(), AudioChannels.Mono); ;
        }

        public CachedSoundData Load(string path, AssetManager manager)
        {
            CachedSoundData data = GetDefaultValue();

            try
            {
                data = SoundReaderManager.LoadCachedSound(path);
            }
            catch (Exception e)
            {
                throw new AssetLoadingException(nameof(CachedSoundData), path, e);
            }

            return data;
        }
    }
}
