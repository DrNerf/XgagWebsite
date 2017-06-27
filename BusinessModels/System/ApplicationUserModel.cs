using Newtonsoft.Json;

namespace BusinessModels
{
    /// <summary>
    /// Application User model.
    /// </summary>
    public class ApplicationUserModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }
    }
}
