using Newtonsoft.Json;
using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions.Messages
{
    public class NowPlayingResponse
    {
        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("albumart")]
        public string AlbumArt { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("playbackstatus")]
        public PlaybackStatus Status { get; set; }

        public NowPlayingResponse()
        {
            Album = "";
            AlbumArt = "";
            Artist = "";
            Title = "";
            Status = PlaybackStatus.Closed;
        }
    }
}
