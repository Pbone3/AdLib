namespace AdLib.DataStructures
{
    public struct Property
    {
        public readonly string Name;
        public object Value;

        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public T Get<T>() => (T)Value;
        public void Set<T>(T value) => Value = value;

        public override int GetHashCode() => Name.GetHashCode() ^ Value.GetHashCode();
        public override string ToString() => Name + ": " + Value;
    }
}
