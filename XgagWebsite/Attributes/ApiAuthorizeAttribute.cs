using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XgagWebsite.Areas.Api.Controllers;
using XgagWebsite.Models;

namespace XgagWebsite.Attributes
{
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        private const string SessionTokenHeaderName = "SessionToken";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            
            IEnumerable<string> headers;
            if (actionContext.Request.Headers.TryGetValues(SessionTokenHeaderName, out headers))
            {
                var token = Guid.Parse(headers.First());
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.ApiSessionToken.HasValue && u.ApiSessionToken == token);
                    if (user == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        var controller = actionContext.ControllerContext.Controller as BaseApiController;
                        if (controller != null)
                        {
                            controller.SessionToken = token;
                        }
                    }
                } 
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
    }
}