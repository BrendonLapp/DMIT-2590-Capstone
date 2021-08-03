using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.PersonalProjects
{
    /// <summary>
    /// DTO for transfering personal projects information from the TB_Capstone_Allocation table to be used in a list
    /// </summary>
    public class PersonalProjectDTO
    {
        [Key]
        public int AllocationID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int EmployeeID { get; set; }
        public decimal AllocatedDays { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public int StartDate { get; set; }
    }
}
