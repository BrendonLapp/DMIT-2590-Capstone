using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.Security
{
    /// <summary>
    /// Strings for user role selection
    /// </summary>
    public static class SecurityRoles
    {
        public static string EmployeeRole { get { return "employee"; } }
        public static string SupervisorRole { get { return "supervisor"; } }
        public static string TeamAdminRole { get { return "teamAdmin"; } }
        public static string GlobalAdminRole { get { return "globalAdmin"; } }
    }
}
