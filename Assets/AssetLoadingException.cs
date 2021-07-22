using System;

namespace AdLib.Assets
{
    [Serializable]
    public class AssetLoadingException : Exception
    {
        public AssetLoadingException() { }
        public AssetLoadingException(string type, string path) : base($"Issue occured while loading {type} from {path}") { }
        public AssetLoadingException(string type, string path, Exception inner) : base($"Issue occured while loading {type} from {path}", inner) { }

        protected AssetLoadingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
