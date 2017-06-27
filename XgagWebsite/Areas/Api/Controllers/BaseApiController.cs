using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XgagWebsite.Models;

namespace XgagWebsite.Areas.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public Guid? SessionToken { get; set; }

        protected ApplicationDbContext DbContext = new ApplicationDbContext();

        protected virtual ApplicationUser GetCurrentUser()
        {
            return DbContext.Users.First(u => u.ApiSessionToken == SessionToken);
        }

        /// <summary>
        /// Gets the base address.
        /// </summary>
        /// <returns>The base address.</returns>
        protected string GetBaseAddress()
        {
            return Request.RequestUri.GetLeftPart(UriPartial.Authority);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DbContext.Dispose();
        }
    }
}
