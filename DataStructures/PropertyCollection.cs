using System.Collections;
using System.Collections.Generic;

namespace AdLib.DataStructures
{
    public class PropertyCollection : IEnumerable<KeyValuePair<string, Property>>
    {
        public Property this[string key]
        {
            get => Inner[key];
            set => Set(key, value);
        }

        public int Length => Inner.Count;

        private readonly Dictionary<string, Property> Inner = new Dictionary<string, Property>();

        public PropertyCollection()
        {
        }

        public PropertyCollection(Dictionary<string, Property> raw)
        {
            raw ??= new Dictionary<string, Property>();
            foreach (KeyValuePair<string, Property> kvp in raw)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public PropertyCollection(Dictionary<string, object> raw)
        {
            raw ??= new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> kvp in raw)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public T Get<T>(string key) => Inner[key].Get<T>();
        public void Set<T>(string key, T value) => Inner[key].Set(value);
        public void SetOrAdd<T>(string key, T value)
        {
            if (Contains(key))
                Set(key, value);
            else
                Add(key, value);
        }

        public void Add<T>(string key, T value) => Inner.Add(key, new Property(key, value));
        public void Add(string key, Property value) => Inner.Add(key, value);
        public void AddIfMissing(string key, Property value)
        {
            if (Inner.ContainsKey(key))
                return;

            Inner.Add(key, value);
        }

        public bool Contains(string key) => Inner.ContainsKey(key);

        public Dictionary<string, object> ToRaw()
        {
            Dictionary<string, object> raw = new Dictionary<string, object>();

            foreach (KeyValuePair<string, Property> kvp in this)
            {
                raw.Add(kvp.Key, kvp.Value.Value);
            }

            return raw;
        }

        public bool TryGet<T>(string key, out T value)
        {
            if (!Contains(key))
            {
                value = default;
                return false;
            }

            value = Get<T>(key);
            return true;
        }

        public IEnumerator<KeyValuePair<string, Property>> GetEnumerator() => Inner.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();

        public override int GetHashCode() => Inner.GetHashCode();
        public override string ToString()
        {
            string s = "";

            foreach (KeyValuePair<string, Property> kvp in this)
            {
                s += kvp.Value.ToString() + "\n";
            }

            return s;
        }
    }
}
