using Sitecore.Annotations;
using Sitecore.Diagnostics;
using Newtonsoft.Json;

namespace Feature.PresentationVisualizer.Data
{
    public class RenderingParameter
    {
        public RenderingParameter([NotNull] string name, [NotNull] string value)
        {
            Assert.ArgumentNotNullOrEmpty(name, nameof(name));
            Assert.ArgumentNotNull(value, nameof(value));

            Name = name;
            Value = value;
        }

        [NotNull]
        [JsonProperty("name")]
        string Name { get; }

        [NotNull]
        [JsonProperty("value")]
        string Value { get; }
    }
}