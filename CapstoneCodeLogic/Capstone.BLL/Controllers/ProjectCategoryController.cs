using System.Collections.Generic;
using System.Linq;
using Capstone.BLL.DTOs;
using Capstone.DAL.Entities;
using Capstone.DAL.Context;
using System;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// ProjectCategory CRUD Controller
    /// </summary>
    public class ProjectCategoryController
    {
        /// <summary>
        /// Gets a list of Project Categories
        /// </summary>
        /// <returns>A list of Project Categories/returns>
        public List<ProjectCategoryDTO> LookUpProjectCategories()
        {
            using (var context = new CapstoneContext())
            {
                List<ProjectCategoryDTO> projectCategories = new List<ProjectCategoryDTO>();
                projectCategories = (from item in context.TB_Capstone_PROJECT_CATEGORY
                                     where item.DeactivationDate == null || item.DeactivationDate > DateTime.Now
                                     select new ProjectCategoryDTO
                                     {
                                         ProjectCategoryID = item.ProjectCategoryID,
                                         Description = item.Description,
                                         CategoryName = item.CategoryName,
                                         ActivationDate = item.ActivationDate,
                                         DeactivationDate = item.DeactivationDate,
                                         Global = item.Global,
                                         Color = item.Color
                                     }).ToList();

                return projectCategories;
            }
        }

        /// <summary>
        /// Gets a single Project Category
        /// </summary>
        /// <param name="projectCategoryID">TB_Capstone_PROJECT_CATEGORY Primary Key value</param>
        /// <returns>Project Category that matches the entered primary key</returns>
        public ProjectCategoryDTO LookupProjectCategory(int projectCategoryID)
        {
            using (var context = new CapstoneContext())
            {
                ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();
                projectCategoryDTO = (from item in context.TB_Capstone_PROJECT_CATEGORY
                                      where item.ProjectCategoryID == projectCategoryID
                                      select new ProjectCategoryDTO
                                      {
                                          ProjectCategoryID = item.ProjectCategoryID,
                                          CategoryName = item.CategoryName,
                                          Description = item.Description,
                                          ActivationDate = item.ActivationDate,
                                          DeactivationDate = item.DeactivationDate,
                                          Global = item.Global,
                                          Color = item.Color
                                      }).FirstOrDefault();

                return projectCategoryDTO;
            }
        }

        /// <summary>
        /// Creates a new entry in the TB_Capstone_PROJECT_CATEGORY table
        /// </summary>
        /// <param name="newProjectCategoryDTO">Information for a newly created Project Category</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateProjectCategory(ProjectCategoryDTO newProjectCategoryDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (newProjectCategoryDTO.DeactivationDate < newProjectCategoryDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                //var newCategory = context.TB_Capstone_PROJECT_CATEGORY.Add(new TB_Capstone_PROJECT_CATEGORY());
                var newCategory = new TB_Capstone_PROJECT_CATEGORY();

                //newCategory.ProjectCategoryID = projectCategoryDTO.ProjectCategoryID;
                newCategory.CategoryName = newProjectCategoryDTO.CategoryName;
                newCategory.Description = newProjectCategoryDTO.Description;
                newCategory.Global = newProjectCategoryDTO.Global;
                newCategory.Color = newProjectCategoryDTO.Color;
                newCategory.ActivationDate = newProjectCategoryDTO.ActivationDate;
                newCategory.DeactivationDate = newProjectCategoryDTO.DeactivationDate;
                newCategory.CreatedBy = employeeID;
                newCategory.CreationDate = System.DateTime.Now;
                newCategory.UpdatedBy = null;
                newCategory.UpdatedDate = null;

                context.TB_Capstone_PROJECT_CATEGORY.Add(newCategory);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Creates a new entry in the TB_Capstone_PROJECT_CATEGORY table
        /// </summary>
        /// <param name="projectCategoryDTO">Information for the Project Category being edited</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        /// <returns>ProjectCategoryDTO as projectCategoryDTO</returns>
        public ProjectCategoryDTO UpdateProjectCategory(ProjectCategoryDTO projectCategoryDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (projectCategoryDTO.DeactivationDate < projectCategoryDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var updateProjectCategory = context.TB_Capstone_PROJECT_CATEGORY.Find(projectCategoryDTO.ProjectCategoryID);

                //updateProjectCategory.ProjectCategoryID = projectCategoryDTO.ProjectCategoryID;
                updateProjectCategory.CategoryName = projectCategoryDTO.CategoryName;
                updateProjectCategory.Description = projectCategoryDTO.Description;
                updateProjectCategory.Global = projectCategoryDTO.Global;
                updateProjectCategory.Color = projectCategoryDTO.Color;
                updateProjectCategory.ActivationDate = projectCategoryDTO.ActivationDate;
                updateProjectCategory.DeactivationDate = projectCategoryDTO.DeactivationDate;
                updateProjectCategory.Color = projectCategoryDTO.Color;
                updateProjectCategory.UpdatedBy = employeeID;
                updateProjectCategory.UpdatedDate = System.DateTime.Now;

                context.Entry(updateProjectCategory).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();


                return projectCategoryDTO;
            }
        }

        /// <summary>
        /// Deactivates a project cateogry to todays date
        /// </summary>
        /// <param name="projectCategoryDTO">Information on the project category being deactivated</param>
        /// <param name="employeeID">The current users ID</param>
        public void DeactivateProjectCategory(ProjectCategoryDTO projectCategoryDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deactivateProjectCategory = context.TB_Capstone_PROJECT_CATEGORY.Find(projectCategoryDTO.ProjectCategoryID);

                deactivateProjectCategory.DeactivationDate = DateTime.Now;
                deactivateProjectCategory.UpdatedBy = employeeID;
                deactivateProjectCategory.UpdatedDate = DateTime.Now;

                context.Entry(deactivateProjectCategory).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
