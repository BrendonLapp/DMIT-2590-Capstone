using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.BLL.DTOs.TeamCalendarDTOs;

namespace Capstone.Web.Models.TeamCalendar
{
    /// <summary>
    ///Data Transfer Object for information designed for TB_Capstone_TEAM_HISTORY && TB_Capstone_EMPLOYEE
    /// </summary>
    public class TeamCalendarTeamMemberModel
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<AbsencesDTO> Absences { get; set; }
    }
}