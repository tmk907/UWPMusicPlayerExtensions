using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions.Store
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
        [JsonConverter(typeof(StringEnumConverter))]
        public MusicPlayerExtensionTypes Type { get; set; }
    }

    public class AvailableExtensions
    {
        [JsonProperty("extensions")]
        public List<AvailableExtension> Extensions { get; set; }
    }
}
