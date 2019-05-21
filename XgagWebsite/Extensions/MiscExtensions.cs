using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace XgagWebsite
{
    public static class MiscExtensions
    {
        private const string m_YoutubeVideoIdRegex = @"embed/(.+)' frame";

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static string GetYoutubeThumbnail(this string iframe)
        {
            if (Regex.IsMatch(iframe, m_YoutubeVideoIdRegex))
            {
                var match = Regex.Match(iframe, m_YoutubeVideoIdRegex);
                if (match.Groups.Count > 1)
                {
                    return $"http://i3.ytimg.com/vi/{match.Groups[1].Value}/hqdefault.jpg";
                }
            }
            
            return iframe;
        }
    }
}