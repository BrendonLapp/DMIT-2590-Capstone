using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.PersonalProjects
{
    /// <summary>
    /// DTO for transfering the data on a project for a specific employee from the TB_Capstone_ALLOCATION table
    /// </summary>
    public class PersonalProjectBreakdownDTO
    {
        [Key]
        public int AllocationID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public string ProjectName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime? ForecastedEndDate { get; set; }
        public int Year { get; set; }
    }
}
