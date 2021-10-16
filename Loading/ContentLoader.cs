using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Tomlet;
using Tomlet.Models;

namespace AdLib.Loading
{
    // LOADS ContentPacks
    public static class ContentLoader
    {
        public static Dictionary<string, ContentPack> LoadedContentPacks = new Dictionary<string, ContentPack>();
        public static EnabledPackList EnabledPackList = new EnabledPackList(new HashSet<string>());

        public static void Load()
        {
            // STEPS
            // 1. REGISTER ContentPacks with ContentRegisterer
            // 2. INITIALIZE ContentPacks
            // 3. LOAD ContentPack content

            // Step 1: REGISTER. Find ContentPacks that you need to load
            List<(string folderPath, string assemblyPath)> contentPackPaths = GetContentPackPaths();

            // Step 2: INITIALIZE. Initialize the ContentPack class, and load metadata
            foreach ((string folderPath, string assemblyPath) in contentPackPaths)
            {
                string mdPath = Path.Combine(folderPath, "Metadata.toml");

                TomlDocument metadata = TomlParser.ParseFile(mdPath);

                // TODO dependencies

                AssemblyLoadContext alc = new AssemblyLoadContext(metadata.GetSubTable("ContentPackInfo").GetString("Name"), true);
                Assembly contentPackAssembly = alc.LoadFromAssemblyPath(assemblyPath);

                Type contentPackType = contentPackAssembly.GetTypes().First(t => t.IsSubclassOf(typeof(ContentPack)));
                ContentPack contentPackInstance = Activator.CreateInstance(contentPackType) as ContentPack;

                contentPackInstance.Metadata = metadata;

                LoadedContentPacks.Add(contentPackInstance.Name, contentPackInstance);

                contentPackInstance.Load();
            }

            // Step 3: LOAD. Load all loadable content to singleton instances
            // I just have this here for clarity. Once an assembly enters the AppDomain,
            // the CLR makes more generic types from existing assemblies for generic types in the loaded asm
            // The actual assingning of instances is down in ContentSingleton
        }

        public static void NewLoad()
        {
            foreach (string pack in EnabledPackList.EnabledPacks)
            {

            }
        }

        public static void Unload()
        {
            // STEPS
            // 1. CALL all relevant unload methods
            // 2. UNLOAD ContentPack assemblies
            // 3. CLEAR LoadedContentPacks dictionary to prepare for the next load

            // Step 1: CALL. Call all relevant unload methods
            foreach (KeyValuePair<string, ContentPack> queryContentPack in LoadedContentPacks)
            {
                queryContentPack.Value.Unload();
            }

            // Step 2: UNLOAD. Unload ContentPack assemblies
            foreach (KeyValuePair<string, ContentPack> queryContentPack in LoadedContentPacks)
            {
                //queryContentPack.Value.Secret_ALC.Unload();
            }

            // Step 3: CLEAR. Clear LoadedContentPacks
            LoadedContentPacks.Clear();
        }

        public static List<(string folderPath, string assemblyPath)> GetContentPackPaths()
        {
            List<(string folderPath, string assemblyPath)> finalPaths = new List<(string folderPath, string assemblyPath)>();
            string[] folders = Directory.GetDirectories(GameInfo.GetContentPath());

            foreach (string contentPackFolderPath in folders)
            {
                string[] contentPackFolderFileNames = Directory.GetFiles(contentPackFolderPath);

                string contentPackAssemblyPath = contentPackFolderFileNames.SingleOrDefault(s => s.EndsWith(".dll"));

                if (contentPackAssemblyPath == default)
                    throw new Exception("Content pack at " + contentPackFolderPath + " does not contain exactly one .dll file.");

                Assembly contentPackAssembly = Assembly.LoadFile(contentPackAssemblyPath);

                if (contentPackAssembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(ContentPack))) == default)
                    throw new Exception("Content pack at " + contentPackFolderPath + " does not contain exactly one class extending ContentPack.");

                finalPaths.Add((contentPackFolderPath, contentPackAssemblyPath));
            }

            return finalPaths;
        }
    }
}
