using AdLib.DataStructures;
using Tomlet.Models;

namespace AdLib.Loading
{
    public abstract class ContentPack
    {
        public abstract string Name { get; }
        public int Identifier => Name.GetHashCode();

        public TomlDocument Metadata;

        public virtual void Load() { }
        public virtual void Unload() { }

        public Identifier GetId(string id) => new Identifier(Name, id);
    }
}
