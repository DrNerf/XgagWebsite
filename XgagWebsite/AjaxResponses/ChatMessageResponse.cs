using System;
using Newtonsoft.Json;

namespace XgagWebsite.AjaxResponses
{
    public class ChatMessageResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "ownerUsername")]
        public string OwnerUsername { get; set; }

        [JsonProperty(PropertyName = "ownerAvatar")]
        public string OwnerAvatar { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "dateTimeSent")]
        public DateTime DateTimeSent { get; set; }

        [JsonProperty(PropertyName = "isOwned")]
        public bool IsOwned { get; set; }
    }
}