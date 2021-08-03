using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_PROJECT_CATEGORY
    /// </summary>
    public class ProjectCategoryDTO
    {
        [Key]
        public int ProjectCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public string Color { get; set; }
        public bool Global { get; set; }
    }
}
