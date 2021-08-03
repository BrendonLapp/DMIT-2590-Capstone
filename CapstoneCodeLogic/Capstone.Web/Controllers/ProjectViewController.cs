using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs.ProjectDTOs;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.Web.Models.Project;

namespace Capstone.Web.Controllers
{
    public class ProjectViewController : Controller
    {
        /// <summary>
        /// GET method for the ProjectView Index.cshtml Form
        /// </summary>
        /// <returns>List of ProjectViewModels as projectViewModel</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectController();

            ViewBag.ProjectCategoryID = new SelectList(controller.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName");

            List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();

            List<ProjectDTO> projectDTOs = controller.LookupProject();

            foreach (var item in projectDTOs)
            {
                projectViewModels.Add(new ProjectViewModel
                {
                    ProjectID = item.ProjectID,
                    ProjectCategoryID = item.ProjectCategoryID,
                    ProjectCategoryName = item.ProjectCategoryName,
                    ProjectName = item.ProjectName,
                    Description = item.Description,
                    ActivationDate = item.ActivationDate,
                    DeactivationDate = item.DeactivationDate,
                    StartDate = item.StartDate,
                    ProjectedEndDate = item.ProjectedEndDate
                });
            }

            return View(projectViewModels);
        }

        /// <summary>
        /// Method for populating the filtered project list
        /// </summary>
        /// <returns>Returns the partialview of _IndexPartial and a list of ProjectViewModels as projectViewModels</returns>
        public ActionResult _IndexPartial(int projectCategoryID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }

            List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();

            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();

            var controller = new ProjectController();

            projectDTOs = controller.LookupProjectsByCategory(projectCategoryID);

            foreach (var item in projectDTOs)
            {
                projectViewModels.Add(new ProjectViewModel
                {
                    ProjectID = item.ProjectID,
                    ProjectCategoryID = item.ProjectCategoryID,
                    ProjectName = item.ProjectName,
                    Description = item.Description,
                    ActivationDate = item.ActivationDate,
                    DeactivationDate = item.DeactivationDate,
                    StartDate = item.StartDate,
                    ProjectedEndDate = item.ProjectedEndDate
                });
            }

            return PartialView("_IndexPartial", projectViewModels);
        }

        /// <summary>
        /// GET method for the ProjectView Create.cshtml Form
        /// </summary>
        /// <returns>Empty view model</returns>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }

            var categoryController = new ProjectCategoryController();

            ViewBag.ProjectCategoryID = new SelectList(categoryController.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName");
            return View();
        }

        /// <summary>
        /// Post method for the ProjectView Create.cshtml Form
        /// </summary>
        /// <returns>Single ProjectViewModel of the newly created project</returns>
        /// <param name="projectViewModel">The view model information from the Create.cshtml form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel projectViewModel)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var categoryController = new ProjectCategoryController();

            ViewBag.ProjectCategoryID = new SelectList(categoryController.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName");

            var controller = new ProjectController();
            if (ModelState.IsValid)
            {
                try
                {
                    var projectDTO = new ProjectDTO();

                    projectDTO.ProjectCategoryID = projectViewModel.ProjectCategoryID;
                    projectDTO.ProjectName = projectViewModel.ProjectName;
                    projectDTO.Description = projectViewModel.Description;
                    projectDTO.StartDate = projectViewModel.StartDate;
                    projectDTO.ProjectedEndDate = projectViewModel.ProjectedEndDate;
                    projectDTO.ActivationDate = DateTime.Now;
                    projectDTO.DeactivationDate = projectViewModel.DeactivationDate;

                    string userID = User.Identity.Name;
                    controller.CreateNewProject(projectDTO, IdentityHelper.GetEmployeeID());
                    return RedirectToAction("Index");
                }
                catch (AggregateException ex)
                {
                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
            }
            return View(projectViewModel);

        }

        /// <summary>
        /// GET method for the ProjectView Edit.cshtml Form
        /// </summary>
        /// <returns>A single ProjectViewModel as viewModel</returns>
        /// <param name="ID">The Project ID to load the projects information</param>
        public ActionResult Edit(int ID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectController();
            var viewModel = new ProjectViewModel();
            var projectDTO = new ProjectDTO();

            var categoryController = new ProjectCategoryController();

            ViewBag.ProjectCategoryID = new SelectList(categoryController.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName");

            projectDTO = controller.LookupProject(ID);

            viewModel.ProjectID = projectDTO.ProjectID;
            viewModel.ProjectCategoryID = projectDTO.ProjectCategoryID;
            viewModel.ProjectName = projectDTO.ProjectName;
            viewModel.Description = projectDTO.Description;
            viewModel.StartDate = projectDTO.StartDate;
            viewModel.ProjectedEndDate = projectDTO.ProjectedEndDate;
            viewModel.ActivationDate = projectDTO.ActivationDate;
            viewModel.DeactivationDate = projectDTO.DeactivationDate;

            return View(viewModel);
        }

        /// <summary>
        /// Post method for the ProjectView Edit.cshtml Form
        /// </summary>
        /// <returns>Single ProjectViewModel of the newly created project</returns>
        /// <param name="projectViewModel">The view model information from the Create.cshtml form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel projectViewModel)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var categoryController = new ProjectCategoryController();
            ViewBag.ProjectCategoryID = new SelectList(categoryController.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName", projectViewModel.ProjectCategoryID);

            var controller = new ProjectController();
            if (ModelState.IsValid)
            {
                try
                {
                    var projectDTO = new ProjectDTO();

                    projectDTO.ProjectID = projectViewModel.ProjectID;
                    projectDTO.ProjectCategoryID = projectViewModel.ProjectCategoryID;
                    projectDTO.ProjectName = projectViewModel.ProjectName;
                    projectDTO.Description = projectViewModel.Description;
                    projectDTO.StartDate = projectViewModel.StartDate;
                    projectDTO.ProjectedEndDate = projectViewModel.ProjectedEndDate;
                    projectDTO.ActivationDate = projectViewModel.ActivationDate;
                    projectDTO.DeactivationDate = projectViewModel.DeactivationDate;

                    controller.UpdateProject(projectDTO, IdentityHelper.GetEmployeeID());
                    return RedirectToAction("Index", "ProjectView");
                }
                catch (AggregateException ex)
                {
                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
                return View(projectViewModel);
            }
            //return PartialView("_EditPartial", projectViewModel);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET method for the ProjectView Deactivate.cshtml Form
        /// </summary>
        /// <returns>A single ProjectViewModel as viewModel</returns>
        /// <param name="ID">The Project ID to load the projects information</param>
        public ActionResult Deactivate(int? ID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var controller = new ProjectController();
            var viewModel = new ProjectViewModel();
            var projectDTO = new ProjectDTO();

            var categoryController = new ProjectCategoryController();

            ViewBag.ProjectCategoryID = new SelectList(categoryController.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName");

            projectDTO = controller.LookupProject(ID.Value);

            viewModel.ProjectID = projectDTO.ProjectID;
            viewModel.ProjectCategoryID = projectDTO.ProjectCategoryID;
            viewModel.ProjectName = projectDTO.ProjectName;
            viewModel.Description = projectDTO.Description;
            viewModel.StartDate = projectDTO.StartDate;
            viewModel.ProjectedEndDate = projectDTO.ProjectedEndDate;
            viewModel.ActivationDate = projectDTO.ActivationDate;
            viewModel.DeactivationDate = projectDTO.DeactivationDate;

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        /// <summary>
        /// Post method for the ProjectView Deactivate.cshtml Form
        /// </summary>
        /// <returns>Single ProjectViewModel of the deactivating selected project</returns>
        /// <param name="ID">The Project ID of the selected project to deactivate</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int ID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectController();
            var deactivatedProject = controller.LookupProject(ID);
            TempData["message"] = "The project " + " " + "'" + deactivatedProject.ProjectName + "'" + " " + "was successfully deactivated.";

            controller.DeactivateProject(deactivatedProject, IdentityHelper.GetEmployeeID());
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET method for the ProjectView Details.cshtml Form
        /// </summary>
        /// <returns>A single ProjectViewModel as viewModel</returns>
        /// <param name="ID">The Project ID to load the projects information</param>
        public ActionResult Details(int ID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectController();
            var viewModel = new ProjectViewModel();
            var projectDTO = new ProjectDTO();

            var categoryController = new ProjectCategoryController();

            ViewBag.ProjectCategoryID = new SelectList(categoryController.LookUpProjectCategories(), "ProjectCategoryID", "CategoryName");

            projectDTO = controller.LookupProject(ID);

            viewModel.ProjectID = projectDTO.ProjectID;
            viewModel.ProjectCategoryID = projectDTO.ProjectCategoryID;
            viewModel.ProjectName = projectDTO.ProjectName;
            viewModel.Description = projectDTO.Description;
            viewModel.StartDate = projectDTO.StartDate;
            viewModel.ProjectedEndDate = projectDTO.ProjectedEndDate;
            viewModel.ActivationDate = projectDTO.ActivationDate;
            viewModel.DeactivationDate = projectDTO.DeactivationDate;

            return View(viewModel);
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.ExceptionHandled)
            {
                return;
            }

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandling/Index.cshtml"
            };

            filterContext.ExceptionHandled = true;
        }
    }
}