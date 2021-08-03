using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.ProjectAllocation
{
    /// <summary>
    /// Data Transfer Object for the AllocationPartial. Used to display the information of the currently selected project.
    /// </summary>
    public class ProjectAllocatedDaysDTO
    {
        [Key]
        public int EmployeeID { get; set; }
        public int Year { get; set; }
        public int? AllocationID { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectJanuary { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectFebruary { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectMarch { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectApril { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectMay { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectJune { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectJuly { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectAugust { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectSeptember { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectOctober { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectNovember { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal ProjectDecember { get; set; }
    }
}
