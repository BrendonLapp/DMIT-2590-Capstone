using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.PersonalProjects
{
    /// <summary>
    /// ViewModel for displaying and transfering personal projects information about the TB_Capstone_ALLOCATION controller in a list
    /// </summary>
    public class ListPersonalProjectsViewModel
    {
        [Key]
        public int AllocationID { get; set; }
        public int ProjectID { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int EmployeeID { get; set; }
        [Display(Name = "Allocated Days")]
        public decimal AllocatedDays { get; set; }
        [Display(Name = "Activation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DeactivationDate { get; set; }
        [Display(Name = "Start Date")]
        public int StartDate { get; set; }
    }
}