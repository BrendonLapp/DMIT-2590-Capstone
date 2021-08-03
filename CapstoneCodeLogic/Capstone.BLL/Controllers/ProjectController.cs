using System;
using System.Collections.Generic;
using System.Linq;
using Capstone.BLL.DTOs.ProjectDTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

//created 3/19/2019
namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// ScheduleType CRUD Controller
    /// </summary>
    public class ProjectController
    {
        /// <summary>
        /// Gets a list of Projects
        /// </summary>
        /// <returns>A list of Projects<returns>
        public List<ProjectDTO> LookupProject()
        {
            using (var context = new CapstoneContext())
            {
                var projectList = (from project in context.TB_Capstone_PROJECT
                                   where project.DeactivationDate == null || project.DeactivationDate > DateTime.Now
                                   select new ProjectDTO
                                   {
                                       ProjectID = project.ProjectID,
                                       ProjectCategoryID = project.ProjectCategoryID,
                                       ProjectCategoryName = (from category in context.TB_Capstone_PROJECT_CATEGORY
                                                              where category.ProjectCategoryID == project.ProjectCategoryID
                                                              select category.CategoryName).FirstOrDefault(),
                                       ProjectName = project.ProjectName,
                                       Description = project.Description,
                                       StartDate = project.StartDate,
                                       ProjectedEndDate = project.ProjectedEndDate,
                                       ActivationDate = project.ActivationDate,
                                       DeactivationDate = project.DeactivationDate
                                   }).ToList();
                return projectList;
            }
        }

        /// <summary>
        /// Gets a single projects information by its ID
        /// </summary>
        /// <returns>All of a single projects information by returning ProjectDto as projectDTO <returns>
        /// <param name="ProjectID">The key value of TB_Capstone_PROJECT to load the Project Infromation from the ID </param>
        public ProjectDTO LookupProject(int ProjectID)
        {
            using (var context = new CapstoneContext())
            {
                var projectDTO = (from project in context.TB_Capstone_PROJECT
                                  where project.ProjectID == ProjectID
                                  select new ProjectDTO
                                  {
                                      ProjectID = project.ProjectID,
                                      ProjectCategoryID = project.ProjectCategoryID,
                                      ProjectName = project.ProjectName,
                                      Description = project.Description,
                                      StartDate = project.StartDate,
                                      ProjectedEndDate = project.ProjectedEndDate,
                                      ActivationDate = project.ActivationDate,
                                      DeactivationDate = project.DeactivationDate
                                  }).FirstOrDefault();
                return projectDTO;
            }
        }

        /// <summary>
        /// Gets a list of ProjectCategories
        /// </summary>
        /// <returns>Returns ProjectCategoryDDLDTO as categoryList<returns>
        public List<ProjectCategoryDDLDTO> LookUpProjectCategories()
        {
            using (var context = new CapstoneContext())
            {
                var categoryList = (from category in context.TB_Capstone_PROJECT_CATEGORY
                                    select new ProjectCategoryDDLDTO
                                    {
                                        ProjectCategoryID = category.ProjectCategoryID,
                                        CategoryName = category.CategoryName
                                    }).ToList();

                return categoryList;
            }
        }

        /// <summary>
        /// Gets a filtered list of all all categories based on a projectCategoryID
        /// </summary>
        /// <returns>Returns a list of ProjectDTOs as projectList<returns>
        /// <param name="projectCategoryID">The key value of TB_Capstone_PROJECT to load the Projects with belonging to the project category</param>
        public List<ProjectDTO> LookupProjectsByCategory(int projectCategoryID)
        {
            if (projectCategoryID == 0)
            {
                return LookupProject();
            }
            else
            {
                using (var context = new CapstoneContext())
                {
                    var projectList = (from project in context.TB_Capstone_PROJECT
                                       where project.ProjectCategoryID == projectCategoryID
                                       select new ProjectDTO
                                       {
                                           ProjectID = project.ProjectID,
                                           ProjectCategoryID = project.ProjectCategoryID,
                                           ProjectName = project.ProjectName,
                                           Description = project.Description,
                                           ActivationDate = project.ActivationDate,
                                           DeactivationDate = project.DeactivationDate,
                                           StartDate = project.StartDate,
                                           ProjectedEndDate = project.ProjectedEndDate
                                       }).ToList();

                    return projectList;
                }
            }
        }

        /// <summary>
        /// Method to create a new Project in TB_Capstone_PROJECT
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="projectDTO">The information passed from the Create.cshtml form</param>
        /// <param name="employeeID">The currently logged in users employeeID</param>
        public void CreateNewProject(ProjectDTO projectDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var errorList = new List<Exception>();
                if (projectDTO.DeactivationDate < DateTime.Now)
                {
                    errorList.Add(new Exception("The deactivation date must come after today."));
                }
                if (projectDTO.StartDate.Date < DateTime.Now.Date)
                {
                    errorList.Add(new Exception("The start date must be greater or equal to today."));
                }
                if (projectDTO.ProjectedEndDate < DateTime.Now || projectDTO.ProjectedEndDate < projectDTO.StartDate)
                {
                    errorList.Add(new Exception("The start date must come before the projected end date."));
                }

                if (errorList.Count > 0)
                {
                    throw new AggregateException("", errorList);
                }

                var newProject = new TB_Capstone_PROJECT();

                newProject.ProjectCategoryID = projectDTO.ProjectCategoryID;
                newProject.ProjectName = projectDTO.ProjectName;
                newProject.Description = projectDTO.Description;
                newProject.StartDate = projectDTO.StartDate;
                newProject.ProjectedEndDate = projectDTO.ProjectedEndDate;
                newProject.ActivationDate = projectDTO.ActivationDate;
                newProject.DeactivationDate = projectDTO.DeactivationDate;
                newProject.CreatedBy = employeeID;
                newProject.CreationDate = DateTime.Now;
                newProject.UpdatedBy = null;
                newProject.UpdatedDate = null;

                context.TB_Capstone_PROJECT.Add(newProject);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method to Edit an existing Project in TB_Capstone_PROJECT. Updates the existing record to be have the changed values and sets the updated and updatedDate properties.
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="projectDTO">The information passed from the Create.cshtml form</param>
        /// <param name="employeeID">The currently logged in users employeeID</param>
        public void UpdateProject(ProjectDTO projectDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var errorList = new List<Exception>();
                if (projectDTO.DeactivationDate < DateTime.Now)
                {
                    errorList.Add(new Exception("The deactivation date must come after today."));
                }
                if (projectDTO.ProjectedEndDate < projectDTO.StartDate)
                {
                    errorList.Add(new Exception("The start date must come before the projected end date."));
                }

                if (errorList.Count > 0)
                {
                    throw new AggregateException("", errorList);
                }

                var updateProject = context.TB_Capstone_PROJECT.Find(projectDTO.ProjectID);

                updateProject.ProjectID = projectDTO.ProjectID;
                updateProject.ProjectCategoryID = projectDTO.ProjectCategoryID;
                updateProject.ProjectName = projectDTO.ProjectName;
                updateProject.Description = projectDTO.Description;
                updateProject.StartDate = projectDTO.StartDate;
                updateProject.ProjectedEndDate = projectDTO.ProjectedEndDate;
                updateProject.DeactivationDate = projectDTO.DeactivationDate;
                updateProject.UpdatedBy = employeeID;
                updateProject.UpdatedDate = DateTime.Now;

                context.Entry(updateProject).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method to deactivate an existing Project in TB_Capstone_PROJECT. Updates the Deactivation date to today.
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="projectDTO">The information passed from the Create.cshtml form</param>
        /// <param name="employeeID">The currently logged in users employeeID</param>
        public void DeactivateProject(ProjectDTO projectDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deactivateProject = context.TB_Capstone_PROJECT.Find(projectDTO.ProjectID);

                deactivateProject.DeactivationDate = DateTime.Now;
                deactivateProject.UpdatedBy = employeeID;
                deactivateProject.UpdatedDate = DateTime.Now;

                context.Entry(deactivateProject).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
