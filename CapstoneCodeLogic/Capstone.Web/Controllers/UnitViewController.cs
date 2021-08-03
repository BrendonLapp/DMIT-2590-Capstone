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
    /// View Controller for views in the Views/UnitView folder
    /// </summary>
    public class UnitViewController : Controller
    {

        private UnitController controller = new UnitController();
        private AreaController areaController = new AreaController();
        /// <summary>
        /// GET method for the Unit Index.cshtml listing
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var unitVMList = new List<UnitViewModel>();
            foreach (UnitDTO unit in controller.LookupUnit())
            {
                unitVMList.Add(new UnitViewModel
                {
                    UnitID = unit.UnitID,
                    AreaID = unit.AreaID,
                    UnitName = unit.UnitName,
                    Description = unit.Description,
                    ActivationDate = unit.ActivationDate,
                    DeactivationDate = unit.DeactivationDate,
                    Area = unit.Area
                });

            }
            return View(unitVMList);
        }

        /// <summary>
        /// GET method for the Unit Details.cshtml Form
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
            UnitDTO unitDTO = controller.LookupUnit(id.Value);
            var unitVM = new UnitViewModel()
            {
                UnitID = unitDTO.UnitID,
                UnitName = unitDTO.UnitName,
                Description = unitDTO.Description,
                AreaID = unitDTO.AreaID,
                ActivationDate = unitDTO.ActivationDate,
                DeactivationDate = unitDTO.DeactivationDate,
                AreaName = unitDTO.Area.AreaName
            };
            if (unitVM == null)
            {
                return HttpNotFound();
            }

            return View(unitVM);
        }


        /// <summary>
        /// GET method for the Unit Create.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }

            ViewBag.AreaID = new SelectList(areaController.LookupArea(), "AreaID", "AreaName");

            return View();
        }

        /// <summary>
        /// POST method for the Unit Create.cshtml Form
        /// </summary>
        /// <param name="unitVM">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnitViewModel unitVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            ViewBag.AreaSearch = new SelectList(areaController.LookupArea(), "AreaID", "AreaName");
            try
            {
                if (ModelState.IsValid)
                {
                    var unitDTO = new UnitDTO
                    {

                        UnitName = unitVM.UnitName,
                        AreaID = unitVM.AreaID,
                        Description = unitVM.Description,
                        ActivationDate = unitVM.ActivationDate,
                        DeactivationDate = unitVM.DeactivationDate
                    };

                    controller.CreateUnit(unitDTO, IdentityHelper.GetEmployeeID());

                    TempData["message"] = "The unit" + " " + "'" + unitDTO.UnitName + "'" + " " + "was successfully created.";

                    return RedirectToAction("Index");



                }

                return View(unitVM);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(unitVM);
            }


        }

        /// <summary>
        /// GET method for the Unit Edit.cshtml Form
        /// </summary>
        /// <param name="id">Key for TB_Capstone_UNIT table</param>
        public ActionResult Edit(int? id)
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
            UnitDTO unitDTO = controller.LookupUnit(id.Value);
            var unitVM = new UnitViewModel
            {
                UnitID = unitDTO.UnitID,
                UnitName = unitDTO.UnitName,
                AreaID = unitDTO.AreaID,
                Description = unitDTO.Description,
                ActivationDate = unitDTO.ActivationDate,
                DeactivationDate = unitDTO.DeactivationDate
            };
            if (unitVM == null)
            {
                return HttpNotFound();
            }


            ViewBag.AreaID = new SelectList(areaController.LookupArea(), "AreaID", "AreaName");
            return View(unitVM);
        }

        /// <summary>
        /// POST method for the Unit Edit.cshtml Form, Saves edited Unit information
        /// </summary>
        /// <remarks>Called when pressing the Save button</remarks>
        /// <param name="unitVM">viewModel for the information on the form</param>
        [HttpPost]
        public ActionResult Edit(int id, UnitViewModel unitVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            ViewBag.AreaID = new SelectList(areaController.LookupArea(), "AreaID", "AreaName");
            try
            {
                if (ModelState.IsValid)
                {

                    var unitDTO = new UnitDTO
                    {
                        UnitID = unitVM.UnitID,
                        UnitName = unitVM.UnitName,
                        AreaID = unitVM.AreaID,
                        Description = unitVM.Description,
                        ActivationDate = unitVM.ActivationDate,
                        DeactivationDate = unitVM.DeactivationDate,
                        Area = areaController.LookupArea(unitVM.AreaID)
                    };
                    unitVM.Area = areaController.LookupArea(unitVM.AreaID);
                    controller.EditUnit(unitDTO, IdentityHelper.GetEmployeeID());

                    TempData["message"] = "The unit" + " " + "'" + unitDTO.UnitName + "'" + " " + "was successfully updated.";
                    return RedirectToAction("Index");
                }

                return View(unitVM);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(unitVM);
            }
        }

        /// <summary>
        /// GET method for the Unit Deactivate.cshtml Form
        /// </summary>
        /// <remarks>Displays details of the deleted Unit as a confirmation</remarks>
        /// <param name="id">Key for TB_Capstone_UNIT table</param>
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
            UnitDTO unitDTO = controller.LookupUnit(id.Value);
            var unitVM = new UnitViewModel()
            {
                UnitID = unitDTO.UnitID,
                UnitName = unitDTO.UnitName,
                Description = unitDTO.Description,
                AreaID = unitDTO.AreaID,
                ActivationDate = unitDTO.ActivationDate,
                DeactivationDate = unitDTO.DeactivationDate,
                AreaName = unitDTO.Area.AreaName
            };
            if (unitVM == null)
            {
                return HttpNotFound();
            }

            return View(unitVM);
        }



        /// <summary>
        /// POST method for the Unit Deactivate.cshtml Form, deactivates selected Unit information
        /// </summary>
        /// <remarks>Called when pressing the Deactivate button</remarks>
        /// <param name="id">Key for TB_Capstone_UNIT table</param>
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var deactivatedUnit = controller.LookupUnit(id);
            TempData["message"] = "The unit" + " " + "'" + deactivatedUnit.UnitName + "'" + " " + "was successfully deactivated.";

            controller.DeactivateUnit(id, IdentityHelper.GetEmployeeID());
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
