using Newtonsoft.Json;
using System.Collections.Generic;
using XgagWebsite.Models;

namespace XgagWebsite.AjaxResponses
{
    public class QuoteItemResponse
    {
        [JsonProperty(PropertyName = "quoteId")]
        public int QuoteId { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "authorImage")]
        public string AuthorImage { get; set; }

        public static QuoteItemResponse Transform(Quote quote)
        {
            var author = quote.AnonymousAuthor;
            if (quote.Author != null)
            {
                author = string.Format("{0} {1}", quote.Author.FirstName, quote.Author.LastName);
            }

            return new QuoteItemResponse()
            {
                Author = author,
                AuthorImage = quote.Author?.Image,
                QuoteId = quote.QuoteId,
                Text = quote.Text
            };
        }
    }
}