using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace AdLib.Audio.Reading
{
    public interface ISoundFileReader
    {
        public string Extension { get; }
        public int DataStartOffset { get; }

        public CachedSoundData LoadCached(string path);

        public int GetDetailsForStreamedSound(out int sampleRate, out AudioChannels channels);
        public byte[] GetMoreStreamedSamples(int offset, Stream stream);
    }
}
