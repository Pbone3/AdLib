using AdLib.DataStructures;
using System.Collections.Generic;

namespace AdLib.Loading
{
    public class ContentSingleton<T> where T : ILoadable
    {
        public static Dictionary<Identifier, T> Instances { get; private set; } = new Dictionary<Identifier, T>();
    }
}
