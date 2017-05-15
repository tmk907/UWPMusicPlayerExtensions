using Newtonsoft.Json;

namespace UWPMusicPlayerExtensions.Messages
{
    public class AlbumInfoRequest
    {
        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("albumartist")]
        public string AlbumArtist { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }
    }
}
