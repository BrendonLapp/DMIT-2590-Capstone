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
    public class PositionViewModelEdit
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Activation Date")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DeactivationDate { get; set; }
        [Display(Name = "Created By")]
        [StringLength(50, ErrorMessage = "Created by cannot be more than 50 characters long.")]
        public string CreatedBy { get; set; }
        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Updated By")]
        [StringLength(50, ErrorMessage = "Updated by cannot be more than 50 characters long.")]
        public string UpdatedBy { get; set; }
        [Display(Name = "Updated Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? UpdatedDate { get; set; }
    }
}