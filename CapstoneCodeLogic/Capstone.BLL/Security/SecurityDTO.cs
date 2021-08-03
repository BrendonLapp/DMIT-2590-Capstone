using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.Security
{
    /// <summary>
    /// Data Transfer Object to send user role information to the front end of the program, for use with authorization
    /// </summary>
    public class SecurityDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string UserID { get; set; }
        public int? TeamID { get; set; }
        public string TeamName { get; set; }
        public string RoleName { get; set; }
        public int? RoleID { get; set; }
    }
}
