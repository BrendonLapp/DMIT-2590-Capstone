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
    /// View Controller for views in the Views/OffDayTypeView folder
    /// </summary>
    public class OffDayTypeViewController : Controller
    {
        /// <summary>
        /// GET method for the OffDayType Index.cshtml listing
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
                var controller = new OffDayTypeController();
                List<OffDayTypeDTO> offDayTypes = controller.LookupOffDayType();
                var offDayTypeViewList = new List<OffDayTypeViewModel>();

                foreach (var item in offDayTypes)
                {
                    var offDayViewItem = new OffDayTypeViewModel();
                    offDayViewItem.OffDayID = item.OffDayID;
                    offDayViewItem.AbbreviatedName = item.AbbreviatedName;
                    offDayViewItem.Description = item.Description;
                    offDayViewItem.PTO = item.PTO;
                    offDayViewItem.Notes = item.Notes;
                    offDayViewItem.ColorDisplayed = item.ColorDisplayed;
                    offDayViewItem.ActivationDate = item.ActivationDate;
                    offDayViewItem.DeactivationDate = item.DeactivationDate;

                    offDayTypeViewList.Add(offDayViewItem);
                }

                return View(offDayTypeViewList);
            }
        }

        /// <summary>
        /// GET method for the OffDay Type Create.cshtml Form
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
        /// POST method for the OffDayType Create.cshtml Form
        /// </summary>
        /// <param name="offDayType">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] OffDayTypeViewModel offDayType)
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
                    ModelState.Remove(nameof(offDayType.OffDayID));

                    if (ModelState.IsValid)
                    {
                        var newOffDayType = new OffDayTypeDTO();
                        var controller = new OffDayTypeController();

                        newOffDayType.AbbreviatedName = offDayType.AbbreviatedName;
                        newOffDayType.Description = offDayType.Description;
                        newOffDayType.PTO = offDayType.PTO;
                        newOffDayType.Notes = offDayType.Notes;
                        newOffDayType.ColorDisplayed = offDayType.ColorDisplayed;
                        newOffDayType.ActivationDate = offDayType.ActivationDate;
                        newOffDayType.DeactivationDate = offDayType.DeactivationDate;

                        TempData["message"] = $"\"{newOffDayType.AbbreviatedName}\" was successfully created.";
                        controller.CreateOffDayType(newOffDayType, IdentityHelper.GetEmployeeID());
                        return RedirectToAction("Index");
                    }
                    return View(offDayType);
                }
            }
            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(offDayType);
            }
        }

        /// <summary>
        /// GET method for the OffDayType Edit.cshtml page
        /// </summary>
        /// <param name="offDayID">Key for TB_Capstone_OFFDAY_TYPE table</param>
        public ActionResult Edit(string offDayID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OffDayTypeController();
                if (offDayID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int offDayTypeIDInt = int.Parse(offDayID);
                OffDayTypeDTO offDayType = controller.LookupOffDayType(offDayTypeIDInt);
                if (offDayType == null)
                {
                    return HttpNotFound();
                }
                var offDayTypeView = new OffDayTypeViewModel();

                offDayTypeView.OffDayID = offDayType.OffDayID;
                offDayTypeView.AbbreviatedName = offDayType.AbbreviatedName;
                offDayTypeView.Description = offDayType.Description;
                offDayTypeView.Notes = offDayType.Notes;
                offDayTypeView.PTO = offDayType.PTO;
                offDayTypeView.ColorDisplayed = offDayType.ColorDisplayed;
                offDayTypeView.ActivationDate = offDayType.ActivationDate;
                offDayTypeView.DeactivationDate = offDayType.DeactivationDate;

                return View(offDayTypeView);

            }
        }

        /// <summary>
        /// POST method for the OffDayType Edit.cshtml Form, Saves edited OffDayType information
        /// </summary>
        /// <remarks></remarks>
        /// <param name="offDayTypeView">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] OffDayTypeViewModel offDayTypeView)
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
                        var controller = new OffDayTypeController();
                        var editedOffDayType = new OffDayTypeDTO();

                        editedOffDayType.OffDayID = offDayTypeView.OffDayID;
                        editedOffDayType.AbbreviatedName = offDayTypeView.AbbreviatedName;
                        editedOffDayType.Description = offDayTypeView.Description;
                        editedOffDayType.Notes = offDayTypeView.Notes;
                        editedOffDayType.PTO = offDayTypeView.PTO;
                        editedOffDayType.ColorDisplayed = offDayTypeView.ColorDisplayed;
                        editedOffDayType.ActivationDate = offDayTypeView.ActivationDate;
                        editedOffDayType.DeactivationDate = offDayTypeView.DeactivationDate;

                        TempData["message"] = $"The off day type \"{editedOffDayType.AbbreviatedName}\" has been updated.";
                        return RedirectToAction("Index");
                    }


                    return View(offDayTypeView);
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(offDayTypeView);
            }

        }

        /// <summary>
        /// Get method for the OffDayType Deactivate.cshtml Form
        /// </summary>0
        /// <param name="offDayID">Key for TB_Capstone_OFFDAY_TYPE table</param>
        public ActionResult Deactivate(string offDayID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OffDayTypeController();
                if (offDayID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int offDayTypeIDInt = int.Parse(offDayID);
                OffDayTypeDTO offDayType = controller.LookupOffDayType(offDayTypeIDInt);
                if (offDayType == null)
                {
                    return HttpNotFound();
                }
                var offDayTypeView = new OffDayTypeViewModel();

                offDayTypeView.OffDayID = offDayType.OffDayID;
                offDayTypeView.AbbreviatedName = offDayType.AbbreviatedName;
                offDayTypeView.Description = offDayType.Description;
                offDayTypeView.Notes = offDayType.Notes;
                offDayTypeView.PTO = offDayType.PTO;
                offDayTypeView.ColorDisplayed = offDayType.ColorDisplayed;
                offDayTypeView.ActivationDate = offDayType.ActivationDate;
                offDayTypeView.DeactivationDate = offDayType.DeactivationDate;

                return View(offDayTypeView);
            }
        }

        /// <summary>
        /// POST method for Deactivate.cshtml, deactivates the selected off day type
        /// </summary>
        /// <param name="offDayTypeView">View model for the selected off day Type</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(OffDayTypeViewModel offDayTypeView)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OffDayTypeController();
                OffDayTypeDTO currentOffDayType = controller.LookupOffDayType(offDayTypeView.OffDayID);
                TempData["message"] = $"\"{currentOffDayType.AbbreviatedName}\" was deactivated successfully";
                controller.DeactivateOffDayType(offDayTypeView.OffDayID, IdentityHelper.GetEmployeeID());


                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string offDayID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                var controller = new OffDayTypeController();
                if (offDayID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int offDayTypeIDInt = int.Parse(offDayID);
                OffDayTypeDTO offDayType = controller.LookupOffDayType(offDayTypeIDInt);
                if (offDayType == null)
                {
                    return HttpNotFound();
                }
                var offDayTypeView = new OffDayTypeViewModel();

                offDayTypeView.OffDayID = offDayType.OffDayID;
                offDayTypeView.AbbreviatedName = offDayType.AbbreviatedName;
                offDayTypeView.Description = offDayType.Description;
                offDayTypeView.Notes = offDayType.Notes;
                offDayTypeView.PTO = offDayType.PTO;
                offDayTypeView.ColorDisplayed = offDayType.ColorDisplayed;
                offDayTypeView.ActivationDate = offDayType.ActivationDate;
                offDayTypeView.DeactivationDate = offDayType.DeactivationDate;

                return View(offDayTypeView);
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