using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Capstone.Web.Admin
{
    /// <summary>
    /// helper method for getting information from custom claims
    /// </summary>
    /// <remarks>
    /// CREATED BY: GREGORY CHYCZIJ 04/10/2019
    /// </remarks>
    public static class IdentityHelper
    {
        /// <summary>
        /// Gets Custom Claim
        /// </summary>
        /// <returns>Employee ID</returns>
        public static int GetEmployeeID()
        {
            var claimsPrincipal = ClaimsPrincipal.Current;
            Claim employeeIDClaim = claimsPrincipal?.FindFirst(CustomClaimTypes.EmployeeID);

            if (employeeIDClaim == null)
            {
                return 0;
            }

            return int.Parse(employeeIDClaim.Value);
        }

        /// <summary>
        /// Gets Custom Claim
        /// </summary>
        /// <returns>Employee Name</returns>
        public static string GetEmployeeName()
        {
            var claimsPrincipal = ClaimsPrincipal.Current;
            Claim employeeNameClaim = claimsPrincipal?.FindFirst(CustomClaimTypes.EmployeeName);

            if (employeeNameClaim == null)
            {
                return "Error";
            }

            return employeeNameClaim.Value;
        }

        /// <summary>
        /// Gets Custom Claim
        /// </summary>
        /// <returns>Role Title</returns>
        public static string GetRoleTitle()
        {
            var claimsPrincipal = ClaimsPrincipal.Current;
            Claim roleTitleClaim = claimsPrincipal.FindFirst(CustomClaimTypes.RoleTitle);

            if (roleTitleClaim == null)
            {
                return "Error";
            }
            string value = roleTitleClaim.Value;
            return roleTitleClaim.Value;
        }

        /// <summary>
        /// Gets Custom Claim
        /// </summary>
        /// <returns>Team ID</returns>
        public static int GetTeamID()
        {
            var claimsPrincipal = ClaimsPrincipal.Current;
            Claim teamIDClaim = claimsPrincipal?.FindFirst(CustomClaimTypes.TeamID);

            if (teamIDClaim == null)
            {
                return 0;
            }

            return int.Parse(teamIDClaim.Value);
        }

        /// <summary>
        /// Gets Custom Claim
        /// </summary>
        /// <returns>Team Name</returns>
        public static string GetTeamName()
        {
            var claimsPrincipal = ClaimsPrincipal.Current;
            Claim teamNameClaim = claimsPrincipal?.FindFirst(CustomClaimTypes.TeamName);

            if (teamNameClaim == null)
            {
                return "Error";
            }

            return teamNameClaim.Value;
        }

        /// <summary>
        /// Gets Custom Claim
        /// </summary>
        /// <returns>Role ID</returns>
        public static int GetRoleID()
        {
            var claimsPrincipal = ClaimsPrincipal.Current;
            Claim roleIDClaim = claimsPrincipal?.FindFirst(CustomClaimTypes.RoleID);

            if (roleIDClaim == null)
            {
                return 0;
            }

            return int.Parse(roleIDClaim.Value);
        }
    }
}