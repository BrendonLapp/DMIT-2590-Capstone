using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_POSITION
    /// </summary>
    public class PositionEditDTO
    {
        public int PositionID { get; set; }
        public string PositionTitle { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
