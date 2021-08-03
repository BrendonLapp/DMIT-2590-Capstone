using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Capstone.BLL.DTOs;

namespace Capstone.Web.Models
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_UNIT controller
    /// </summary>
    public class UnitViewModel
    {
        [Key]
        [Display(Name = "Unit ID")]
        public int UnitID { get; set; }
        [Required]
        [Display(Name = "Area Name")]
        public int AreaID { get; set; }
        [Required]
        [Display(Name = "Unit Name")]
        [StringLength(30, ErrorMessage = "Unit names cannot be longer that 30 characters.")]
        public string UnitName { get; set; }
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Descriptions cannot be longer than 50 characters.")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Activation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DeactivationDate { get; set; }
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }
        [Display(Name = "Area Name")]
        public virtual AreaDTO Area { get; set; }
    }
}