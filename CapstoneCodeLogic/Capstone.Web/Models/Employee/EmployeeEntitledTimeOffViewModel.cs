using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.Employee
{
    /// <summary>
    /// View Model for entitled time off creation, stores an entire employee's entitled time off
    /// </summary>
    public class EmployeeEntitledTimeOffViewModel
    {
        public int EmployeeID { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Entitled Time Off")]
        public List<EntitledTimeOffViewModel> EmployeeEntitlements { get; set; }

        public EmployeeEntitledTimeOffViewModel()
        {
            EmployeeEntitlements = new List<EntitledTimeOffViewModel>();
        }
    }
}