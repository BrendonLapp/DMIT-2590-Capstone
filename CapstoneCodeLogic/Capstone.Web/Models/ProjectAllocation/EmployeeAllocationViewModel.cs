using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ProjectAllocation
{
    /// <summary>
    /// View Model for the TeamPartial. Used to display the information of team members.
    /// </summary>
    public class EmployeeAllocationViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public int TeamID { get; set; }
        public int RoleID { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public int PositionID { get; set; }
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}