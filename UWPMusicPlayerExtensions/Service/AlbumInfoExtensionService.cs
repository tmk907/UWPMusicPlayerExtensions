using Newtonsoft.Json;
using UWPMusicPlayerExtensions.Messages;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Service
{
    public class AlbumInfoExtensionService
    {
        public ArtistInfoRequest GetRequest(ValueSet message)
        {
            ArtistInfoRequest request = null;

            if (message.ContainsKey(Commands.GetArtistInfo))
            {
                string data = message[Commands.GetArtistInfo] as string;
                request = JsonConvert.DeserializeObject<ArtistInfoRequest>(data);
            }
            return request;
        }

        public ValueSet PrepareResponse(ArtistInfoResponse response)
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
