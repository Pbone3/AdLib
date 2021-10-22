using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace AdLib.Audio
{
    public interface ISoundResource
    {
        public Identifier Id { get; set; }

        public int SampleRate { get; set; }
        public AudioChannels Channels { get; set; }

        public byte[] GetMoreSamples();
    }
}
