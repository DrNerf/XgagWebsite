using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using XgagWebsite.Models;

namespace XgagWebsite.Helpers
{
    public static class SlackHelper
    {
        private const string MessageTemplate = "Howdy, a new post has been uploaded to {0}. Check it out <{1}|here>!";

        public static Task SendNotification(int postId, string websiteUrl)
        {
            if (!string.IsNullOrEmpty(ConfigurationHelper.Instance.SlackHook))
            {
                var payload = new SlackPayload
                {
                    Text = string.Format(
                            MessageTemplate,
                            ConfigurationHelper.Instance.ProductName,
                            $"{websiteUrl}/Posts/View/{postId}"),
                    Username = ConfigurationHelper.Instance.ProductName,
                    Icon = ConfigurationHelper.Instance.ProductFavicon
                };

                var client = new HttpClient();
                return client.PostAsJsonAsync(ConfigurationHelper.Instance.SlackHook, payload); 
            }

            return null;
        }

        private class SlackPayload
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("icon_url")]
            public string Icon { get; set; }
        }
    }
}