using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// View Controller for views in the Views/OvertimeTypeView folder
    /// </summary>
    public class OvertimeTypeViewController : Controller
    {
        /// <summary>
        /// GET method for the OvertimeType Index.cshtml listing
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OvertimeTypeController();
                List<OvertimeTypeDTO> overtimeTypeList = controller.LookupOvertimeType();
                var overtimeTypeViewList = new List<OvertimeTypeViewModel>();

                foreach (var item in overtimeTypeList)
                {
                    var overtimeViewItem = new OvertimeTypeViewModel();
                    overtimeViewItem.OvertimeTypeID = item.OvertimeTypeID;
                    overtimeViewItem.Name = item.Name;
                    overtimeViewItem.Description = item.Description;
                    overtimeViewItem.PayMultiplier = item.PayMultiplier;
                    overtimeViewItem.ActivationDate = item.ActivationDate;
                    overtimeViewItem.DeactivationDate = item.DeactivationDate;

                    overtimeTypeViewList.Add(overtimeViewItem);
                }

                return View(overtimeTypeViewList);
            }
        }

        /// <summary>
        /// GET method for the OvertimeType Create.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// POST method for the OvertimeType Create.cshtml Form
        /// </summary>
        /// <param name="overtimeType">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] OvertimeTypeViewModel overtimeType)
        {
            try
            {
                if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
                {
                    Response.StatusCode = 403;
                    return new EmptyResult();
                }
                else
                {
                    ModelState.Remove(nameof(overtimeType.OvertimeTypeID));

                    if (ModelState.IsValid)
                    {
                        var newOvertimeType = new OvertimeTypeDTO();
                        var controller = new OvertimeTypeController();

                        newOvertimeType.Name = overtimeType.Name;
                        newOvertimeType.Description = overtimeType.Description;
                        newOvertimeType.PayMultiplier = overtimeType.PayMultiplier;
                        newOvertimeType.ActivationDate = overtimeType.ActivationDate;
                        newOvertimeType.DeactivationDate = overtimeType.DeactivationDate;

                        TempData["message"] = $"\"{newOvertimeType.Name}\" was successfully created.";
                        controller.CreateOvertimeType(newOvertimeType, IdentityHelper.GetEmployeeID());
                        return RedirectToAction("Index");
                    }

                    return View(overtimeType);
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(overtimeType);
            }

        }

        /// <summary>
        /// GET method for the OvertimeType Edit.cshtml Form
        /// </summary>
        /// <param name="overtimeTypeID">Key for TB_Capstone_OVERTIME_TYPE table</param>
        public ActionResult Edit(string overtimeTypeID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OvertimeTypeController();
                if (overtimeTypeID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int overtimeTypeIDInt = int.Parse(overtimeTypeID);
                OvertimeTypeDTO overtimeType = controller.LookupOvertimeType(overtimeTypeIDInt);
                if (overtimeType == null)
                {
                    return HttpNotFound();
                }
                var overtimeView = new OvertimeTypeViewModel();

                overtimeView.OvertimeTypeID = overtimeType.OvertimeTypeID;
                overtimeView.Name = overtimeType.Name;
                overtimeView.Description = overtimeType.Description;
                overtimeView.PayMultiplier = overtimeType.PayMultiplier;
                overtimeView.ActivationDate = overtimeType.ActivationDate;
                overtimeView.DeactivationDate = overtimeType.DeactivationDate;


                return View(overtimeView);
            }
        }

        /// <summary>
        /// POST method for the OvertimeType Edit.cshtml Form, Saves edited OverTimeType information
        /// </summary>
        /// <remarks></remarks>
        /// <param name="overtimeTypeView">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] OvertimeTypeViewModel overtimeTypeView)
        {
            try
            {
                if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
                {
                    Response.StatusCode = 403;
                    return new EmptyResult();
                }
                else
                {
                    //ModelState.Remove("ActivationDate");
                    //ModelState.Remove("DeactivationDate");
                    if (ModelState.IsValid)
                    {
                        var controller = new OvertimeTypeController();
                        var editedOvertimeType = new OvertimeTypeDTO();

                        editedOvertimeType.OvertimeTypeID = overtimeTypeView.OvertimeTypeID;
                        editedOvertimeType.Name = overtimeTypeView.Name;
                        editedOvertimeType.Description = overtimeTypeView.Description;
                        editedOvertimeType.PayMultiplier = overtimeTypeView.PayMultiplier;
                        controller.EditOvertimeType(editedOvertimeType, IdentityHelper.GetEmployeeID());

                        TempData["message"] = $"The overtime type \"{editedOvertimeType.Name}\" has been updated.";
                        return RedirectToAction("Index");
                    }


                    return View(overtimeTypeView);
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(overtimeTypeView);
            }
        }

        /// <summary>
        /// Get method for the OvertimeType Delete.cshtml Form
        /// </summary>
        /// <param name="overtimeTypeID">Key for TB_Capstone_OVERTIME_TYPE table</param>
        public ActionResult Deactivate(string overtimeTypeID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OvertimeTypeController();
                if (overtimeTypeID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int overtimeTypeIDInt = int.Parse(overtimeTypeID);
                OvertimeTypeDTO overtimeType = controller.LookupOvertimeType(overtimeTypeIDInt);
                if (overtimeType == null)
                {
                    return HttpNotFound();
                }
                var overtimeView = new OvertimeTypeViewModel();

                overtimeView.OvertimeTypeID = overtimeType.OvertimeTypeID;
                overtimeView.Name = overtimeType.Name;
                overtimeView.Description = overtimeType.Description;
                overtimeView.PayMultiplier = overtimeType.PayMultiplier;
                overtimeView.ActivationDate = overtimeType.ActivationDate;
                overtimeView.DeactivationDate = overtimeType.DeactivationDate;


                return View(overtimeView);
            }
        }

        /// <summary>
        /// POST method for Delete.cshtml, deactivates the selected overtime type
        /// </summary>
        /// <param name="overtimeTypeView">View model for the selected Overtime Type</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(OvertimeTypeViewModel overtimeTypeView)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OvertimeTypeController();
                OvertimeTypeDTO currentOvertimeType = controller.LookupOvertimeType(overtimeTypeView.OvertimeTypeID);
                TempData["message"] = $"\"{currentOvertimeType.Name}\" was deactivated successfully";
                controller.DeactivateOvertimeType(overtimeTypeView.OvertimeTypeID, IdentityHelper.GetEmployeeID());


                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string overtimeTypeID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OvertimeTypeController();
                if (overtimeTypeID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int overtimeTypeIDInt = int.Parse(overtimeTypeID);
                OvertimeTypeDTO overtimeType = controller.LookupOvertimeType(overtimeTypeIDInt);
                if (overtimeType == null)
                {
                    return HttpNotFound();
                }
                var overtimeView = new OvertimeTypeViewModel();

                overtimeView.OvertimeTypeID = overtimeType.OvertimeTypeID;
                overtimeView.Name = overtimeType.Name;
                overtimeView.Description = overtimeType.Description;
                overtimeView.PayMultiplier = overtimeType.PayMultiplier;
                overtimeView.ActivationDate = overtimeType.ActivationDate;
                overtimeView.DeactivationDate = overtimeType.DeactivationDate;


                return View(overtimeView);
            }
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