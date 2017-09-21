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

        public async Task<ActionResult> SearchUsers(string username)
        {
            var result = DbContext.Users.Select(u => u);
            if (!string.IsNullOrEmpty(username))
            {
                result = result.Where(u => u.UserName == username);
            }

            return JsonResult(result.ToList().Select(u => (ApplicationUserModel)u));
        }
    }
}