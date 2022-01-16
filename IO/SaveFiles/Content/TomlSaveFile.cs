using AdLib.Collections;
using AdLib.Helpers;
using System.Collections.Generic;
using System.IO;
using Tomlet;
using Tomlet.Models;

namespace AdLib.IO.SaveFiles.Content
{
    // TODO Test
    public class TomlSaveFile : SimpleSaveFile
    {
        public override string GetExtension() => "toml";

        private readonly object _lock = new object();

        public override void Load(string path)
        {
            lock (_lock)
            {
                if (!File.Exists(FullPath(path)))
                {
                    FileStream stream = File.Create(FullPath(path));
                    stream.Dispose();
                }

                string text = File.ReadAllText(FullPath(path));
                TomlDocument doc = new TomlParser().Parse(text);

                Dictionary<string, object> data = IOHelper.FlattenTomlDocument(doc);

                Data = new PropertyCollection(data);
            }
        }

        public override void Save(string path)
        {
            lock (_lock)
            {
                Dictionary<string, object> raw = Data.ToRaw();

                string text = TomletMain.DocumentFrom<Dictionary<string, object>>(Data.ToRaw()).SerializedValue;

                File.WriteAllText(FullPath(path), text);
            }
        }
    }
}
