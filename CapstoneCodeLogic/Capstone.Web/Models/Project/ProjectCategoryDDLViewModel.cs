using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.Project
{
    /// <summary>
    /// ViewModel for displaying a list of project categories and their names on project category index page
    /// </summary>
    public class ProjectCategoryDDLViewModel
    {
        [Key]
        public int ProjectCategoryID { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}