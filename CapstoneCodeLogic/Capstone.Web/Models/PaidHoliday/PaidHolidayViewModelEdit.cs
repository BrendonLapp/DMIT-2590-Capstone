using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.PaidHoliday
{
    public class PaidHolidayViewModelEdit
    {
        [Key]
        public int PaidHolidayID { get; set; }
        [Required]
        [Display(Name = "Holiday Name")]
        [StringLength(50, ErrorMessage = "Holiday names cannot be more than 50 characters long.")]
        public string HolidayName { get; set; }
        [Required]
        [Display(Name = "Holiday Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime HolidayDate { get; set; }
        [Display(Name = "Notes")]
        [StringLength(100, ErrorMessage = "Notes cannot be  more than 100 characters long.")]
        public string Notes { get; set; }
        [Required]
        [Display(Name = "Activation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DeactivationDate { get; set; }
        [Required]
        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }
        [Display(Name = "Creation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Updated By")]
        public int UpdatedBy { get; set; }
        [Display(Name = "Updated Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? UpdatedDate { get; set; }
    }
}