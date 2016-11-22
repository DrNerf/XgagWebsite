﻿using System;
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DbContext.Dispose();
        }
    }
}