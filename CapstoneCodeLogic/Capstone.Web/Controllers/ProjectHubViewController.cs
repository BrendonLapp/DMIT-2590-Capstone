using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    public class ProjectHubViewController : Controller
    {
        /// <summary>
        /// Get method to load the Index.cshtml page in the ProjectHub folder
        /// </summary>
        /// <returns>Empty view</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
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