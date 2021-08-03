using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_OFFDAY_TYPE
    /// </summary>
    public class OffDayTypeDTO
    {
        public int OffDayID { get; set; }

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Marks whether or not this offday type is considered a form of paid time off
        /// </summary>
        public bool PTO { get; set; }

        public string Notes { get; set; }

        public string ColorDisplayed { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}
