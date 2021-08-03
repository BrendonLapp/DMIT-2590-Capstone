using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Unauthorized()
        {
            return View();
        }

        public ViewResult Forbidden()
        {
            return View();
        }
    }
}