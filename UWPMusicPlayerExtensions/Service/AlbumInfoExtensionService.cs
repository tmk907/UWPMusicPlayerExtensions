﻿using Newtonsoft.Json;
using UWPMusicPlayerExtensions.Messages;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Service
{
    public class AlbumInfoExtensionService
    {
        public AlbumInfoRequest GetRequest(ValueSet message)
        {
            AlbumInfoRequest request = null;

            if (message.ContainsKey(Commands.GetAlbumInfo))
            {
                string data = message[Commands.GetAlbumInfo] as string;
                request = JsonConvert.DeserializeObject<AlbumInfoRequest>(data);
            }
            return request;
        }

        public ValueSet PrepareResponse(AlbumInfoResponse response)
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
