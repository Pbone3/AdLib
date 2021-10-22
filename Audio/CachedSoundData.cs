using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace AdLib.Audio
{
    public struct CachedSoundData
    {
        public Identifier Id;
        public int SampleRate;
        public byte[] Data;
        public AudioChannels Channels;

        public CachedSoundData(Identifier id, int sampleRate, byte[] data, AudioChannels channels)
        {
            SampleRate = sampleRate;
            Data = data;
            Channels = channels;
            Id = id;
        }
    }
}
