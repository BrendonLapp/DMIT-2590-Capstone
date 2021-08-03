﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.BLL.Exceptions;
using Capstone.Web.Controllers;


namespace Capstone.Web
{
    /// <summary>
    /// Autogenerated Application Class
    /// </summary>
    /// <remarks>
    /// CREATED BY Gregory Chyczij 3/26/2019
    /// </remarks>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Auto Generated
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// On Authentication Request Method, Gets currently logged in information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_OnPostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (!Request.Path.Equals("/Error/Unauthorized") && !Request.Path.Equals("~/Error/Forbidden"))
            {
                try
                {
                    var controller = new SecurityController();
                    SecurityDTO loggedInUser = controller.LookupSecurityInformation();
                    var claimID = new ClaimsIdentity();

                    switch (loggedInUser.RoleID)
                    {
                        case 1:
                            AddEmployeeRole(ref claimID);
                            break;
                        case 2:
                            AddEmployeeRole(ref claimID);
                            AddSupervisorRole(ref claimID);
                            break;
                        case 3:
                            AddEmployeeRole(ref claimID);
                            AddTeamAdminRole(ref claimID);
                            break;
                        case 4:
                            AddEmployeeRole(ref claimID);
                            AddTeamAdminRole(ref claimID);
                            AddGlobalAdminRole(ref claimID);
                            break;
                        default:
                            AddEmployeeRole(ref claimID);
                            break;
                    }

                    claimID.AddClaim(new Claim(CustomClaimTypes.EmployeeID, loggedInUser.EmployeeID.ToString(), ClaimValueTypes.Integer));
                    claimID.AddClaim(new Claim(CustomClaimTypes.EmployeeName, loggedInUser.EmployeeName));
                    claimID.AddClaim(new Claim(CustomClaimTypes.RoleTitle, loggedInUser.RoleName));
                    claimID.AddClaim(new Claim(CustomClaimTypes.TeamName, loggedInUser.TeamName));
                    claimID.AddClaim(new Claim(CustomClaimTypes.TeamID, loggedInUser.TeamID.ToString(), ClaimValueTypes.Integer));
                    claimID.AddClaim(new Claim(CustomClaimTypes.RoleID, loggedInUser.RoleID.ToString(), ClaimValueTypes.Integer));

                    ClaimsPrincipal.Current.AddIdentity(claimID);
                }
                catch (UserNotFoundException)
                {
                    Response.ClearContent();
                    Response.StatusCode = 401;
                    Response.Redirect("~/Error/Unauthorized");
                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }

        /// <summary>
        /// On Page End Method, redirects to error if forbidden user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Response.StatusCode == 403)
            {
                Response.ClearContent();
                Response.Redirect("~/Error/Forbidden");
            }
        }

        private void AddEmployeeRole(ref ClaimsIdentity claimID)
        {
            var employeeClaim = new Claim(ClaimTypes.Role, SecurityRoles.EmployeeRole);
            claimID.AddClaim(employeeClaim);
        }

        private void AddSupervisorRole(ref ClaimsIdentity claimID)
        {
            var supervisorClaim = new Claim(ClaimTypes.Role, SecurityRoles.SupervisorRole);
            claimID.AddClaim(supervisorClaim);
        }

        private void AddTeamAdminRole(ref ClaimsIdentity claimID)
        {
            var teamAdminClaim = new Claim(ClaimTypes.Role, SecurityRoles.TeamAdminRole);
            claimID.AddClaim(teamAdminClaim);
        }

        private void AddGlobalAdminRole(ref ClaimsIdentity claimID)
        {
            var globalAdminClaim = new Claim(ClaimTypes.Role, SecurityRoles.GlobalAdminRole);
            claimID.AddClaim(globalAdminClaim);
        }

    }
}
