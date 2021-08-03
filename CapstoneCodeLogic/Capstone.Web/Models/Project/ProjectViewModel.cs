using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.Project
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_PROJECT controller
    /// </summary>
    public class ProjectViewModel
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "Project Category")]
        public int ProjectCategoryID { get; set; }
        [Display(Name = "Project Category")]
        public string ProjectCategoryName { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Projected End Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ProjectedEndDate { get; set; }
        [Required]
        [Display(Name = "Activation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DeactivationDate { get; set; }
    }
}