using Newtonsoft.Json;
using UWPMusicPlayerExtensions.Messages;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Service
{
    public class NowPlayingNotificationExtensionReceiver
    {
        public NowPlayingNotification GetRequest(ValueSet message)
        {
            NowPlayingNotification request = null;

            if (message.ContainsKey(Commands.NowPlayingNotification))
            {
                string data = message[Commands.NowPlayingNotification] as string;
                request = JsonConvert.DeserializeObject<NowPlayingNotification>(data);
            }
            return request;
        }
    }
}
