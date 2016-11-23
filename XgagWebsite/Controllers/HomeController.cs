using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var posts = DbContext.Posts
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UploadPost(Post post)
        {
            var isSuccess = false;
            var isValid = false;
            string message = string.Empty;

            if (!String.IsNullOrEmpty(post.Title) && !String.IsNullOrEmpty(post.ImageUrl))
            {
                isValid = true;
            }
            else
            {
                message = "Invalid data!";
            }

            if (isValid)
            {
                try
                {
                    post.DateCreated = DateTime.Now;
                    post.Score = 0;
                    DbContext.Posts.Add(post);
                    await DbContext.SaveChangesAsync();
                    isSuccess = true;
                }
                catch (Exception ex) 
                {
                    message = ex.Message;
                }
            }

            return Json(new { IsSuccess = isSuccess, Message = message });
        }
    }
}