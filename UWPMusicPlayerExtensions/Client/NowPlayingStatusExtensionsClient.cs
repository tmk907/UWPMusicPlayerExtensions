using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UWPMusicPlayerExtensions.Messages;

namespace UWPMusicPlayerExtensions.Client
{
    public class NowPlayingStatusExtensionsClient
    {
        private IExtensionClientHelper extensionHelper;

        public NowPlayingStatusExtensionsClient(IExtensionClientHelper extensionsHelper)
        {
            this.extensionHelper = extensionsHelper;
        }

        public async Task<List<NowPlayingResponse>> SendRequestAsync(NowPlayingRequest request, CancellationToken token)
        {
            var availableExtensions = await extensionHelper.GetInstalledExtensions();
            token.ThrowIfCancellationRequested();
            var response = await SendRequestAsync(request, availableExtensions, token);
            return response;
        }

        public async Task<List<NowPlayingResponse>> SendRequestAsync(NowPlayingRequest request, AppExtensionInfo extension, CancellationToken token)
        {
            var response = await SendRequestAsync(request, new List<AppExtensionInfo>() { extension }, token);
            return response;
        }

        public async Task<List<NowPlayingResponse>> SendRequestAsync(NowPlayingRequest request, IEnumerable<AppExtensionInfo> extensions, CancellationToken token)
        {
            List<NowPlayingResponse> list = new List<NowPlayingResponse>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var data = JsonConvert.SerializeObject(request);
            parameters.Add(Commands.GetNowPlayingInfo, data);

            foreach (var ext in extensions)
            {
                token.ThrowIfCancellationRequested();
                var response = await extensionHelper.InvokeExtension(ext, parameters);
                token.ThrowIfCancellationRequested();
                if (response != null && response.ContainsKey(Response.Result))
                {
                    var res = JsonConvert.DeserializeObject<NowPlayingResponse>(response[Response.Result] as string);
                    if (res != null)
                    {
                        list.Add(res);
                    }
                }
            }

            return list;
        }
    }
}
