using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using AdLib.Collections;

namespace AdLib.IO.SaveFiles.Content
{
    public class JsonSaveFile : SimpleSaveFile
    {
        public override string GetExtension() => "json";

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

                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(text, new JsonSerializerSettings() {
                    Formatting = Formatting.Indented
                });

                Data = new PropertyCollection(data);
            }
        }

        public override void Save(string path)
        {
            lock (_lock)
            {
                Dictionary<string, object> raw = Data.ToRaw();

                string text = JsonConvert.SerializeObject(raw, Formatting.Indented);

                File.WriteAllText(FullPath(path), text);
            }
        }
    }
}
