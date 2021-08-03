using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ProjectAllocation
{
    /// <summary>
    /// View Model for the AllocationPartial. Used to display the information of the currently selected project.
    /// </summary>
    public class ProjectAllocatedDaysViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public int Year { get; set; }
        public int? AllocationID { get; set; }
        [Required]
        public decimal ProjectJanuary { get; set; }
        [Required]
        public decimal ProjectFebruary { get; set; }
        [Required]
        public decimal ProjectMarch { get; set; }
        [Required]
        public decimal ProjectApril { get; set; }
        [Required]
        public decimal ProjectMay { get; set; }
        [Required]
        public decimal ProjectJune { get; set; }
        [Required]
        public decimal ProjectJuly { get; set; }
        [Required]
        public decimal ProjectAugust { get; set; }
        [Required]
        public decimal ProjectSeptember { get; set; }
        [Required]
        public decimal ProjectOctober { get; set; }
        [Required]
        public decimal ProjectNovember { get; set; }
        [Required]
        public decimal ProjectDecember { get; set; }
    }
}