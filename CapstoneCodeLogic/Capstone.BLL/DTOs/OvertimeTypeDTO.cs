using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{

    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_OVERTIME_TYPE
    /// </summary>
    public class OvertimeTypeDTO
    {
        public int OvertimeTypeID { get; set; }

        public string Name { get; set; }

        public decimal PayMultiplier { get; set; }

        public string Description { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}
