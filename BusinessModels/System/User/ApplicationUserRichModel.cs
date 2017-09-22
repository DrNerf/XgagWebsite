using Newtonsoft.Json;
using System;

namespace BusinessModels
{
    public class ApplicationUserRichModel : ApplicationUserModel
    {
        [JsonProperty(PropertyName = "isActivated")]
        public bool IsActivated { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "lastLogin")]
        public DateTime? LastLogin { get; set; }
    }
}
