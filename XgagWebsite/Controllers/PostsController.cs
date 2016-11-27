using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.Helpers;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public class PostsController : BaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Upload(string title, HttpPostedFileBase uploadImage)
        {
            if (string.IsNullOrEmpty(title) || uploadImage == null)
            {
                throw new HttpException(400, "Invalid input data!");
            }

            var image = await ImagesHelper.SaveImageAsync(uploadImage, DbContext);
            var owner = DbContext.Users.First(u => u.UserName == User.Identity.Name);
            if (image == null)
            {
                throw new HttpException(500, "Invalid input data!");
            }

            var post = DbContext.Posts.Create();
            post.DateCreated = DateTime.Now;
            post.Title = title;
            post.Score = 0; //will be removed
            post.Image = image;
            post.Owner = owner;

            DbContext.Posts.Add(post);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult View(int id)
        {
            var post = DbContext.Posts.FirstOrDefault(p => p.PostId == id);

            if (post == null)
            {
                throw new HttpException(404, "Post not found :(");
            }

            return View(post);
        }
    }
}