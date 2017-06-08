using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UWPMusicPlayerExtensions.Messages;

namespace UWPMusicPlayerExtensions.Client
{
    public class NowPlayingNotificationBroadcaster
    {
        private IExtensionClientHelper extensionHelper;

        public NowPlayingNotificationBroadcaster(IExtensionClientHelper extensionsHelper)
        {
            this.extensionHelper = extensionsHelper;
        }

        public async Task SendRequestAsync(NowPlayingNotification request, CancellationToken token)
        {
            var availableExtensions = await extensionHelper.GetInstalledExtensions();
            token.ThrowIfCancellationRequested();
            await SendRequestAsync(request, availableExtensions, token);
        }

        public async Task SendRequestAsync(NowPlayingNotification request, AppExtensionInfo extension, CancellationToken token)
        {
            await SendRequestAsync(request, new List<AppExtensionInfo>() { extension }, token);
        }

        public async Task SendRequestAsync(NowPlayingNotification request, IEnumerable<AppExtensionInfo> extensions, CancellationToken token)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var data = JsonConvert.SerializeObject(request);
            parameters.Add(Commands.GetNowPlayingInfo, data);

            foreach (var ext in extensions)
            {
                token.ThrowIfCancellationRequested();
                await extensionHelper.InvokeExtension(ext, parameters);
            }
        }
    }
}
