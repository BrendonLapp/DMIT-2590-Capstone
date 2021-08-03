using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.PaidHoliday
{
    /// <summary>
    ///Data Transfer Object for information designed for TB_Capstone_PAID_HOLIDAY
    /// </summary>
    public class PaidHolidayViewModel
    {
        public int PaidHolidayID { get; set; }
        [Required]
        [Display(Name = "Holiday Name")]
        [StringLength(50, ErrorMessage = "Holiday names cannot be more than 50 characters long.")]
        public string HolidayName { get; set; }
        [Display(Name = "Holiday Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime HolidayDate { get; set; }
        [Display(Name = "Notes")]
        [StringLength(100, ErrorMessage = "Notes cannot be more than 100 characters long.")]
        public string Notes { get; set; }
        [Display(Name = "Activation Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DeactivationDate { get; set; }



    }
}