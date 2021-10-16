using System;
using System.Diagnostics.CodeAnalysis;

namespace AdLib.DataStructures
{
    public struct Identifier : IEquatable<Identifier>
    {
        public string Root;
        public string Path;

        public Identifier(string root, string path)
        {
            Root = root;
            Path = path;
        }

        public override string ToString() => Root + "." + Path;
        public override int GetHashCode() => Root.GetHashCode() ^ Path.GetHashCode();

        public bool Equals([AllowNull] Identifier other) => Root == other.Root && Path == other.Path;

        public static bool operator ==(Identifier left, Identifier right) => left.Equals(right);
        public static bool operator !=(Identifier left, Identifier right) => !left.Equals(right);
    }
}
