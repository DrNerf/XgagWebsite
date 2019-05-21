using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.Helpers;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public class ImagesController : BaseController
    {
        public ActionResult View(int id)
        {
            var item = DbContext.Images.FirstOrDefault(i => i.ImageId == id);
            if (item != null)
            {
                byte[] buffer = item.Data;
                return new FileStreamResult(new MemoryStream(buffer), "image/jpg"); 
            }

            throw new HttpException(404, "Whoops, Image not found.");
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase[] uploadImages)
        {
            if (uploadImages != null && uploadImages.Any())
            {
                List<int> ids = new List<int>();
                foreach (var image in uploadImages)
                {
                    var dbimage = await ImagesHelper.SaveImageAsync(image, DbContext);

                    if (dbimage != null)
                    {
                        ids.Add(dbimage.ImageId);
                    }
                    else
                    {
                        throw new HttpException(500, "Whoops.");
                    }
                }

                return Json(new { IsSuccess = true, Ids = ids });
            }

            throw new HttpException(400, "No image provided.");
        }
    }
}