using SDL2;
using System;
using System.Collections.Generic;
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
	}
}
