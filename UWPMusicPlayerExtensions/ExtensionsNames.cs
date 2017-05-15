using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions
{
    public class ExtensionsNames
    {
        public static readonly string AlbumInfo = "uwp.music-player.albuminfo";
        public static readonly string ArtistInfo = "uwp.music-player.artistinfo";
        public static readonly string Lyrics = "uwp.music-player.lyrics";
        public static readonly string TrackInfo = "uwp.music-player.trackinfo";
        public static readonly string Unknown = "uwp.music-player.unknown";

        public static string GetName(ExtensionTypes type)
        {
            switch (type)
            {
                case ExtensionTypes.AlbumInfo:
                    return AlbumInfo;
                case ExtensionTypes.ArtistInfo:
                    return ArtistInfo;
                case ExtensionTypes.Lyrics:
                    return Lyrics;
                case ExtensionTypes.TrackInfo:
                    return TrackInfo;
                default:
                    return Unknown;
            }
        }
    }
}
