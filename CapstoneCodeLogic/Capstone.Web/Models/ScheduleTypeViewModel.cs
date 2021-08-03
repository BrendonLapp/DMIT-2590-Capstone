using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_SCHEDULE_TYPE tables
    /// </summary>
    public class ScheduleTypeViewModel
    {
        [Key]
        public int ScheduleTypeID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "Schedule type names cannot be longer than 30 characters.")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Descriptions cannot be longer than 50 characters.")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Hours Per Day")]
        public decimal HoursPerDay { get; set; }
        [Required]
        [Display(Name = "Activation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DeactivationDate { get; set; }
    }
}