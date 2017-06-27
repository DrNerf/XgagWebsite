using Newtonsoft.Json;
using System;

namespace BusinessModels
{
    /// <summary>
    /// User model containing the session of the user and base info.
    /// </summary>
    /// <seealso cref="ApplicationUserModel" />
    public class SessionUserModel : ApplicationUserModel
    {
        [JsonProperty(PropertyName = "sessionToken")]
        public Guid SessionToken { get; set; }
    }
}
