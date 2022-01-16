using AdLib.DataStructures;
using AdLib.Helpers;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace AdLib.Audio.Reading.SoundFileReaders
{
    public class WAVReader : ISoundFileReader
    {
        public string Extension => "wav";

        public int DataStartOffset => 44 * 8; // 44 Bytes

        public CachedSoundData LoadCached(Identifier id, string path)
        {
            CachedSoundData data = new CachedSoundData();
            data.Id = id; // TODO DEFAULT IDENTIFIER

            using (FileStream stream = File.OpenRead(path))
                IOHelper.ReadWavFileFromStream(stream, out data.SampleRate, out data.Data, out data.Channels);

            return data;
        }


        public int GetDetailsForStreamedSound(out int sampleRate, out AudioChannels channels)
        {
            // TODO THIS
            throw new System.NotImplementedException();
        }

        public byte[] GetMoreStreamedSamples(ref StreamedSoundData data, Stream stream)
        {
            // TODO
            int length = data.Offset;
            byte[] raw = new byte[length]; 
            stream.Read(raw, data.Offset, length); // THIS IS TOTALLY FALSE AND JUST A PLACEHOLDER

            data.Offset += data.SampleRate;

            return raw;
        }
    }
}
