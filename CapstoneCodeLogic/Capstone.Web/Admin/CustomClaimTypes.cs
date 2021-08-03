using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Admin
{
    /// <summary>
    /// Stores strings for custom claims, allowing the claims to be sycronized for calls
    /// </summary>
    /// <remarks>
    /// CREATED BY: GREGORY CHYCZIJ 04/10/2019
    /// </remarks>
    public static class CustomClaimTypes
    {
        public static string EmployeeID { get { return "EmployeeID"; } }
        public static string EmployeeName { get { return "EmployeeName"; } }
        public static string RoleTitle { get { return "RoleTitle"; } }
        public static string TeamName { get { return "TeamName"; } }
        public static string TeamID { get { return "TeamID"; } }
        public static string RoleID { get { return "RoleID"; } }
    }
}