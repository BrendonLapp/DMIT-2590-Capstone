using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.TeamAllocation
{
    /// <summary>
    /// View Model for the Team Assignment partial views in Team Management
    /// </summary>
    public class TeamAssignmentViewModel
    {
        [Key]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Display(Name = "Team")]
        public int TeamID { get; set; }

        [Display(Name = "Role")]
        public int RoleID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Current Team")]
        public string TeamName { get; set; }

        [Display(Name = "Position")]
        public string PositionTitle { get; set; }

        [Display(Name = "Role")]
        public string RoleTitle { get; set; }

        public bool IsUnsaved { get; set; }

        [Display(Name = "Employee Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}