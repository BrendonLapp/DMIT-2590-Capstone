using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.ProjectAllocation
{
    /// <summary>
    /// Data Transfer Object for the TeamPartial. Used to display the information of team members.
    /// </summary>
    public class EmployeeAllocationDTO
    {
        [Key]
        public int EmployeeID { get; set; }
        public int TeamID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
