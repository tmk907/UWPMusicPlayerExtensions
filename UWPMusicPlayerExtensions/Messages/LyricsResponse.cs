using Newtonsoft.Json;

namespace UWPMusicPlayerExtensions.Messages
{
    public class LyricsResponse
    {
        [JsonProperty("lyrics")]
        public string Lyrics { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("synchronized")]
        public bool Synchronized { get; set; }

        public LyricsResponse()
        {
            Lyrics = "";
            Url = "";
            Synchronized = false;
        }
    }
}
