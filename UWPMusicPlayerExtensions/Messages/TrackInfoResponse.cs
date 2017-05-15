using Newtonsoft.Json;
using System;

namespace UWPMusicPlayerExtensions.Messages
{
    public class TrackInfoResponse
    {
        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }

        public TrackInfoResponse()
        {
            Album = "";
            Artist = "";
            Title = "";
            Duration = TimeSpan.Zero;
        }
    }
}
