using System.Linq;

namespace AdLib.Loading
{
    public static class ContentManager
    {
        /// <summary>
        /// Gets the loaded content pack with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ContentPack GetContentPack(string name)
        {
            ContentLoader.LoadedContentPacks.TryGetValue(name, out ContentPack pack);
            return pack;
        }
        /// <summary>
        /// Returns whether the specified ContentPack is loaded or not
        /// </summary>
        public static bool IsContentPackLoaded(string name) => ContentLoader.LoadedContentPacks.ContainsKey(name);

        /// <summary>
        /// Gets an instance of the specified content. If multiple instances exist, gets the first
        /// </summary>
        public static T GetInstance<T>() where T : ILoadable, new() => ContentSingleton<T>.Instances.First().Value;
        public static T GetInstanceClone<T>() where T : ILoadable, new() => (T)GetInstance<T>().CloneContent();
    }
}
