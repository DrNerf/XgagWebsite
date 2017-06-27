using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity.Owin;
using XgagWebsite.Models;
using Microsoft.Owin;
using XgagWebsite.Attributes;
using BusinessModels;

namespace XgagWebsite.Areas.Api.Controllers
{
    /// <summary>
    /// Authorization Controller.
    /// </summary>
    /// <seealso cref="BaseApiController" />
    [RoutePrefix("api/Auth")]
    public class AuthorizationController : BaseApiController
    {
        private ApplicationSignInManager m_SignInManager;

        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return m_SignInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                m_SignInManager = value;
            }
        }

        /// <summary>
        /// Logs in the specified user model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <seealso cref="SessionUserModel" />
        /// <returns>User model.</returns>
        [HttpPost]
        [Route("Login")]
        [ResponseType(typeof(SessionUserModel))]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            model.RememberMe = false;
            var token = Guid.NewGuid();
            var response = new SessionUserModel() { SessionToken = token };
            try
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                var user = DbContext.Users.First(u => u.UserName.ToLower() == model.UserName.ToLower());
                user.ApiSessionToken = token;
                await DbContext.SaveChangesAsync();
                response.Avatar = user.GetProfilePictureUrl(GetBaseAddress());
                response.Username = user.UserName;
                response.Id = user.Id;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            return Ok(response);
        }

        /// <summary>
        /// Logs off this instance.
        /// Requires SessionToken header.
        /// </summary>
        /// <remarks>
        /// Requires SessionToken header.
        /// </remarks>
        /// <returns>
        /// OK(200) if the operation is successful.
        /// </returns>
        [HttpPost]
        [Route("Logoff")]
        [ApiAuthorize]
        public async Task<IHttpActionResult> Logoff()
        {
            try
            {
                var user = GetCurrentUser();
                user.ApiSessionToken = default(Guid?);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }
    }
}
