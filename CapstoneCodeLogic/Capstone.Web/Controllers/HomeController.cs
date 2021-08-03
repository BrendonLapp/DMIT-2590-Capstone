using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Get method to load the Index.cshtml page in the Home folder
        /// </summary>
        /// <returns>Empty view</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.EmployeeRole))
            {
                Response.StatusCode = 403;
            }
            return View();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandling/Index.cshtml"
            };
        }
    }
}