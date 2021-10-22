using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace AdLib.Audio
{
    public class CachedSoundResource : ISoundResource
    {
        public Identifier Id { get; set; }
        public int SampleRate { get; set; }
        public AudioChannels Channels { get; set; }

        public byte[] Data;

        public byte[] GetMoreSamples() => Data;

        public CachedSoundResource(Identifier id, int sampleRate, byte[] data, AudioChannels channels)
        {
            Id = id;
            SampleRate = sampleRate;
            Data = data;
            Channels = channels;
        }

        public CachedSoundResource(CachedSoundData data)
        {
            Id = data.Id;
            SampleRate = data.SampleRate;
            Data = data.Data;
            Channels = data.Channels;
        }
    }
}
