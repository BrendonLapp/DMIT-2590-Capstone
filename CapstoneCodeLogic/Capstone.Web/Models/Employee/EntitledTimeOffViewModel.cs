using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.Employee
{
    /// <summary>
    /// View Model for entitled time off creation, stores a single entitled time off
    /// </summary>
    public class EntitledTimeOffViewModel
    {
        public int OffDayTypeID { get; set; }

        public string OffDayTypeName { get; set; }

        [Display(Name = "Entitled Days Off")]
        public decimal HoursAccumulated { get; set; }
    }
}