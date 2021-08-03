using System.Collections.Generic;
using Capstone.BLL.DTOs.ProjectAllocation;

namespace Capstone.Web.Models.ProjectAllocation
{
    /// <summary>
    /// View Model for the AllocationPartial. Used to display the all information across all projects and the current project for the selected year.
    /// </summary>
    public class AllocatedEmployeesViewModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int Year { get; set; }
        public int? AllocationID { get; set; }
        public List<AllocatedDaysDTO> allocatedDays { get; set; }
        public List<ProjectAllocatedDaysDTO> projectAllocation { get; set; }
    }
}