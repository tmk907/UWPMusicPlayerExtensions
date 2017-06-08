using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UWPMusicPlayerExtensions.Messages;

namespace UWPMusicPlayerExtensions.Client
{
    public class AlbumInfoExtensionsClient
    {
        private IExtensionClientHelper extensionHelper;
        private ConcurrentDictionary<string, AlbumInfoResponse> cache;

        public bool UseCache { get; set; }

        public AlbumInfoExtensionsClient(IExtensionClientHelper extensionsHelper)
        {
            this.extensionHelper = extensionsHelper;
            cache = new ConcurrentDictionary<string, AlbumInfoResponse>();
            UseCache = true;
        }

        public async Task<AlbumInfoResponse> SendRequestAsync(AlbumInfoRequest request, CancellationToken token)
        {
            var availableExtensions = await extensionHelper.GetInstalledExtensions();
            token.ThrowIfCancellationRequested();
            var response = await SendRequestAsync(request, availableExtensions, token);
            return response;
        }

        public async Task<AlbumInfoResponse> SendRequestAsync(AlbumInfoRequest request, AppExtensionInfo extension, CancellationToken token)
        {
            var response = await SendRequestAsync(request, new List<AppExtensionInfo>() { extension }, token);
            return response;
        }

        public async Task<AlbumInfoResponse> SendRequestAsync(AlbumInfoRequest request, IEnumerable<AppExtensionInfo> extensions, CancellationToken token)
        {
            AlbumInfoResponse res = null;
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
            parameters.Add(Commands.GetAlbumInfo, data);

            foreach (var ext in extensions)
            {
                token.ThrowIfCancellationRequested();
                var response = await extensionHelper.InvokeExtension(ext, parameters);
                token.ThrowIfCancellationRequested();
                if (response != null && response.ContainsKey(Response.Result))
                {
                    res = JsonConvert.DeserializeObject<AlbumInfoResponse>(response[Response.Result] as string);
                    if (res != null && (!String.IsNullOrEmpty(res.AlbumInfo) || !String.IsNullOrEmpty(res.AlbumArt) || res.Songs.Count != 0))
                    {
                        cache.TryAdd(key, res);
                        break;
                    }
                }
            }

            if (res == null)
            {
                res = new AlbumInfoResponse() { Album = request.Album, AlbumArtist = request.AlbumArtist, Year = request.Year ?? 0 };
            }

            return res;
        }

        private string ToKey(AlbumInfoRequest request)
        {
            return request.Album + "/" + request.AlbumArtist + "/" + request.Year;
        }
    }
}
