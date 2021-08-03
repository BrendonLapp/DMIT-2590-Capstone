using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_OVERTIME_TYPE tables
    /// </summary>
    public class OvertimeTypeViewModel
    {
        [Key]
        [Display(Name = "Overtime Type ID")]
        public int OvertimeTypeID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Overtime Type names cannot be more than 20 characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Pay Multiplier")]
        public decimal PayMultiplier { get; set; }

        [StringLength(50, ErrorMessage = "Descriptions cannot be more than 50 characters long.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Activation Date")]
        public DateTime ActivationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deactivation Date")]
        public DateTime? DeactivationDate { get; set; }
    }
}