using AdLib.Audio;
using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AdLib.Assets.AssetReaders
{
    public class CachedSoundDataReader : IAssetReader<CachedSoundData>
    {
        public CachedSoundData GetDefaultValue() => new CachedSoundData(Identifier.Default, 0, Array.Empty<byte>(), AudioChannels.Mono);

        public CachedSoundData Load(string path, AssetManager manager)
        {
            throw new System.NotImplementedException();
        }
    }
}
