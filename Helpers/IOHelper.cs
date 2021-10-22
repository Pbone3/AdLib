using Microsoft.Xna.Framework.Audio;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using Tomlet;
using Tomlet.Exceptions;
using Tomlet.Models;

namespace AdLib.Helpers
{
    public static class IOHelper
    {
		public static Dictionary<string, object> FlattenTomlDocument(TomlDocument document)
        {
			Dictionary<string, object> data = new Dictionary<string, object>();

			foreach (KeyValuePair<string, TomlValue> kvp in document.Entries)
            {
				FlattenTomlValue(kvp.Key, kvp.Value, ref data);
            }

			return data;
        }

		private static Type[] types = new Type[] {
				typeof(string), typeof(bool), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long),
				typeof(double), typeof(long), typeof(double), typeof(float), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan)
			};

		public static void FlattenTomlValue(string key, TomlValue toml, ref Dictionary<string, object> data)
        {
			if (toml is TomlTable table)
			{
				foreach (KeyValuePair<string, TomlValue> kvp in table.Entries)
				{
					// key + "." + kvp.Key is needed to the name of the table is added to the value's key
					FlattenTomlValue(key + "." + kvp.Key, kvp.Value, ref data);
				}

				return;
			}


			// Jump through a few hoops to get the value
			for (int i = 0; i < types.Length; i++)
            {
				Type t = types[i];

                try
                {
					data.Add(key, TomletMain.To(t, toml));
					break;
				}
				catch (TomlTypeMismatchException)
                {
					continue;
                }
            }
		}

		// From https://github.com/FNA-XNA/FNA/blob/master/src/FNAPlatform/SDL2_FNAPlatform.cs
		public static string GetBaseDirectory()
		{
			string osVersion = SDL.SDL_GetPlatform();

			if (Environment.GetEnvironmentVariable("FNA_SDL2_FORCE_BASE_PATH") != "1")
			{
				// If your platform uses a CLR, you want to be in this list!
				if (osVersion.Equals("Windows") ||
					osVersion.Equals("Mac OS X") ||
					osVersion.Equals("Linux") ||
					osVersion.Equals("FreeBSD") ||
					osVersion.Equals("OpenBSD") ||
					osVersion.Equals("NetBSD"))
				{
					return AppDomain.CurrentDomain.BaseDirectory;
				}
			}
			string result = SDL.SDL_GetBasePath();
			if (string.IsNullOrEmpty(result))
			{
				result = AppDomain.CurrentDomain.BaseDirectory;
			}
			if (string.IsNullOrEmpty(result))
			{
				/* In the chance that there is no base directory,
				 * return the working directory and hope for the best.
				 *
				 * If we've reached this, the game has either been
				 * started from its directory, or a wrapper has set up
				 * the working directory to the game dir for us.
				 *
				 * Note about Android:
				 *
				 * There is no way from the C# side of things to cleanly
				 * obtain where the game is located without looking at an
				 * instance of System.Diagnostics.StackTrace or without
				 * some interop between the Java and C# side of things.
				 * We're assuming that either the environment itself is
				 * setting one of the possible base paths to point to the
				 * game dir, or that the Java side has called into the C#
				 * side to set Environment.CurrentDirectory.
				 *
				 * In the best case, nothing would be set and the game
				 * wouldn't use the title location in the first place, as
				 * the assets would be read directly from the .apk / .obb
				 * -ade
				 */
				result = Environment.CurrentDirectory;
			}
			return result;
		}

		// From https://github.com/FNA-XNA/FNA/blob/master/src/Audio/SoundEffect.cs
		public static void ReadWavFileFromStream(Stream stream, out int outSampleRate, out byte[] outDataBuffer, out AudioChannels outChannels)
        {
			// Sample data
			byte[] data;

			// WaveFormatEx data
			ushort wFormatTag;
			ushort nChannels;
			uint nSamplesPerSec;
			uint nAvgBytesPerSec;
			ushort nBlockAlign;
			ushort wBitsPerSample;
			// ushort cbSize;

			int samplerLoopStart = 0;
			int samplerLoopEnd = 0;

			using (BinaryReader reader = new BinaryReader(stream))
			{
				// RIFF Signature
				string signature = new string(reader.ReadChars(4));
				if (signature != "RIFF")
				{
					throw new NotSupportedException("Specified stream is not a wave file.");
				}

				reader.ReadUInt32(); // Riff Chunk Size

				string wformat = new string(reader.ReadChars(4));
				if (wformat != "WAVE")
				{
					throw new NotSupportedException("Specified stream is not a wave file.");
				}

				// WAVE Header
				string format_signature = new string(reader.ReadChars(4));
				while (format_signature != "fmt ")
				{
					reader.ReadBytes(reader.ReadInt32());
					format_signature = new string(reader.ReadChars(4));
				}

				int format_chunk_size = reader.ReadInt32();

				wFormatTag = reader.ReadUInt16();
				nChannels = reader.ReadUInt16();
				nSamplesPerSec = reader.ReadUInt32();
				nAvgBytesPerSec = reader.ReadUInt32();
				nBlockAlign = reader.ReadUInt16();
				wBitsPerSample = reader.ReadUInt16();

				// Reads residual bytes
				if (format_chunk_size > 16)
				{
					reader.ReadBytes(format_chunk_size - 16);
				}

				// data Signature
				string data_signature = new string(reader.ReadChars(4));
				while (data_signature.ToLowerInvariant() != "data")
				{
					reader.ReadBytes(reader.ReadInt32());
					data_signature = new string(reader.ReadChars(4));
				}
				if (data_signature != "data")
				{
					throw new NotSupportedException("Specified wave file is not supported.");
				}

				int waveDataLength = reader.ReadInt32();
				data = reader.ReadBytes(waveDataLength);

				// Scan for other chunks
				while (reader.PeekChar() != -1)
				{
					char[] chunkIDChars = reader.ReadChars(4);
					if (chunkIDChars.Length < 4)
					{
						break; // EOL!
					}
					byte[] chunkSizeBytes = reader.ReadBytes(4);
					if (chunkSizeBytes.Length < 4)
					{
						break; // EOL!
					}
					string chunk_signature = new string(chunkIDChars);
					int chunkDataSize = BitConverter.ToInt32(chunkSizeBytes, 0);
					if (chunk_signature == "smpl") // "smpl", Sampler Chunk Found
					{
						reader.ReadUInt32(); // Manufacturer
						reader.ReadUInt32(); // Product
						reader.ReadUInt32(); // Sample Period
						reader.ReadUInt32(); // MIDI Unity Note
						reader.ReadUInt32(); // MIDI Pitch Fraction
						reader.ReadUInt32(); // SMPTE Format
						reader.ReadUInt32(); // SMPTE Offset
						uint numSampleLoops = reader.ReadUInt32();
						int samplerData = reader.ReadInt32();

						for (int i = 0; i < numSampleLoops; i += 1)
						{
							reader.ReadUInt32(); // Cue Point ID
							reader.ReadUInt32(); // Type
							int start = reader.ReadInt32();
							int end = reader.ReadInt32();
							reader.ReadUInt32(); // Fraction
							reader.ReadUInt32(); // Play Count

							if (i == 0) // Grab loopStart and loopEnd from first sample loop
							{
								samplerLoopStart = start;
								samplerLoopEnd = end;
							}
						}

						if (samplerData != 0) // Read Sampler Data if it exists
						{
							reader.ReadBytes(samplerData);
						}
					}
					else // Read unwanted chunk data and try again
					{
						reader.ReadBytes(chunkDataSize);
					}
				}
				// End scan
			}

			outSampleRate = (int)nSamplesPerSec;
			outDataBuffer = data;
			outChannels = (AudioChannels)nChannels;
		}
	}
}
