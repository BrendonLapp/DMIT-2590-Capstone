using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Capstone.BLL.DTOs;

namespace Capstone.Web.Models
{
    /// <summary>
    ///Data Transfer Object for information designed for the Index Page of Team Calendar
    /// </summary>
    public class TeamCalendarViewModel
    {
        [Display(Name = "Team Name")]
        public int TeamID { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}