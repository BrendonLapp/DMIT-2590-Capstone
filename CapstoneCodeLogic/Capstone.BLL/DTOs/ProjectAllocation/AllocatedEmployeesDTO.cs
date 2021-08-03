using System.Collections.Generic;

namespace Capstone.BLL.DTOs.ProjectAllocation
{
    /// <summary>
    /// Data Transfer Object for the AllocationPartial. Used to display the all information across all projects and the current project for the selected year.
    /// </summary>
    public class AllocatedEmployeesDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int Year { get; set; }
        public int? AllocationID { get; set; }
        public List<AllocatedDaysDTO> allocatedDays { get; set; }
        public List<ProjectAllocatedDaysDTO> projectDays { get; set; }
    }
}
