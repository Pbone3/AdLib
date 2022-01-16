using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace AdLib.Audio
{
    public class CachedSoundResource : ISoundResource
    {
        public Identifier Id { get => Data.Id; set => Data.Id = value; }
        public int SampleRate { get => Data.SampleRate; set => Data.SampleRate = value; }
        public AudioChannels Channels { get => Data.Channels; set => Data.Channels = value; }

        public CachedSoundData Data;
        public byte[] RawData;

        public byte[] GetMoreSamples() => RawData;

        public CachedSoundResource(CachedSoundData data)
        {
            Id = data.Id;
            SampleRate = data.SampleRate;
            RawData = data.Data;
            Channels = data.Channels;
        }
    }
}
