using AdLib.DataStructures;

namespace AdLib.IO.SaveFiles
{
    public interface ISaveFile
    {
        Property Get(string key);
        void Set(string key, object value);

        void AddIfMissing(string key, Property value);

        void Load(string path);
        void Save(string path);
        string GetExtension();
    }
}
