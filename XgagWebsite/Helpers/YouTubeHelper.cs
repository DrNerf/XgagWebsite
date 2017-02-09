using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace XgagWebsite.Helpers
{
    public static class YouTubeHelper
    {
        private const string YouTubeAddressRegex = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
        const string YouTubeEmbedHtml = "<iframe title='YouTube video player' class='post-image' height='400' src='http://www.youtube.com/embed/$1' frameborder='0' allowfullscreen='1'></iframe>";

        public static bool TryGetEmbedHtml(string link, out string html)
        {
            var rgx = new Regex(YouTubeAddressRegex);
            html = rgx.Replace(link, YouTubeEmbedHtml);

            return html != YouTubeEmbedHtml;
        }
    }
}