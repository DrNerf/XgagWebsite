using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessModels
{
    /// <summary>
    /// Post Rich Model.
    /// </summary>
    /// <seealso cref="BusinessModels.PostModel" />
    public class PostRichModel : PostModel
    {
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        public IEnumerable<CommentModel> Comments { get; set; }
    }
}
