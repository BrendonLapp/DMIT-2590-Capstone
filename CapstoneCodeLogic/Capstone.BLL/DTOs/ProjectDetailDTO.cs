using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_PROJECT_DETAIL
    /// </summary>
    public class ProjectDetailDTO
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
