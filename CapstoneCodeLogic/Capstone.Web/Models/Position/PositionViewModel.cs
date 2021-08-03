using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.Position
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_POSITION
    /// </summary>
    public class PositionViewModel
    {

        public int PositionID { get; set; }
        [Required]
        [Display(Name = "Position Name")]
        [StringLength(30, ErrorMessage = "Position titles cannot be more than 30 characters long.")]
        public string PositionTitle { get; set; }
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Descriptions cannot be more than 50 characters long.")]
        public string Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Activation Date")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DeactivationDate { get; set; }
    }
}