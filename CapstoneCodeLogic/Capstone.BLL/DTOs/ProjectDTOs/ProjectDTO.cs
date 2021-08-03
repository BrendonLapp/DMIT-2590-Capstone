using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.ProjectDTOs
{
    /// <summary>
    /// DTO for transfering data from TB_Capstone_PROJECT between the ProjectViewController and ProjectController
    /// </summary>
    public class ProjectDTO
    {
        [Key]
        public int ProjectID { get; set; }
        public int ProjectCategoryID { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
    }
}
