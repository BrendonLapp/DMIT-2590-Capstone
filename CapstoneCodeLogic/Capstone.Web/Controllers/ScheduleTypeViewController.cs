using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL;
using Capstone.BLL.DTOs;
using Capstone.Web.Models;
using Capstone.Web.Admin;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    public class ScheduleTypeViewController : Controller
    {
        /// <summary>
        /// GET method for the ScheduleType Index.cshtml Form
        /// </summary>
        /// <returns>List of ScheduleTypes as viewModel</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ScheduleTypeController();

            List<ScheduleTypeDTO> scheduleTypeDTOs = controller.LookupScheduleTypes();

            List<ScheduleTypeViewModel> viewModel = new List<ScheduleTypeViewModel>();

            foreach (var item in scheduleTypeDTOs)
            {
                viewModel.Add(new ScheduleTypeViewModel
                {
                    ScheduleTypeID = item.ScheduleTypeID,
                    Name = item.Name,
                    Description = item.Description,
                    HoursPerDay = item.HoursPerDay,
                    ActivationDate = item.ActivationDate,
                    DeactivationDate = item.DeactivationDate
                });
            }

            return View(viewModel);
        }

        /// <summary>
        /// GET method for the ScheduleType Details.cshtml Form
        /// </summary>
        /// <returns>ScheduleTypeViewModel as scheduleTypeViewModel</returns>
        /// <param name="scheduleTypeID">TB_Capstone_SCHEDULE_TYPEs primary key</param>
        public ActionResult Details(int? scheduleTypeID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ScheduleTypeController();
            if (scheduleTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleTypeDTO = controller.LookupScheduleType(scheduleTypeID.Value);
            var scheduleTypeViewModel = new ScheduleTypeViewModel
            {
                ScheduleTypeID = scheduleTypeDTO.ScheduleTypeID,
                Name = scheduleTypeDTO.Name,
                Description = scheduleTypeDTO.Description,
                HoursPerDay = scheduleTypeDTO.HoursPerDay,
                ActivationDate = scheduleTypeDTO.ActivationDate,
                DeactivationDate = scheduleTypeDTO.DeactivationDate
            };

            if (scheduleTypeViewModel == null)
            {
                return HttpNotFound();
            }

            return View(scheduleTypeViewModel);
        }
        /// <summary>
        /// GET method for the ScheduleType Create.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            return View();
        }

        /// <summary>
        /// POST method for the ScheduleType Create.cshtml Form
        /// </summary>
        /// <returns>ScheduleTypeVIewModel as scheduleType</returns>
        /// <param name="scheduleType">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScheduleTypeViewModel scheduleType)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var controller = new ScheduleTypeController();

                    ScheduleTypeDTO scheduleTypeDTO = new ScheduleTypeDTO();

                    scheduleTypeDTO.Name = scheduleType.Name;
                    scheduleTypeDTO.Description = scheduleType.Description;
                    scheduleTypeDTO.HoursPerDay = scheduleType.HoursPerDay;

                    scheduleTypeDTO.ActivationDate = scheduleType.ActivationDate;
                    scheduleTypeDTO.DeactivationDate = scheduleType.DeactivationDate;

                    string userID = User.Identity.Name;

                    controller.CreateNewScheduleType(scheduleTypeDTO, IdentityHelper.GetEmployeeID());
                    TempData["message"] = "The schedule type" + " " + "'" + scheduleTypeDTO.Name + "'" + " " + "was successfully created.";
                    return RedirectToAction("Index");
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(scheduleType);
        }

        /// <summary>
        /// GET method for the ScheduleType Edit.cshtml Form
        /// </summary>
        /// <returns>ScheduleTypeViewModel as viewModel</returns>
        /// <param name="scheduleTypeID">The ID of the schedule type to be editted</param>
        public ActionResult Edit(int? scheduleTypeID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ScheduleTypeController();
            ScheduleTypeViewModel viewModel = new ScheduleTypeViewModel();
            ScheduleTypeDTO scheduleTypeDTO = new ScheduleTypeDTO();

            if (scheduleTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            scheduleTypeDTO = controller.LookupScheduleType(scheduleTypeID.Value);

            viewModel.ScheduleTypeID = scheduleTypeDTO.ScheduleTypeID;
            viewModel.Name = scheduleTypeDTO.Name;
            viewModel.Description = scheduleTypeDTO.Description;
            viewModel.HoursPerDay = scheduleTypeDTO.HoursPerDay;
            viewModel.ActivationDate = scheduleTypeDTO.ActivationDate;
            viewModel.DeactivationDate = scheduleTypeDTO.DeactivationDate;

            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        /// <summary>
        /// POST method for the ScheduleType Edit.cshtml Form
        /// </summary>
        /// <returns>ScheduleTypeViewModel as scheduleTyp</returns>
        /// <param name="scheduleType">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScheduleTypeViewModel scheduleType)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var controller = new ScheduleTypeController();

                    ScheduleTypeDTO scheduleTypeDTO = new ScheduleTypeDTO();

                    scheduleTypeDTO.ScheduleTypeID = scheduleType.ScheduleTypeID;
                    scheduleTypeDTO.Name = scheduleType.Name;
                    scheduleTypeDTO.Description = scheduleType.Description;
                    scheduleTypeDTO.HoursPerDay = scheduleType.HoursPerDay;
                    scheduleTypeDTO.ActivationDate = scheduleType.ActivationDate;
                    scheduleTypeDTO.DeactivationDate = scheduleType.DeactivationDate;

                    string userID = User.Identity.Name;

                    TempData["message"] = "The schedule type" + " " + "'" + scheduleTypeDTO.Name + "'" + " " + "was successfully updated.";
                    controller.UpdateScheduleType(scheduleTypeDTO, IdentityHelper.GetEmployeeID());

                    controller.UpdateScheduleType(scheduleTypeDTO, IdentityHelper.GetEmployeeID());
                    return RedirectToAction("Index");
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(scheduleType);
        }

        /// <summary>
        /// Get method for the ScheduleType Deactivate.cshtml file
        /// </summary>
        /// <returns>ScheduleTypeViewModel as scheduleTypeViewModel</returns>
        /// <param name="scheduleTypeID">The ID of the selected schedule type</param>
        public ActionResult Deactivate(int? scheduleTypeID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ScheduleTypeController();
            ScheduleTypeViewModel scheduleTypeViewModel = new ScheduleTypeViewModel();
            ScheduleTypeDTO scheduleTypeDTO = new ScheduleTypeDTO();

            if (scheduleTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                scheduleTypeDTO = controller.LookupScheduleType(scheduleTypeID.Value);

                scheduleTypeViewModel.ScheduleTypeID = scheduleTypeDTO.ScheduleTypeID;
                scheduleTypeViewModel.Name = scheduleTypeDTO.Name;
                scheduleTypeViewModel.Description = scheduleTypeDTO.Description;
                scheduleTypeViewModel.HoursPerDay = scheduleTypeDTO.HoursPerDay;
                scheduleTypeViewModel.ActivationDate = scheduleTypeDTO.ActivationDate;
                scheduleTypeViewModel.DeactivationDate = scheduleTypeDTO.DeactivationDate;

                if (scheduleTypeViewModel == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(scheduleTypeViewModel);
        }

        /// <summary>
        /// POST method for the ScheduleType Deactivate.cshtml Form
        /// </summary>
        /// <returns>ScheduleTypeViewModel as scheduleTypeViewModel</returns>
        /// <param name="scheduleTypeViewModel">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new ScheduleTypeController();
            ScheduleTypeDTO scheduleTypeDTO = new ScheduleTypeDTO();

            if (ModelState.IsValid)
            {
                scheduleTypeDTO.ScheduleTypeID = scheduleTypeViewModel.ScheduleTypeID;
                var deactivatedScheduleType = controller.LookupScheduleType(scheduleTypeDTO.ScheduleTypeID);
                TempData["message"] = "The schedule type" + " " + "'" + deactivatedScheduleType.Name + "'" + "was successfully deactivated";

                try
                {
                    controller.DeactivateScheduleType(deactivatedScheduleType, IdentityHelper.GetEmployeeID());
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(scheduleTypeViewModel);
        }

        /// <summary>
        /// Method to redirect to an error handling page.
        /// </summary>
        /// <param name="filterContext">Context variable to track the exceptions context</param>
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