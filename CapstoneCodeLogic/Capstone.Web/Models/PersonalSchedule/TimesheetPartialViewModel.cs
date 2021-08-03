using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Capstone.BLL.DTOs;


namespace Capstone.Web.Models.PersonalSchedule
{
    public class TimesheetPartialViewModel
    {

        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }
        [Display(Name = "End Time")]
        public string EndTime { get; set; }
        [Display(Name = "Select a project")]
        public KeyValueDTO Project { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }

    }
}