using AdLib.Collections;
using AdLib.DataStructures;

namespace AdLib.IO.SaveFiles
{
    public abstract class SimpleSaveFile : ISaveFile
    {
        public string FullPath(string path) => path + "." + GetExtension();

        public PropertyCollection Data = new PropertyCollection();

        public abstract string GetExtension();

        public abstract void Load(string path);
        public abstract void Save(string path);

        public void AddIfMissing(string key, object value) => AddIfMissing(key, new Property(key, value));
        public void AddIfMissing(string key, Property value) => Data.AddIfMissing(key, value);

        public T Get<T>(string key) => Data.Get<T>(key);
        public Property Get(string key) => Data[key];
        public void Set(string key, object value) => Data.Set(key, value);
    }
}
