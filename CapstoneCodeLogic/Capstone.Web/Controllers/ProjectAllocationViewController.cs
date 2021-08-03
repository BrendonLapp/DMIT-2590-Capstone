using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs.ProjectAllocation;
using Capstone.BLL.DTOs.ProjectDTOs;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.Web.Models.Project;
using Capstone.Web.Models.ProjectAllocation;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// Project Allocation View Controller for views in the Views/ProjectAllocationView folder
    /// </summary>
    public class ProjectAllocationViewController : Controller
    {

        /// <summary>
        /// GET method for the ProjectAllocation Index.cshtml Form
        /// </summary>
        /// <returns>ProjectViewModel as projectList</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectController();

            var projectList = new List<ProjectViewModel>();
            var projectDTOs = new List<ProjectDTO>();

            projectDTOs = controller.LookupProject();

            foreach (var item in projectDTOs)
            {
                projectList.Add(new ProjectViewModel
                {
                    ProjectID = item.ProjectID,
                    ProjectCategoryID = item.ProjectCategoryID,
                    ProjectName = item.ProjectName,
                    ProjectCategoryName = item.ProjectCategoryName,
                    Description = item.Description,
                    StartDate = item.StartDate,
                    ProjectedEndDate = item.ProjectedEndDate,
                    ActivationDate = item.ActivationDate,
                    DeactivationDate = item.DeactivationDate
                });
            }

            return View(projectList);
        }

        /// <summary>
        /// GET method for the ProjectAllocation ProjectIndex.cshtml Form. Uses a projectID to get relevant information into the partial views.
        /// </summary>
        /// <returns>Returns an empty view</returns>
        /// <param name="projectID">The Key of the selected project to get information from the TB_Capstone_PROJECT table</param>
        public ActionResult ProjectIndex(int projectID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            ViewBag.projectID = projectID;
            var projectController = new ProjectController();
            var project = projectController.LookupProject(projectID);

            ViewBag.ProjectName = project.ProjectName;

            var teamController = new TeamController();
            ViewBag.TeamID = new SelectList(teamController.LookupTeam(), "TeamID", "TeamName");
            return View();
        }

        /// <summary>
        /// GET method for the _TeamPartial _TeamPartail.cshtml.
        /// </summary>
        /// <returns>Returns a partial view as _TeamPartial and a list of EmployeeAllocationViewModels as allocationViewModels</returns>
        /// <param name="projectID">The Key of the selected project to get information from the TB_Capstone_PROJECT table</param>
        /// <param name="teamID">The key of the selected team to get information of the members on the selected team</param>
        /// <param name="year">The current year of the model in the _AllocationPartail</param>
        public ActionResult _TeamPartial(int? teamID, int? projectID, int? year)
        {
            if (teamID == null)
            {
                teamID = IdentityHelper.GetTeamID();
            }

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            var controller = new ProjectAllocationController();

            var allocationViewModels = new List<EmployeeAllocationViewModel>();
            var allocationDTOs = new List<EmployeeAllocationDTO>();

            allocationDTOs = controller.LookupEmployeesByTeam(teamID.Value, projectID.Value, year.Value);

            foreach (var item in allocationDTOs)
            {
                allocationViewModels.Add(
                    new EmployeeAllocationViewModel
                    {
                        EmployeeID = item.EmployeeID,
                        PositionID = item.PositionID,
                        PositionName = item.PositionName,
                        RoleID = item.RoleID,
                        RoleName = item.RoleName,
                        TeamID = item.TeamID,
                        FirstName = item.FirstName,
                        LastName = item.LastName
                    });
            }

            return PartialView("_TeamPartial", allocationViewModels);
        }

        /// <summary>
        /// GET method for the _AllocationPartial _AllocationPartial.cshtml.
        /// </summary>
        /// <returns>Returns a partial view as _AllocationPartial and a list of AllocatedEmployeesViewModel as allocatedDays</returns>
        /// <returns>Possible to return and empty AllocatedEmplyeesViewModel with the current year to load the page on page load.</returns>
        /// <param name="projectID">The Key of the selected project to get information from the TB_Capstone_PROJECT table</param>
        /// <param name="year">The current year of the model in the _AllocationPartail</param>
        public ActionResult _AllocationPartial(int? projectID, int? year)
        {
            var controller = new ProjectAllocationController();

            var allocatedDays = new List<AllocatedEmployeesViewModel>();
            var allocatedDaysDTOs = new List<AllocatedEmployeesDTO>();

            if (year == 0 || year == null)
            {
                year = DateTime.Now.Year;
            }

            allocatedDaysDTOs = controller.LookupEmployeesForProject(projectID.Value, year.Value);

            foreach (var item in allocatedDaysDTOs)
            {
                allocatedDays.Add(new AllocatedEmployeesViewModel
                {
                    EmployeeID = item.EmployeeID,
                    EmployeeName = item.EmployeeName,
                    Year = item.Year,
                    AllocationID = item.AllocationID,
                    allocatedDays = item.allocatedDays,
                    projectAllocation = item.projectDays
                });
            }

            if (allocatedDays.Count != 0)
            {
                return PartialView("_AllocationPartial", allocatedDays);
            }
            else
            {
                //This must happen to load the view if there is nothing to show
                var psuedoList = new List<AllocatedEmployeesViewModel>();
                psuedoList.Add(new AllocatedEmployeesViewModel
                {
                    allocatedDays = new List<AllocatedDaysDTO>(),
                    AllocationID = 0,
                    EmployeeID = 0,
                    EmployeeName = "",
                    projectAllocation = new List<ProjectAllocatedDaysDTO>(),
                    Year = year.Value
                });

                return PartialView("_AllocationPartial", psuedoList);
            }
        }

        /// <summary>
        /// POST Method to create a new employee allocation on a project. Refreshed the _Allocation partial when the allocation is successfully created.
        /// </summary>
        /// <returns>Returns a partial view as _AllocationPartial and a list of AllocatedEmployeesViewModel as allocatedDays</returns>
        /// <param name="projectID">The Key of the selected project to get information from the TB_Capstone_PROJECT table</param>
        /// <param name="employeeID">The key of the selected employee to assign the new allocation to</param>
        /// <param name="year">The current year of the model in the _AllocationPartail</param>
        [HttpPost]
        public ActionResult CreateNewEmployeeOnProject(int employeeID, int year, int projectID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectAllocationController();
            var allocatedDays = new List<AllocatedEmployeesViewModel>();
            var allocatedDaysDTOs = new List<AllocatedEmployeesDTO>();

            try
            {
                controller.CreateNewAllocation(employeeID, year, projectID, IdentityHelper.GetEmployeeID());

                allocatedDaysDTOs = controller.LookupEmployeesForProject(projectID, year);

                foreach (var item in allocatedDaysDTOs)
                {
                    allocatedDays.Add(new AllocatedEmployeesViewModel
                    {
                        EmployeeID = item.EmployeeID,
                        EmployeeName = item.EmployeeName,
                        Year = item.Year,
                        AllocationID = item.AllocationID,
                        allocatedDays = item.allocatedDays,
                        projectAllocation = item.projectDays
                    });
                }

                return PartialView("_AllocationPartial", allocatedDays);
            }
            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

                return PartialView("_AllocationPartial", allocatedDays);
            }
        }

        /// <summary>
        /// POST Method to deactivate an allocation on a project. Refreshed the _Allocation partial when the allocation is successfully deactivated.
        /// </summary>
        /// <returns>Returns a partial view as _AllocationPartial and a list of AllocatedEmployeesViewModel as allocatedDays</returns>
        /// <param name="allocationID">The Key of the selected allocation to get information from the TB_Capstone_ALLOCATION table</param>
        /// <param name="employeeID">The key of the selected employee to assign the new allocation to</param>
        /// <param name="year">The current year of the model in the _AllocationPartail</param>
        [HttpPost]
        public ActionResult DeactivateEmployeeOnProject(int allocationID, int year, int projectID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectAllocationController();
            var allocatedDays = new List<AllocatedEmployeesViewModel>();
            var allocatedDaysDTOs = new List<AllocatedEmployeesDTO>();

            try
            {
                controller.DeactivateAllocation(allocationID, IdentityHelper.GetEmployeeID(), projectID, year);

                return _AllocationPartial(projectID, year);
            }
            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

                return PartialView("_AllocationPartial", allocatedDays);
            }
        }

        /// <summary>
        /// POST Method to save allocation on save button click, next year click or previous year click. 
        /// </summary>
        /// <returns>Returns the user back to a new instance of the ProjectIndex page</returns>
        /// <param name="projectID">The Key of the selected project to get information from the TB_Capstone_PROJECT table</param>
        /// <param name="json">A json list that is converted into ProjectAllocatedDaysViewModel</param>
        [HttpPost]
        public ActionResult PostAllocation(List<ProjectAllocatedDaysViewModel> json, int projectID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectAllocationController();

            var allocatedDaysDTO = new List<ProjectAllocatedDaysDTO>();

            int year = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in json)
                    {
                        allocatedDaysDTO.Add(new ProjectAllocatedDaysDTO
                        {
                            EmployeeID = item.EmployeeID,
                            Year = item.Year,
                            AllocationID = item.AllocationID,
                            ProjectJanuary = item.ProjectJanuary,
                            ProjectFebruary = item.ProjectFebruary,
                            ProjectMarch = item.ProjectMarch,
                            ProjectApril = item.ProjectApril,
                            ProjectMay = item.ProjectMay,
                            ProjectJune = item.ProjectJune,
                            ProjectJuly = item.ProjectJuly,
                            ProjectAugust = item.ProjectAugust,
                            ProjectSeptember = item.ProjectSeptember,
                            ProjectOctober = item.ProjectOctober,
                            ProjectNovember = item.ProjectNovember,
                            ProjectDecember = item.ProjectDecember
                        });

                        year = item.Year;
                        controller.UpdateAllocation(allocatedDaysDTO, projectID, IdentityHelper.GetEmployeeID());
                        TempData["message"] = "The allocation has been saved.";
                        return ProjectIndex(projectID);
                    }
                }
            }
            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

                return _AllocationPartial(projectID, year);
            }
            return PartialView("_AllocationPartial", controller.LookupEmployeesForProject(projectID, year));
        }

    }
}