using Newtonsoft.Json;
using UWPMusicPlayerExtensions.Messages;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Service
{
    public class LyricsExtensionService
    {
        public LyricsRequest GetRequest(ValueSet message)
        {
            LyricsRequest request = null;

            if (message.ContainsKey(Commands.GetLyrics))
            {
                string data = message[Commands.GetLyrics] as string;
                request = JsonConvert.DeserializeObject<LyricsRequest>(data);
            }
            return request;
        }

        public ValueSet PrepareResponse(LyricsResponse response)
        {
            ValueSet message = new ValueSet();

            if (response == null)
            {
                message.Add(Response.Status, Response.Error);
            }
            else
            {
                string serialized = JsonConvert.SerializeObject(response);
                message.Add(Response.Result, serialized);
                message.Add(Response.Status, Response.OK);
            }

            return message;
        }
    }
}
