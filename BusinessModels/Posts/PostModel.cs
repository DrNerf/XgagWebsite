using Newtonsoft.Json;
using System;

namespace BusinessModels
{
    public class PostModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "score")]
        public int Score { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty(PropertyName = "commentsCount")]
        public int CommentsCount { get; set; }

        [JsonProperty(PropertyName = "anyNewComments")]
        public bool AnyNewComments { get; set; }

        [JsonProperty(PropertyName = "youTubeLink")]
        public string YouTubeLink { get; set; }
    }
}
