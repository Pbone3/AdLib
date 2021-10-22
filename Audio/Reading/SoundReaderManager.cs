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

        public CachedSoundData LoadCachedSound(string path) => LoadCachedSound(path, path.Split('.').Last());

        public CachedSoundData LoadCachedSound(string path, string ext)
        {
            if (!Readers.TryGetValue(ext.ToLower(), out ISoundFileReader reader))
                throw new AssetLoadingException("Cached Sound", path, new Exception("Reader for " + ext.ToLower() + "files doesn't exist."));

            return reader.LoadCached(path);
        }

        public StreamedSoundData LoadStreamedSound(string path) => LoadStreamedSound(path, path.Split('.').Last());

        public StreamedSoundData LoadStreamedSound(string path, string ext)
        {
            if (!Readers.TryGetValue(ext.ToLower(), out ISoundFileReader reader))
                throw new AssetLoadingException("Streamed Sound", path, new Exception("Reader for " + ext.ToLower() + "files doesn't exist."));

            // TODO DEFAULT IDENTIFIER
            reader.GetDetailsForStreamedSound(out int sampleRate, out AudioChannels channels);
            return new StreamedSoundData(Identifier.Default, path, sampleRate, channels, reader.DataStartOffset, this, ext);
        }

        public byte[] GetMoreSamples(string ext, int offset, Stream stream)
        {
            // This should never throw
            ISoundFileReader reader = Readers[ext.ToLower()];

            return reader.GetMoreStreamedSamples(offset, stream);
        }
    }
}
