using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_UNIT
    /// </summary>
    public class UnitDTO
    {
        public int UnitID { get; set; }
        public int AreaID { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public string AreaName { get; set; }
        public virtual AreaDTO Area { get; set; }
    }
}
