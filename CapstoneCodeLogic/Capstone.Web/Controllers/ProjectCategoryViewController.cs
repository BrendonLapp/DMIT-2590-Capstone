using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// Project Category View Controller for views in the Views/ProjectCategoryView folder
    /// </summary>
    public class ProjectCategoryViewController : Controller
    {
        /// <summary>
        /// GET method for the ProjectCategory Index.cshtml Form
        /// </summary>
        /// <returns>ProjectCategoryViewModel as viewModel</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectCategoryController();

            List<ProjectCategoryDTO> projectCategoryDTOs = controller.LookUpProjectCategories();

            List<ProjectCategoryViewModel> viewModel = new List<ProjectCategoryViewModel>();

            foreach (var categories in projectCategoryDTOs)
            {
                viewModel.Add(new ProjectCategoryViewModel
                {
                    ProjectCategoryID = categories.ProjectCategoryID,
                    CategoryName = categories.CategoryName,
                    Description = categories.Description,
                    ActivationDate = categories.ActivationDate,
                    DeactivationDate = categories.DeactivationDate,
                    Global = categories.Global,
                    Color = categories.Color
                });
            }

            return View(viewModel);
        }

        /// <summary>
        /// GET method for the ProjectCategory Details.cshtml Form
        /// </summary>
        /// <param name="ProjectCategoryID">The KEY of TB_Capstone_PROJECT_CATEGORY table</param>
        public ActionResult Details(int? ProjectCategoryID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (ProjectCategoryID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var controller = new ProjectCategoryController();

            ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();

            ProjectCategoryViewModel projectCategoryViewModel = new ProjectCategoryViewModel();

            projectCategoryDTO = controller.LookupProjectCategory(ProjectCategoryID.Value);

            projectCategoryViewModel.ProjectCategoryID = projectCategoryDTO.ProjectCategoryID;
            projectCategoryViewModel.CategoryName = projectCategoryDTO.CategoryName;
            projectCategoryViewModel.Description = projectCategoryDTO.Description;
            projectCategoryViewModel.Global = projectCategoryDTO.Global;
            projectCategoryViewModel.Color = projectCategoryDTO.Color;
            projectCategoryViewModel.ActivationDate = projectCategoryDTO.ActivationDate;
            projectCategoryViewModel.DeactivationDate = projectCategoryDTO.DeactivationDate;

            if (projectCategoryViewModel == null)
            {
                return HttpNotFound();
            }

            return View(projectCategoryViewModel);
        }

        /// <summary>
        /// GET method for the ProjectCategory Create.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            return View();
        }

        /// <summary>
        /// POST method for the ProjectCategory Create.cshtml Form
        /// </summary>
        /// <param name="projectCategory">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectCategoryViewModel projectCategory)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var controller = new ProjectCategoryController();

                    ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();

                    projectCategoryDTO.ProjectCategoryID = projectCategory.ProjectCategoryID;
                    projectCategoryDTO.CategoryName = projectCategory.CategoryName;
                    projectCategoryDTO.Description = projectCategory.Description;
                    projectCategoryDTO.Global = projectCategory.Global;
                    projectCategoryDTO.Color = projectCategory.Color;
                    projectCategoryDTO.ActivationDate = projectCategory.ActivationDate;
                    projectCategoryDTO.DeactivationDate = projectCategory.DeactivationDate;

                    controller.CreateProjectCategory(projectCategoryDTO, IdentityHelper.GetEmployeeID());

                    TempData["message"] = "The project category" + " " + "'" + projectCategoryDTO.CategoryName + "'" + " " + "was successfully created.";

                    return RedirectToAction("Index");
                }

                return View(projectCategory);
            }


            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(projectCategory);
            }
        }

        /// <summary>
        /// GET method for the ProjectCategory Edit.cshtml Form
        /// </summary>
        /// <param name="projectCategoryID">The KEY of TB_Capstone_PROJECT_CATEGORY table</param>
        public ActionResult Edit(int projectCategoryID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectCategoryController();

            ProjectCategoryViewModel projectCategoryViewModel = new ProjectCategoryViewModel();

            ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();

            projectCategoryDTO = controller.LookupProjectCategory(projectCategoryID);

            projectCategoryViewModel.ProjectCategoryID = projectCategoryDTO.ProjectCategoryID;
            projectCategoryViewModel.CategoryName = projectCategoryDTO.CategoryName;
            projectCategoryViewModel.Description = projectCategoryDTO.Description;
            projectCategoryViewModel.Global = projectCategoryDTO.Global;
            projectCategoryViewModel.Color = projectCategoryDTO.Color;
            projectCategoryViewModel.ActivationDate = projectCategoryDTO.ActivationDate;
            projectCategoryViewModel.DeactivationDate = projectCategoryDTO.DeactivationDate;

            return View(projectCategoryViewModel);
        }

        /// <summary>
        /// POST method for the ProjectCategory Create.cshtml Form
        /// </summary>
        /// <param name="projectCategory">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectCategoryViewModel projectCategory)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var controller = new ProjectCategoryController();

                    ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();

                    projectCategoryDTO.ProjectCategoryID = projectCategory.ProjectCategoryID;
                    projectCategoryDTO.CategoryName = projectCategory.CategoryName;
                    projectCategoryDTO.Description = projectCategory.Description;
                    projectCategoryDTO.Global = projectCategory.Global;
                    projectCategoryDTO.Color = projectCategory.Color;
                    projectCategoryDTO.ActivationDate = projectCategory.ActivationDate;
                    projectCategoryDTO.DeactivationDate = projectCategory.DeactivationDate;

                    controller.UpdateProjectCategory(projectCategoryDTO, IdentityHelper.GetEmployeeID());

                    TempData["message"] = "The project category" + " " + "'" + projectCategoryDTO.CategoryName + "'" + " " + "was successfully updated.";

                    return RedirectToAction("Index");
                }

                return View(projectCategory);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(projectCategory);
            }
        }

        /// <summary>
        /// GET method for the ProjectCategory Deactivate.cshtml Form
        /// </summary>
        /// <param name="projectCategoryID">The ID of the project category being found</param>
        public ActionResult Deactivate(int? projectCategoryID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectCategoryController();

            ProjectCategoryViewModel projectCategoryViewModel = new ProjectCategoryViewModel();

            ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();

            projectCategoryDTO = controller.LookupProjectCategory(projectCategoryID.Value);

            projectCategoryViewModel.ProjectCategoryID = projectCategoryDTO.ProjectCategoryID;
            projectCategoryViewModel.CategoryName = projectCategoryDTO.CategoryName;
            projectCategoryViewModel.Description = projectCategoryDTO.Description;
            projectCategoryViewModel.Global = projectCategoryDTO.Global;
            projectCategoryViewModel.Color = projectCategoryDTO.Color;
            projectCategoryViewModel.ActivationDate = projectCategoryDTO.ActivationDate;
            projectCategoryViewModel.DeactivationDate = projectCategoryDTO.DeactivationDate;

            return View(projectCategoryViewModel);
        }

        /// <summary>
        /// POST method for the ProjectCategory Create.cshtml Form
        /// </summary>
        /// <param name="projectCategoryID">ID to base the delete on</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(ProjectCategoryViewModel projectCategoryViewModel)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ProjectCategoryController();
            ProjectCategoryDTO projectCategoryDTO = new ProjectCategoryDTO();

            var deactivatedProjectCategory = controller.LookupProjectCategory(projectCategoryViewModel.ProjectCategoryID);
            TempData["message"] = "The project category" + " " + "'" + deactivatedProjectCategory.CategoryName + "'" + " " + "was successfully deactivated.";
            //controller.DeactivateProjectCategory(ID, UserID);
            if (ModelState.IsValid)
            {
                controller.DeactivateProjectCategory(projectCategoryDTO, IdentityHelper.GetEmployeeID());
                return RedirectToAction("Index");

            }
            return View();
        }

        /// <summary>
        /// POST method for the ScheduleType Deactivate.cshtml Form
        /// </summary>
        /// <param name="scheduleTypeViewModel">viewModel for the information on the form</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandling/Index.cshtml"
            };
        }

    }
}