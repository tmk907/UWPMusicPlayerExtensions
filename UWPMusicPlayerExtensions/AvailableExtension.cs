using Newtonsoft.Json;
using System.Collections.Generic;
using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions
{
    public class AvailableExtension
    {
        [JsonProperty("displayname")]
        public string DisplayName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("storeid")]
        public string StoreId { get; set; }
        [JsonProperty("type")]
        public ExtensionTypes Type { get; set; }
    }

    public class AvailableExtensions
    {
        [JsonProperty("extensions")]
        public List<AvailableExtension> Extensions { get; set; }
    }
}
