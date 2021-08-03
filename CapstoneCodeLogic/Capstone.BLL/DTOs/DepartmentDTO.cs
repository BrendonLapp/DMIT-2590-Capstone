using System;
using System.ComponentModel.DataAnnotations;


namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_DEPARTMENT
    /// </summary>
    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
    }
}
