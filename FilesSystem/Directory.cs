using Newtonsoft.Json;

namespace FilesSystem
{
    public class Directory
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("files")]
        public List<string> Files { get; set; }

        [JsonProperty("directories")]
        public List<List<Directory>> Directories { get; set; }
    }
}
