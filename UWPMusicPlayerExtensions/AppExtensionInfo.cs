using UWPMusicPlayerExtensions.Enums;

namespace UWPMusicPlayerExtensions
{
    public class AppExtensionInfo
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string PackageName { get; set; }
        public ExtensionTypes Type { get; set; }
    }
}
