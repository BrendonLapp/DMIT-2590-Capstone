using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.PersonalProjects
{
    /// <summary>
    /// View model for transfering the data on a project for a specific employee from the TB_Capstone_ALLOCATION table
    /// </summary>
    public class PersonalProjectBreakdownViewModel
    {
        [Key]
        public int AllocationID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Startdate { get; set; }
        [Display(Name = "Forecasted End Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? ForecastedEndDate { get; set; }
        public int Year { get; set; }
    }
}