using Newtonsoft.Json;

namespace UWPMusicPlayerExtensions.Messages
{
    public class ArtistInfoRequest
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }
    }
}
