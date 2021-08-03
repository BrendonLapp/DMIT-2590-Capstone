using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.Employee
{
    /// <summary>
    /// View Model for Employee CRUD pages, with the names of related pages
    /// </summary>
    public class ListEmployeeViewModel : EmployeeViewModel
    {
        [Display(Name = "Team")]
        public string TeamName { get; set; }

        [Display(Name = "Position")]
        public string PositionTitle { get; set; }

        [Display(Name = "Role")]
        public string RoleTitle { get; set; }

        [Display(Name = "Schedule Type")]
        public string ScheduleTypeName { get; set; }
    }
}