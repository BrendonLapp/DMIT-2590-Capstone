using System;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_AREA
    /// </summary>
    public class AreaDTO
    {
        public int AreaID { get; set; }
        public int DepartmentID { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public string DepartmentName { get; set; }
        public virtual DepartmentDTO Department { get; set; }
    }
}
