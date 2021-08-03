using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.Web.Models.PaidHoliday;
using Capstone.BLL.DTOs;
using Capstone.Web.Admin;
using System;
using Capstone.BLL.Security;

namespace Capstone.Web.Views
{
    /// <summary>
    /// View Controller for views in the Views/PaidHoliday folder
    /// </summary>
    public class PaidHolidayViewController : Controller
    {
        //private CapstoneContext db = new CapstoneContext();
        PaidHolidayController controller = new PaidHolidayController();


        /// <summary>
        /// GET method for the PaidHoliday Index.cshtml page
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var paidHolidayList = new List<PaidHolidayViewModel>();
            foreach (var holiday in controller.LookUpPaidHoliday())
            {
                paidHolidayList.Add(new PaidHolidayViewModel
                {
                    PaidHolidayID = holiday.PaidHolidayID,
                    HolidayName = holiday.HolidayName,
                    HolidayDate = holiday.HolidayDate,
                    ActivationDate = holiday.ActivationDate,
                    DeactivationDate = holiday.DeactivationDate
                });
            }
            return View(paidHolidayList);
        }

        /// <summary>
        /// GET method for the PaidHoliday Details.cshtml Page
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var paidHolidayDTO = controller.LookUpPaidHoliday(id.Value);
            var paidHolidayViewModelEdit = new PaidHolidayViewModelEdit
            {
                PaidHolidayID = paidHolidayDTO.PaidHolidayID,
                HolidayName = paidHolidayDTO.HolidayName,
                HolidayDate = paidHolidayDTO.HolidayDate,
                Notes = paidHolidayDTO.Notes,
                ActivationDate = paidHolidayDTO.ActivationDate,
                DeactivationDate = paidHolidayDTO.DeactivationDate,
                CreationDate = paidHolidayDTO.CreationDate,
                UpdatedDate = paidHolidayDTO.UpdatedDate
            };

            if (paidHolidayViewModelEdit == null)
            {
                return HttpNotFound();
            }

            return View(paidHolidayViewModelEdit);
        }

        /// <summary>
        /// GET method for the PaidHoliday Create.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
            }
            return View();
        }

        /// <summary>
        /// POST method for the PaidHoliday Create.cshtml Form
        /// </summary>
        /// <param name="paidHolidayViewModel">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaidHolidayViewModel paidHolidayViewModel)
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
                    var paidHolidayDTO = new PaidHolidayDTO
                    {
                        PaidHolidayID = paidHolidayViewModel.PaidHolidayID,
                        HolidayName = paidHolidayViewModel.HolidayName,
                        HolidayDate = paidHolidayViewModel.HolidayDate,
                        ActivationDate = paidHolidayViewModel.ActivationDate,
                        DeactivationDate = paidHolidayViewModel.DeactivationDate
                    };
                    controller.CreatePaidHoliday(paidHolidayDTO, IdentityHelper.GetEmployeeID());
                    TempData["message"] = "The paid holiday" + " " + "'" + paidHolidayDTO.HolidayName + "'" + " " + "was successfully created.";
                    return RedirectToAction("Index");
                }

                return View(paidHolidayViewModel);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(paidHolidayViewModel);
            }

        }

        /// <summary>
        /// GET method for the PaidHoliday Edit.cshtml Form
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var paidHolidayEditDTO = controller.LookUpPaidHoliday(id.Value);
            var paidHolidayViewModelEdit = new PaidHolidayViewModelEdit
            {
                PaidHolidayID = paidHolidayEditDTO.PaidHolidayID,
                HolidayName = paidHolidayEditDTO.HolidayName,
                HolidayDate = paidHolidayEditDTO.HolidayDate,
                Notes = paidHolidayEditDTO.Notes,
                ActivationDate = paidHolidayEditDTO.ActivationDate,
                DeactivationDate = paidHolidayEditDTO.DeactivationDate
            };

            if (paidHolidayViewModelEdit == null)
            {
                return HttpNotFound();
            }

            return View(paidHolidayViewModelEdit);
        }

        /// <summary>
        /// POST method for the PaidHoliday Edit.cshtml Form
        /// </summary>
        /// <param name="paidHolidayViewModelEdit">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaidHolidayViewModelEdit paidHolidayViewModelEdit)
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
                    var paidHolidayEditDTO = new PaidHolidayEditDTO
                    {
                        PaidHolidayID = paidHolidayViewModelEdit.PaidHolidayID,
                        HolidayName = paidHolidayViewModelEdit.HolidayName,
                        HolidayDate = paidHolidayViewModelEdit.HolidayDate,
                        Notes = paidHolidayViewModelEdit.Notes,
                        ActivationDate = paidHolidayViewModelEdit.ActivationDate,
                        DeactivationDate = paidHolidayViewModelEdit.DeactivationDate
                    };
                    controller.EditPaidHoliday(paidHolidayEditDTO, IdentityHelper.GetEmployeeID());
                    TempData["message"] = "The paid holiday" + " " + "'" + paidHolidayEditDTO.HolidayName + "'" + " " + "was successfully updated.";
                    return RedirectToAction("index");
                }
                return View(paidHolidayViewModelEdit);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(paidHolidayViewModelEdit);
            }
        }

        /// <summary>
        /// GET method for the PaidHoliday Deactivate.cshtml Form
        /// </summary>
        public ActionResult Deactivate(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var paidHolidayEditDTO = controller.LookUpPaidHoliday(id.Value);
            var paidHolidayModelEdit = new PaidHolidayViewModelEdit
            {
                PaidHolidayID = paidHolidayEditDTO.PaidHolidayID,
                HolidayName = paidHolidayEditDTO.HolidayName,
                HolidayDate = paidHolidayEditDTO.HolidayDate,
                Notes = paidHolidayEditDTO.Notes,
                ActivationDate = paidHolidayEditDTO.ActivationDate,
                DeactivationDate = paidHolidayEditDTO.DeactivationDate,
                CreationDate = paidHolidayEditDTO.CreationDate,
                UpdatedDate = paidHolidayEditDTO.UpdatedDate

            };

            if (paidHolidayModelEdit == null)
            {
                return HttpNotFound();
            }

            return View(paidHolidayModelEdit);
        }

        /// <summary>
        /// POST method for the Position Deactivate.cshtml Form
        /// </summary>
        /// <param name="id">the id of the paidholiday that is going to be deactivated</param>
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var deactivatedPaidHoliday = controller.LookUpPaidHoliday(id);
            TempData["message"] = "The paid holiday" + " " + "'" + deactivatedPaidHoliday.HolidayName + "'" + " " + "was successfully deactivated.";


            controller.DeactivatePaidHoliday(id, IdentityHelper.GetEmployeeID());
            return RedirectToAction("Index");
        }

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
