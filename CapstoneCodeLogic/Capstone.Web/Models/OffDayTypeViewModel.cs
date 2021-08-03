using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_OFFDAY_TYPE table
    /// </summary>
    public class OffDayTypeViewModel
    {
        [Key]
        [Display(Name = "Off Day ID")]
        public int OffDayID { get; set; }

        [Required]
        [Display(Name = "Abbreviated Name")]
        [StringLength(15, ErrorMessage = "Abbreviated name cannot be more than 15 characters long.")]
        public string AbbreviatedName { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Descriptions cannot be more than 50 characters long.")]
        public string Description { get; set; }

        /// <summary>
        /// Marks whether or not this offday type is considered a form of paid time off
        /// </summary>
        [Required]
        [Display(Name = "Paid Time Off")]
        public bool PTO { get; set; }

        [Display(Name = "Notes")]
        [StringLength(100, ErrorMessage = "Notes cannot be more than 100 characters long.")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string ColorDisplayed { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Activation Date")]
        public DateTime ActivationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deactivation Date")]
        public DateTime? DeactivationDate { get; set; }
    }
}