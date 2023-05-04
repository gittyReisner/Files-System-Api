using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilesSystem
{
    public class ResourceReader
    {
        private static readonly Assembly ExecAssembly = Assembly.GetExecutingAssembly();
        private static readonly string[] ResourceNames = ExecAssembly.GetManifestResourceNames();
        public static string FileJsonName = "tree.json";

        public static string Read()
        {
            var resource = GetResourceName();
            using var stream = ExecAssembly.GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            return reader.ReadToEnd();
        }
        static string GetResourceName() => ResourceNames.First(n => n.EndsWith(FileJsonName));
        public static T ReadAndConvert<T>()
        {
            string content = Read();
            return JsonConvert.DeserializeObject<T>(content);
        }
        public static List<Directory> GetByPrefix(string q)
        {
            var mainDirectories = ReadAndConvert<List<Directory>>();
            var directories = FilterDirectories(mainDirectories, q);
            return directories;
        }

        private static List<Directory> FilterDirectories(List<Directory> directories, string prefix)
        {
            List<Directory> filteredDirectories = new List<Directory>();
            for (int i = 0; i < directories.Count; i++)
            {
                var innerDirectory = directories[i];
                innerDirectory.Files = innerDirectory.Files.Where(f => f.StartsWith(prefix)).ToList();

                for (int j = 0; j < innerDirectory.Directories.Count; j++)
                {
                    var nestedFilteredDirectories = FilterDirectories(innerDirectory.Directories[j], prefix);
                    innerDirectory.Directories[j] = nestedFilteredDirectories;
                }

                innerDirectory.Directories = innerDirectory.Directories
                    .Where(nestedDirectory => nestedDirectory.Count > 0).ToList();

                if (IsMatch(innerDirectory, prefix))
                    filteredDirectories.Add(innerDirectory);
            }

            return filteredDirectories;
        }


        private static bool IsMatch(Directory directory, string prefix) => directory.Directories.Count > 0 ||
                                                                    directory.Files.Count > 0 ||
                                                                    directory.Name.StartsWith(prefix)
            ? true
            : false;




    }
}