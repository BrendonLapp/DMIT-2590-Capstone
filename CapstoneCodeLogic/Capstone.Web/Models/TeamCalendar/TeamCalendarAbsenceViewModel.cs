using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.TeamCalendar
{
    /// <summary>
    ///Data Transfer Object for information designed for TB_Capstone_ABSENCE_DETAIL FOR TEAM CALENDAR EDITS AND CREATES
    /// </summary>
    public class TeamCalendarAbsenceViewModel
    {
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        public int AbsenceID { get; set; }
        [Required]
        [Display(Name = "Absence Type")]
        public int OffDayID { get; set; }
        [Display(Name = "Half Day")]
        [StringLength(2, ErrorMessage = "Half day must be set to 'AM' or 'PM'")]
        public string HalfDay { get; set; }
        [Display(Name = "Absence Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AbsenceDate { get; set; }
        [Required]
        [Display(Name = "Hours")]
        public decimal? Hours { get; set; }
        [Display(Name = "Notes")]
        [StringLength(100, ErrorMessage = "Notes cannot be more than 100 characters long.")]
        public string Notes { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

    }
}