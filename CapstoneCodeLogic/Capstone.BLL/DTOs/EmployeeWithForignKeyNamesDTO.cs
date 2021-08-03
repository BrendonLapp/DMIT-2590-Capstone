using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Extension of EmployeeDTO, includes more info from related tables
    /// </summary>
    public class EmployeeWithForignKeyNamesDTO : EmployeeDTO
    {
        public string RoleTitle { get; set; }

        public string PositionTitle { get; set; }

        public string TeamName { get; set; }

        public string ScheduleTypeName { get; set; }
    }
}
