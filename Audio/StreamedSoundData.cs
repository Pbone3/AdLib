using AdLib.Audio.Reading;
using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace AdLib.Audio
{
    public struct StreamedSoundData
    {
        public Identifier Id;
        public string Path;
        public int SampleRate;
        public AudioChannels Channels;
        public int Offset;

        public SoundReaderManager SoundReaderManager;
        public string Extension;

        public StreamedSoundData(Identifier id, string path, int sampleRate, AudioChannels channels, int offset, SoundReaderManager soundReaderManager, string extension)
        {
            Id = id;
            Path = path;
            SampleRate = sampleRate;
            Channels = channels;
            Offset = offset;

            SoundReaderManager = soundReaderManager;
            Extension = extension;
        }
    }
}
