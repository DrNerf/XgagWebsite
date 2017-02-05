using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XgagWebsite.AjaxResponses
{
    public class PeopleTableItemResponse
    {
        [JsonProperty(PropertyName = "personId")]
        public int PersonId { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "votes")]
        public int Votes { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "experience")]
        public int Experience { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }
    }
}