using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace AdLib.Audio.Reading
{
    public interface ISoundFileReader
    {
        public string Extension { get; }
        public int DataStartOffset { get; }

        public CachedSoundData LoadCached(Identifier id, string path);

        public int GetDetailsForStreamedSound(out int sampleRate, out AudioChannels channels);
        public byte[] GetMoreStreamedSamples(ref StreamedSoundData data, Stream stream);
    }
}
