using System;
using System.ComponentModel.DataAnnotations;
using Capstone.BLL.DTOs;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_AREA controller
    /// </summary>
    public class AreaViewModel
    {
        [Key]
        [Display(Name = "Area ID")]
        public int AreaID { get; set; }
        [Required]
        [Display(Name = "Department Name")] //Needs to be named this for the drop down partial in edit.cshtml
        public int DepartmentID { get; set; }
        [Required]
        [Display(Name = "Area Name")]
        [StringLength(30, ErrorMessage = "Area names cannot be more than 30 characters long.")]
        public string AreaName { get; set; }
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
        [Display(Name = "Department Name")]
        //Dose not require string length, always inside of a dropdownlist
        public string DepartmentName { get; set; }
        public virtual DepartmentDTO Department { get; set; }


    }
}