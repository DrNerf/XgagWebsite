using BusinessModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using XgagWebsite.Helpers;

namespace XgagWebsite.Controllers
{
    [Authorize(Roles = RolesHelper.AdminRole)]
    public class AdminController : BaseController
    {
        public ActionResult UsersPanel()
        {
            return View();
        }

        public async Task<ActionResult> SearchUsers(
            string username,
            string email,
            bool? isActivated)
        {
            var result = DbContext.Users.Select(u => u);
            if (!string.IsNullOrEmpty(username))
            {
                result = result.Where(u => u.UserName == username);
            }

            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(u => u.Email == email);
            }

            if (isActivated.HasValue)
            {
                result = result.Where(u => u.IsActivated == isActivated.Value);
            }

            return JsonResult(result.ToList().Select(u => (ApplicationUserRichModel)u));
        }
    }
}