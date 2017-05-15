using Newtonsoft.Json;

namespace UWPMusicPlayerExtensions.Messages
{
    public class LyricsRequest
    {
        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("prefersynchronized")]
        public bool PreferSynchronized { get; set; } = false;
    }
}
