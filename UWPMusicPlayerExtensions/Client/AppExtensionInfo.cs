using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions.Client
{
    public class AppExtensionInfo
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string PackageName { get; set; }
        public string ServiceName { get; set; }
        public string StoreId { get; set; }
        public MusicPlayerExtensionTypes Type { get; set; }
    }
}
