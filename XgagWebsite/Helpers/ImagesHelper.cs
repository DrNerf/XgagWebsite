using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using XgagWebsite.Models;

namespace XgagWebsite.Helpers
{
    public static class ImagesHelper
    {
        private const string m_ImageUrlRegex = "(https?:)?//?[^'\" <>]+?\\.(jpg|jpeg|gif|png)";
        private const string m_ImageTagTemplate = "<img class=\"comment-image\" src=\"{0}\" />";

        public static async Task<Image> SaveImageAsync(HttpPostedFileBase image, ApplicationDbContext dbContext)
        {
            if (image.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(image.InputStream))
                {
                    imageData = binaryReader.ReadBytes(image.ContentLength);
                }
                var headerImage = dbContext.Images.Create();
                headerImage.Data = imageData;
                var dbImage = dbContext.Images.Add(headerImage);
                await dbContext.SaveChangesAsync();
                return dbImage;
            }

            return null;
        }

        public static bool IsImageUrl(string text)
        {
            return Regex.IsMatch(text, m_ImageUrlRegex);
        }

        public static string GetImageTag(string url)
        {
            return string.Format(m_ImageTagTemplate, url);
        }
    }
}