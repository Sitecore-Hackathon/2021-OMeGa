using System.Collections.Generic;
using Sitecore.Annotations;
using Sitecore.Diagnostics;
using Newtonsoft.Json;

namespace Feature.PresentationVisualizer.Data
{
    public class PresentationElement
    {
        public PresentationElement([NotNull] string name, [NotNull] string type, [CanBeNull] DataSource dataSource, [NotNull] IList<PresentationElement> children)
        {
            Assert.ArgumentNotNullOrEmpty(name, nameof(name));
            Assert.ArgumentNotNullOrEmpty(type, nameof(type));
            Assert.ArgumentNotNull(children, nameof(children));

            Name = name;
            Type = type;
            DataSource = dataSource;
            Children = children;
        }

        [NotNull]
        [JsonProperty("name")]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty("type")]
        public string Type { get; set; }

        [CanBeNull]
        [JsonProperty("dataSource")]
        public DataSource DataSource { get; set; }

        [NotNull]
        [JsonProperty("children")]
        public IList<PresentationElement> Children { get; set; }
    }
}