using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UWPMusicPlayerExtensions.Messages;

namespace UWPMusicPlayerExtensions.Client
{
    public class ArtistInfoExtensionsClient
    {
        private IExtensionClientHelper extensionHelper;
        private ConcurrentDictionary<string, ArtistInfoResponse> cache;

        public bool UseCache { get; set; }

        public ArtistInfoExtensionsClient(IExtensionClientHelper extensionsHelper)
        {
            this.extensionHelper = extensionsHelper;
            cache = new ConcurrentDictionary<string, ArtistInfoResponse>();
            UseCache = true;
        }

        public async Task<ArtistInfoResponse> SendRequestAsync(ArtistInfoRequest request, CancellationToken token)
        {
            var availableExtensions = await extensionHelper.GetAvailableExtensions();
            token.ThrowIfCancellationRequested();
            var response = await SendRequestAsync(request, availableExtensions, token);
            return response;
        }

        public async Task<ArtistInfoResponse> SendRequestAsync(ArtistInfoRequest request, AppExtensionInfo extension, CancellationToken token)
        {
            var response = await SendRequestAsync(request, new List<AppExtensionInfo>() { extension }, token);
            return response;
        }

        public async Task<ArtistInfoResponse> SendRequestAsync(ArtistInfoRequest request, IEnumerable<AppExtensionInfo> extensions, CancellationToken token)
        {
            ArtistInfoResponse res = null;
            string key = ToKey(request);
            if (UseCache)
            {
                if (cache.TryGetValue(key, out res))
                {
                    return res;
                }
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var data = JsonConvert.SerializeObject(request);
            parameters.Add(Commands.GetArtistInfo, data);

            foreach (var ext in extensions)
            {
                token.ThrowIfCancellationRequested();
                var response = await extensionHelper.InvokeExtension(ext, parameters);
                token.ThrowIfCancellationRequested();
                if (response != null && response.ContainsKey(Response.Result))
                {
                    res = JsonConvert.DeserializeObject<ArtistInfoResponse>(response[Response.Result] as string);
                    if (res != null && (!String.IsNullOrEmpty(res.ArtistBio) || !String.IsNullOrEmpty(res.ArtistImage) || res.Discography.Count != 0))
                    {
                        cache.TryAdd(key, res);
                        break;
                    }
                }
            }

            if (res == null)
            {
                res = new ArtistInfoResponse() { ArtistBio = "", ArtistImage = "" };
            }

            return res;
        }

        private string ToKey(ArtistInfoRequest request)
        {
            return request.Artist;
        }
    }
}
