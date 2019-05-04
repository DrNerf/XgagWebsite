using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext DbContext = new ApplicationDbContext();

        protected virtual ApplicationUser GetCurrentUser()
        {
            return DbContext.Users.First(u => u.UserName == User.Identity.Name);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DbContext.Dispose();
        }

        protected ActionResult JsonResult(object result)
        {
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        protected string GetCurrentWebsiteRoot()
        {
            return Request.Url.GetLeftPart(UriPartial.Authority);
        }
    }
}