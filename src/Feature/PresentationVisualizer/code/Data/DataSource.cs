using Sitecore.Annotations;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Newtonsoft.Json;

namespace Feature.PresentationVisualizer.Data
{
    public class DataSource
    {
        public DataSource([NotNull] ID id, [NotNull] string name, [NotNull] string path)
        {
            Assert.ArgumentNotNull(id, nameof(id));
            Assert.ArgumentNotNullOrEmpty(name, nameof(name));
            Assert.ArgumentNotNullOrEmpty(path, nameof(path));

            Id = id;
            Name = name;
            Path = path;
        }

        [NotNull]
        [JsonProperty("id")]
        public ID Id { get; set; }

        [NotNull]
        [JsonProperty("name")]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}