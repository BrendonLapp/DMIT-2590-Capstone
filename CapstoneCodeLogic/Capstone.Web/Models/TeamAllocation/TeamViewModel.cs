using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Capstone.BLL.DTOs;

namespace Capstone.Web.Models.TeamAllocation
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_TEAM controller
    /// </summary>
    public class TeamViewModel
    {
        [Key]
        public int TeamID { get; set; }
        [Requried]
        [Display(Name = "Unit Name")]
        public int UnitID { get; set; }
        [Required]
        [Display(Name = "Team Name")]
        [StringLength(30, ErrorMessage = "Team names cannot be longer than 30 characters.")]
        public string TeamName { get; set; }
        [Required]
        [Display(Name = "Activation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? ActivationDate { get; set; }
        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DeactivationDate { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }
        public virtual UnitDTO Unit { get; set; }
        [Display(Name = "Other Team Selection")]
        public int? PotentialTeamID { get; set; }

        [Display(Name = "Current Team Members")]
        public List<TeamAssignmentViewModel> CurrentTeam { get; set; }
        [Display(Name = "Potential Team Members")]
        public List<TeamAssignmentPotentialViewModel> PotentialTeam { get; set; }
        public List<UnsavedAssignmentDataClass> UnsavedAssignments { get; set; }
    }
}