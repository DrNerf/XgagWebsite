using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using XgagWebsite.Models;

namespace XgagWebsite.Helpers
{
    public static class SlackHelper
    {
        private const string MessageTemplate = "Howdy, a new post has been uploaded to {0}. Check it out <{1}|here>!";

        public static Task SendNotification(Post post, string websiteUrl)
        {
            if (!string.IsNullOrEmpty(ConfigurationHelper.Instance.SlackHook))
            {
                var payload = new SlackPayload
                {
                    Text = string.Format(
                            MessageTemplate,
                            ConfigurationHelper.Instance.ProductName,
                            $"{websiteUrl}/Posts/View/{post.PostId}"),
                    Username = ConfigurationHelper.Instance.ProductName,
                    Icon = ConfigurationHelper.Instance.ProductFavicon
                };

                Attachment attachment = null;
                if (post.Image != null)
                {
#if DEBUG
                    // Slack won't display images from "localhost". Jeez...
                    attachment = new Attachment(
                        "Preview",
                        ConfigurationHelper.Instance.ProductLogo,
                        post.Title);
#else
                    attachment = new Attachment(
                        "Preview",
                        $"{websiteUrl}/jpg/Images/View/{post.Image.ImageId}.jpg",
                        post.Title);
#endif
                }
                else
                {
                    attachment = new Attachment(
                        "Preview",
                        post.YouTubeLink.GetYoutubeThumbnail(),
                        post.Title,
                        "This is a youtube video",
                        "https://cdn1.iconfinder.com/data/icons/google_jfk_icons_by_carlosjj/512/youtube.png");
                }

                if (attachment != null)
                {
                    payload.Attachments.Add(attachment);
                }

                var client = new HttpClient();
                var result = client.PostAsJsonAsync(ConfigurationHelper.Instance.SlackHook, payload).Result;
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

            [JsonProperty("attachments")]
            public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
        }

        private class Attachment
        {
            [JsonProperty("fallback")]
            public string Fallback { get; set; }

            [JsonProperty("image_url")]
            public string ImageUrl { get; set; }

            [JsonProperty("pretext")]
            public string PreText { get; set; }

            [JsonProperty("footer")]
            public string Footer { get; set; }

            [JsonProperty("footer_icon")]
            public string FooterIcon { get; set; }

            public Attachment(string fallback, string imageUrl, string preText)
            {
                Fallback = fallback;
                ImageUrl = imageUrl;
                PreText = preText;
            }

            public Attachment(string fallback, string imageUrl, string preText, string footer, string footerIcon)
                : this(fallback, imageUrl, preText)
            {
                Footer = footer;
                FooterIcon = footerIcon;
            }
        }
    }
}