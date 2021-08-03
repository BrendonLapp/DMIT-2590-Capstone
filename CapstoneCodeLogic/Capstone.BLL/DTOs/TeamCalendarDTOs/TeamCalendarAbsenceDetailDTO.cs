using System;
using System.Collections.Generic;
namespace Capstone.BLL.DTOs.TeamCalendarDTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_EMPLOYEE && TB_Capstone_OFFDAY_TYPE 
    /// </summary>
    public class TeamCalendarAbsenceDetailDTO
    {
        public int AbsenceID { get; set; }
        public int OffDayID { get; set; }
        public string OffDayAbbreviatedName { get; set; }
        public string HalfDay { get; set; }
        public DateTime AbsenceDate { get; set; }
        public decimal? Hours { get; set; }
        public string Notes { get; set; }
        public string Color { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeRole { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
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
