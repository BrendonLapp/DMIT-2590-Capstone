using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ProjectDetailViewModel
    {
        public int ProjectDetailID { get; set; }

        public int ProjectID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Notes { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}