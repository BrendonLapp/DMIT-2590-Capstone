using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    public class AdministrativeHubController : Controller
    {
        /// <summary>
        /// Get method to load the Index.cshtml page in the AdministrativeHub folder
        /// </summary>
        /// <returns>Empty view</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
            }
            return View();
        }
    }
}