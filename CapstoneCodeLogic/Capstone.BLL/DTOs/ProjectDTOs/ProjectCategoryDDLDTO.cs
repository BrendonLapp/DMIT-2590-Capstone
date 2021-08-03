using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.ProjectDTOs
{
    /// <summary>
    /// DTO for displaying a list of project categories and their names on project category index page
    /// </summary>
    public class ProjectCategoryDDLDTO
    {
        [Key]
        public int ProjectCategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
