using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using XgagWebsite.Models;

namespace XgagWebsite.Helpers
{
    public static class ImagesHelper
    {
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
    }
}