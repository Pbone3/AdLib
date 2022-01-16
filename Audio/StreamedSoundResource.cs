using AdLib.Audio.Reading;
using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;

namespace AdLib.Audio
{
    public class StreamedSoundResource : ISoundResource, IDisposable
    {
        public Identifier Id { get => Data.Id; set => Data.Id = value; }
        public int SampleRate { get => Data.SampleRate; set => Data.SampleRate = value; }
        public AudioChannels Channels { get => Data.Channels; set => Data.Channels = value; }

        public StreamedSoundData Data;
        public Stream Stream;
        public SoundReaderManager SoundReaderManager;

        public StreamedSoundResource(StreamedSoundData data)
        {
            Data = data;
            SoundReaderManager = data.SoundReaderManager;

            Stream = File.OpenRead(data.Path);
        }

        public byte[] GetMoreSamples() => SoundReaderManager.GetMoreSamples(ref Data, Stream);

        public void Dispose()
        {
            Stream.Dispose();
            GC.SuppressFinalize(this);
        }

        ~StreamedSoundResource()
        {
            Dispose();
        }
    }
}
