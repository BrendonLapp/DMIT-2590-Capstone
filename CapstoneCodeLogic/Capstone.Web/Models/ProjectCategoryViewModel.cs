using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information on the ProjectCategoryView pages
    /// </summary>
    public class ProjectCategoryViewModel
    {
        [Key]
        [Display(Name = "Project Category ID")]
        public int ProjectCategoryID { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(30, ErrorMessage = "Category names cannot be more than 30 characters long.")]
        public string CategoryName { get; set; }
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
        [Required]
        [Display(Name = "Color")]
        [StringLength(7, ErrorMessage = "Colors must be a hex value.")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Global")]
        public bool Global { get; set; }
    }
}