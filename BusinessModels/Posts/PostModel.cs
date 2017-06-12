using Newtonsoft.Json;

namespace BusinessModels
{
    /// <summary>
    /// Post Model.
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        [JsonProperty(PropertyName = "dateCreated")]
        public long DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the comments count.
        /// </summary>
        [JsonProperty(PropertyName = "commentsCount")]
        public int CommentsCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [any new comments].
        /// </summary>
        [JsonProperty(PropertyName = "anyNewComments")]
        public bool AnyNewComments { get; set; }

        /// <summary>
        /// Gets or sets you tube link.
        /// </summary>
        [JsonProperty(PropertyName = "youTubeLink")]
        public string YouTubeLink { get; set; }
    }
}
