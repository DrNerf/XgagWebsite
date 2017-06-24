using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using XgagWebsite.Models;

namespace XgagWebsite.Areas.Api.Controllers
{
    [RoutePrefix("api")]
    public class AuthorizationController : ApiController
    {
        [HttpPost]
        [Route("Auth/Login")]
        [ResponseType(typeof(IEnumerable<Cookie>))]
        public async Task<IHttpActionResult> Login(LoginViewModel viewModel)
        {
            viewModel.RememberMe = true;
            var results = Enumerable.Empty<Cookie>();
            try
            {
                var cookieJar = new CookieContainer();
                using (var handler = new HttpClientHandler())
                {
                    handler.CookieContainer = cookieJar;
                    using (HttpClient client = new HttpClient(handler))
                    {
                        var result = await client.PostAsJsonAsync<LoginViewModel>(
                            $"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/Account/Login",
                            viewModel);
                        results = cookieJar.GetCookies(new System.Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority)))
                            .Cast<Cookie>();

                        if (!results.Any())
                        {
                            throw new Exception("Could not retrieve the authorization tokens.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            return Ok(results);
        }
    }
}
