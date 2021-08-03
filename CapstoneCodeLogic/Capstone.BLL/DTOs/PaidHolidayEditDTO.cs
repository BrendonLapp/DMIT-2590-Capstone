using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_PAID_HOLIDAY 
    /// </summary>
    public class PaidHolidayEditDTO
    {
        public int PaidHolidayID { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Notes { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
