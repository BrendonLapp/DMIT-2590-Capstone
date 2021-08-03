using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs.TeamCalendarDTOs
{    /// <summary>
     /// Data Transfer Object for information designed for TB_Capstone_ABSENCE_DETAIL 
     /// </summary>
    public class AbsencesDTO
    {
        public int EmployeeID { get; set; }
        public int AbsenceID { get; set; }
        public int OffDayID { get; set; }
        public string Name { get; set; }
        public string OffDayAbbreviatedName { get; set; }
        public string HalfDay { get; set; }
        public DateTime AbsenceDate { get; set; }
        public decimal? Hours { get; set; }
        public string Notes { get; set; }
        public string Color { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
    }
}
