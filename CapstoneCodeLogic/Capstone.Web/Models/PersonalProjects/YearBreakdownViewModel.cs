using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.PersonalProjects
{
    /// <summary>
    /// ViewModel for transfering personal projects breakdown per calendar year from TB_Capstone_ALLOCATION table
    /// </summary>
    public class YearBreakdownViewModel
    {
        [Key]
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public int Year { get; set; }
        public decimal January { get; set; }
        public decimal Febuary { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }
    }
}