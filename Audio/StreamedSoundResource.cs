using AdLib.Audio.Reading;
using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;

namespace AdLib.Audio
{
    public class StreamedSoundResource : ISoundResource, IDisposable
    {
        public Identifier Id { get; set; }
        public int SampleRate { get; set; }
        public AudioChannels Channels { get; set; }

        public string Extension;
        public int Offset;
        public Stream Stream;
        public SoundReaderManager SoundReaderManager;

        public StreamedSoundResource(Identifier id, string path, int sampleRate, AudioChannels channels, string extension, int offset, SoundReaderManager soundReaderManager)
        {
            Id = id;
            SampleRate = sampleRate;
            Channels = channels;
            Extension = extension;
            Offset = offset;
            SoundReaderManager = soundReaderManager;

            Stream = File.OpenRead(path);
        }

        public StreamedSoundResource(StreamedSoundData data)
        {
            Id = data.Id;
            SampleRate = data.SampleRate;
            Channels = data.Channels;
            Extension = data.Extension;
            Offset = data.Offset;
            SoundReaderManager = data.SoundReaderManager;

            Stream = File.OpenRead(data.Path);
        }

        public byte[] GetMoreSamples() => SoundReaderManager.GetMoreSamples(Extension, Offset, Stream);

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
