using System;
using System.Diagnostics.CodeAnalysis;

namespace AdLib.DataStructures
{
    public struct Identifier : IEquatable<Identifier>
    {
        public static Identifier Default = new Identifier("NOT_DEFINED:this.identifier.is.the.default.value");

        public string Root;
        public string Path;

        public Identifier(string root, string path)
        {
            Root = root;
            Path = path;
        }

        public Identifier(string raw)
        {
            string[] parts = raw.Split(':');
            if (parts.Length != 2)
                throw new ArgumentException("Raw identifier is not formatted properly.");

            Root = parts[0];
            Path = parts[1];
        }

        public override string ToString() => Root + ":" + Path;
        public override int GetHashCode() => Root.GetHashCode() ^ Path.GetHashCode();

        public bool Equals([AllowNull] Identifier other) => Root == other.Root && Path == other.Path;
        public override bool Equals(object obj) => obj is Identifier identifier && Equals(identifier);

        public static bool operator ==(Identifier left, Identifier right) => left.Equals(right);
        public static bool operator !=(Identifier left, Identifier right) => !left.Equals(right);

        public static implicit operator string(Identifier me) => me.ToString();
        public static implicit operator Identifier(string raw) => new Identifier(raw);
    }
}
