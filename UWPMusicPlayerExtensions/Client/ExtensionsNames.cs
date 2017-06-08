using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions.Client
{
    public class ExtensionsNames
    {
        public static readonly string AlbumInfo = "uwp.music-player.albuminfo";
        public static readonly string ArtistInfo = "uwp.music-player.artistinfo";
        public static readonly string NowPlayingSongStatus = "uwp.music-player.nowplayingsongstatus";
        public static readonly string NowPlayingSongNotification = "uwp.music-player.npnotification";
        public static readonly string Lyrics = "uwp.music-player.lyrics";
        public static readonly string TrackInfo = "uwp.music-player.trackinfo";
        public static readonly string Unknown = "uwp.music-player.unknown";

        public static string GetName(MusicPlayerExtensionTypes type)
        {
            switch (type)
            {
                case MusicPlayerExtensionTypes.AlbumInfo:
                    return AlbumInfo;
                case MusicPlayerExtensionTypes.ArtistInfo:
                    return ArtistInfo;
                case MusicPlayerExtensionTypes.NowPlaying:
                    return NowPlayingSongStatus;
                case MusicPlayerExtensionTypes.NowPlayingNotification:
                    return NowPlayingSongNotification;
                case MusicPlayerExtensionTypes.Lyrics:
                    return Lyrics;
                case MusicPlayerExtensionTypes.TrackInfo:
                    return TrackInfo;
                default:
                    return Unknown;
            }
        }
    }
}
