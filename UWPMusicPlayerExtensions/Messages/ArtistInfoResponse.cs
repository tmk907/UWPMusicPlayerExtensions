using Newtonsoft.Json;
using System.Collections.Generic;

namespace UWPMusicPlayerExtensions.Messages
{
    public class ArtistInfoResponse
    {
        [JsonProperty("artistbio")]
        public string ArtistBio { get; set; }

        [JsonProperty("artistimage")]
        public string ArtistImage { get; set; }

        [JsonProperty("discography")]
        public List<AlbumInfoResponse> Discography { get; set; }

        public ArtistInfoResponse()
        {
            ArtistBio = "";
            ArtistImage = "";
            Discography = new List<AlbumInfoResponse>();
        }
    }
}
