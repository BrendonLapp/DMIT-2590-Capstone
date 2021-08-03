using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_DEPARTMENT controller
    /// </summary>
    public class DepartmentViewModel
    {
        [Key]
        [Display(Name = "Department ID")]
        public int DepartmentID { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        [StringLength(30, ErrorMessage = "Department names cannot be more that 30 characters long.")]
        public string DepartmentName { get; set; }
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Descriptions cannot be more than 50 characters long.")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Activation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DeactivationDate { get; set; }

    }
}