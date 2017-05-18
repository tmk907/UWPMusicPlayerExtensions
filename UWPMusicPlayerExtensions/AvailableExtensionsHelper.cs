using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace UWPMusicPlayerExtensions
{
    public class AvailableExtensionsHelper
    {
        public AvailableExtensionsHelper()
        {
            uri = new Uri(@"https://raw.githubusercontent.com/tmk907/UWPMusicPlayerExtensions/extensions/AvailableExtensions.json");
        }

        public Uri uri { get; private set; }

        public async Task<List<AvailableExtension>> GetAvailableExtensions()
        {
            List<AvailableExtension> extensions = new List<AvailableExtension>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(uri);
                    extensions = JsonConvert.DeserializeObject<AvailableExtensions>(response).Extensions;
                }
                catch (Exception ex)
                {

                }
            }

            return extensions;
        }
    }
}
