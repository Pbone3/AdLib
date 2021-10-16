using AdLib.DataStructures;

namespace AdLib.Loading
{
    public interface ILoadable
    {
        public void Load() { }
        public void Unload() { }

        public Identifier Identifier { get; }

        public object CloneContent();
    }
}
