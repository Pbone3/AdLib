using AdLib.Assets;
using AdLib.Audio.Reading.SoundFileReaders;
using AdLib.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdLib.Audio.Reading
{
    public class SoundReaderManager
    {
        public Dictionary<string, ISoundFileReader> Readers;

        public SoundReaderManager(bool registerDefaultReaders = true)
        {
            Readers = new Dictionary<string, ISoundFileReader>();

            if (registerDefaultReaders)
            {
                AddReader<WAVReader>();
            }
        }

        public void AddReader<T>() where T : ISoundFileReader, new() => AddReader(new T());
        public void AddReader(ISoundFileReader instance) => Readers.Add(instance.Extension, instance);

        public CachedSoundData LoadCachedSound(Identifier id, string path) => LoadCachedSound(id, path, path.Split('.').Last());

        public CachedSoundData LoadCachedSound(Identifier id, string path, string ext)
        {
            if (!Readers.TryGetValue(ext.ToLower(), out ISoundFileReader reader))
                throw new AssetLoadingException("Cached Sound", path, new Exception("Reader for " + ext.ToLower() + " files doesn't exist."));

            return reader.LoadCached(id, path);
        }

        public StreamedSoundData LoadStreamedSound(Identifier id, string path) => LoadStreamedSound(id, path, path.Split('.').Last());

        public StreamedSoundData LoadStreamedSound(Identifier id, string path, string ext)
        {
            if (!Readers.TryGetValue(ext.ToLower(), out ISoundFileReader reader))
                throw new AssetLoadingException("Streamed Sound", path, new Exception("Reader for " + ext.ToLower() + " files doesn't exist."));

            reader.GetDetailsForStreamedSound(out int sampleRate, out AudioChannels channels);
            return new StreamedSoundData(id, path, sampleRate, channels, reader.DataStartOffset, this, ext);
        }

        public byte[] GetMoreSamples(ref StreamedSoundData data, Stream stream) => GetMoreSamples(data.Extension, ref data, stream);

        public byte[] GetMoreSamples(string ext, ref StreamedSoundData data, Stream stream)
        {
            // This should never throw
            ISoundFileReader reader = Readers[ext.ToLower()];

            return reader.GetMoreStreamedSamples(ref data, stream);
        }
    }
}
