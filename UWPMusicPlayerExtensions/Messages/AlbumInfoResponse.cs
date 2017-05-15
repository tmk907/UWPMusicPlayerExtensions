using Newtonsoft.Json;
using System.Collections.Generic;

namespace UWPMusicPlayerExtensions.Messages
{
    public class AlbumInfoResponse
    {
        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("albumart")]
        public string AlbumArt { get; set; }

        [JsonProperty("albumartist")]
        public string AlbumArtist { get; set; }

        [JsonProperty("albuminfo")]
        public string AlbumInfo { get; set; }

        [JsonProperty("songs")]
        public List<TrackInfoResponse> Songs { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }


        public AlbumInfoResponse()
        {
            Album = "";
            AlbumArt = "";
            AlbumArtist = "";
            AlbumInfo = "";
            Songs = new List<TrackInfoResponse>();
            Year = 0;
        }
    }
}
