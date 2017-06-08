using Newtonsoft.Json;
using UWPMusicPlayerExtensions.Messages;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Service
{
    public class NowPlayingStatusExtensionService
    {
        public NowPlayingRequest GetRequest(ValueSet message)
        {
            NowPlayingRequest request = null;

            if (message.ContainsKey(Commands.GetNowPlayingInfo))
            {
                string data = message[Commands.GetNowPlayingInfo] as string;
                request = JsonConvert.DeserializeObject<NowPlayingRequest>(data);
            }
            return request;
        }

        public ValueSet PrepareResponse(NowPlayingResponse response)
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
