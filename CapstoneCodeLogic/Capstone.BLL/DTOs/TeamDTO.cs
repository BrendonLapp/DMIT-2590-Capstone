using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_TEAM
    /// </summary>
    public class TeamDTO
    {
        public int TeamID { get; set; }
        public int UnitID { get; set; }
        public string TeamName { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public string UnitName { get; set; }
        public virtual UnitDTO Unit { get; set; }

        public List<UnsavedAssignmentDataClass> NewTeamMembers { get; set; }
    }
}
