using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using XgagWebsite.Helpers;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? page)
        {
            var pageSize = ConfigurationHelper.Instance.PageSize;
            var posts = DbContext.Posts
                .OrderByDescending(p => p.DateCreated)
                .Skip(pageSize * ((page ?? 1) - 1))
                .Take(pageSize)
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

            if (!String.IsNullOrEmpty(post.Title) && post.Image != null)
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