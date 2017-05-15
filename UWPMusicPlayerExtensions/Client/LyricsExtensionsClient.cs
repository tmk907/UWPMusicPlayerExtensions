using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UWPMusicPlayerExtensions.Messages;

namespace UWPMusicPlayerExtensions.Client
{
    public class LyricsExtensionsClient
    {
        private IExtensionClientHelper extensionHelper;
        private ConcurrentDictionary<string, LyricsResponse> cache;

        public bool UseCache { get; set; }

        public LyricsExtensionsClient(IExtensionClientHelper extensionsHelper)
        {
            this.extensionHelper = extensionsHelper;
            cache = new ConcurrentDictionary<string, LyricsResponse>();
            UseCache = true;
        }

        public async Task<LyricsResponse> SendRequestAsync(LyricsRequest request, CancellationToken token)
        {
            var availableExtensions = await extensionHelper.GetAvailableExtensions();
            token.ThrowIfCancellationRequested();
            var response = await SendRequestAsync(request, availableExtensions, token);
            return response;
        }

        public async Task<LyricsResponse> SendRequestAsync(LyricsRequest request, AppExtensionInfo extension, CancellationToken token)
        {
            var response = await SendRequestAsync(request, new List<AppExtensionInfo>() { extension }, token);
            return response;
        }

        public async Task<LyricsResponse> SendRequestAsync(LyricsRequest request, IEnumerable<AppExtensionInfo> extensions, CancellationToken token)
        {
            LyricsResponse res = null;
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
            parameters.Add(Commands.GetLyrics, data);

            foreach (var ext in extensions)
            {
                token.ThrowIfCancellationRequested();
                var response = await extensionHelper.InvokeExtension(ext, parameters);
                token.ThrowIfCancellationRequested();
                if (response != null && response.ContainsKey(Response.Result))
                {
                    res = JsonConvert.DeserializeObject<LyricsResponse>(response[Response.Result] as string);
                    if (res != null && (!String.IsNullOrEmpty(res.Lyrics) || !String.IsNullOrEmpty(res.Url)))
                    {
                        cache.TryAdd(key, res);
                        break;
                    }
                }
            }

            if (res == null)
            {
                res = new LyricsResponse() { Lyrics = "", Url = "" };
            }

            return res;
        }

        private string ToKey(LyricsRequest request)
        {
            return request.Album + "/" + request.Artist + "/" + request.PreferSynchronized + "/" + request.Title;
        }
    }
}
