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

        public CachedSoundData LoadCached(string path)
        {
            CachedSoundData data = new CachedSoundData();
            data.Id = Identifier.Default; // TODO DEFAULT IDENTIFIER

            using (FileStream stream = File.OpenRead(path))
                IOHelper.ReadWavFileFromStream(stream, out data.SampleRate, out data.Data, out data.Channels);

            return data;
        }


        public int GetDetailsForStreamedSound(out int sampleRate, out AudioChannels channels)
        {
            // TODO THIS
            throw new System.NotImplementedException();
        }

        public byte[] GetMoreStreamedSamples(int offset, Stream stream)
        {
            // TODO
            int length = 4; // ACTUALLY CALCULATE LENGTH PLS, SHOULD BE THE SAMPLE RATE BUT TEST IT ANYWAYS
            byte[] data = new byte[length]; 
            stream.Read(data, offset, length); // THIS IS TOTALLY FALSE AND JUST A PLACEHOLDER

            return data;
        }
    }
}
