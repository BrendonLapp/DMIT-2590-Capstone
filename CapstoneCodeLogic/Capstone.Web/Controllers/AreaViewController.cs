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
    /// View Controller for views in the Views/AreaView folder
    /// </summary>
    public class AreaViewController : Controller
    {

        private AreaController areaController = new AreaController();
        private DepartmentController departmentController = new DepartmentController();
        /// <summary>
        /// GET method for the Area Index.cshtml listing
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            ViewBag.DepartmentID = new SelectList(departmentController.LookupDepartment(), "DepartmentID", "DepartmentName");

            var areaVMList = new List<AreaViewModel>();
            foreach (AreaDTO area in areaController.LookupArea())
            {
                areaVMList.Add(new AreaViewModel
                {
                    AreaID = area.AreaID,
                    DepartmentID = area.DepartmentID,
                    AreaName = area.AreaName,
                    Description = area.Description,
                    ActivationDate = area.ActivationDate,
                    DeactivationDate = area.DeactivationDate,
                    Department = area.Department
                });

            }
            return View(areaVMList);
        }
        /// <summary>
        /// POST method for retrieving Areas by Department ID
        /// </summary>
        /// <param name="deptId">The Department ID the areas belong too</param>
        [HttpPost]
        public PartialViewResult GetAreasByDepartment(int deptId)
        {

            var areaVMList = new List<AreaViewModel>();
            foreach (AreaDTO area in areaController.LookupAreasByDepartment(deptId))
            {
                areaVMList.Add(new AreaViewModel
                {
                    AreaID = area.AreaID,
                    DepartmentID = area.DepartmentID,
                    AreaName = area.AreaName,
                    Description = area.Description,
                    ActivationDate = area.ActivationDate,
                    DeactivationDate = area.DeactivationDate,
                    Department = area.Department
                });

            }
            return PartialView("_AreasIndex", areaVMList);
        }

        /// <summary>
        /// GET method for the Area Details.cshtml Form
        /// </summary>
        /// <param name="id">The ID of the Area details request for</param>
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AreaDTO areaDTO = areaController.LookupArea(id.Value);
            if (areaDTO == null)
            {
                TempData["message"] = $"Area at index {id} not found.";
                return RedirectToAction("Index");
            }
            var areaVM = new AreaViewModel()
            {
                AreaID = areaDTO.AreaID,
                AreaName = areaDTO.AreaName,
                Description = areaDTO.Description,
                DepartmentName = areaDTO.DepartmentName,
                ActivationDate = areaDTO.ActivationDate,
                DeactivationDate = areaDTO.DeactivationDate,
                Department = areaDTO.Department
            };

            return View(areaVM);
        }


        /// <summary>
        /// GET method for the Area Create.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var deptController = new DepartmentController();

            ViewBag.DepartmentID = new SelectList(deptController.LookupDepartment(), "DepartmentID", "DepartmentName");

            return View();
        }

        /// <summary>
        /// POST method for the Area Create.cshtml Form
        /// </summary>
        /// <param name="areaVM">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaViewModel areaVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                ViewBag.DepartmentID = new SelectList(departmentController.LookupDepartment(), "DepartmentID", "DepartmentName");

                if (ModelState.IsValid)
                {
                    var areaDTO = new AreaDTO
                    {

                        AreaName = areaVM.AreaName,
                        DepartmentID = areaVM.DepartmentID,
                        Description = areaVM.Description,
                        ActivationDate = areaVM.ActivationDate,
                        DeactivationDate = areaVM.DeactivationDate
                    };

                    areaController.CreateArea(areaDTO, IdentityHelper.GetEmployeeID());

                    TempData["message"] = "The area" + " " + "'" + areaDTO.AreaName + "'" + " " + "was successfully created.";
                    return RedirectToAction("Index");



                }

                return View(areaVM);
            }
            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(areaVM);
            }


        }

        /// <summary>
        /// GET method for the Area Edit.cshtml Form
        /// </summary>
        /// <param name="id">Key for TB_Capstone_AREA table</param>
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AreaDTO areaDTO = areaController.LookupArea(id.Value);
            if (areaDTO == null)
            {
                TempData["message"] = $"Area at index {id} not found.";
                return RedirectToAction("Index");
            }

            var areaVM = new AreaViewModel
            {
                AreaID = areaDTO.AreaID,
                AreaName = areaDTO.AreaName,
                DepartmentID = areaDTO.DepartmentID,
                Description = areaDTO.Description,
                ActivationDate = areaDTO.ActivationDate,
                DeactivationDate = areaDTO.DeactivationDate
            };
            var deptController = new DepartmentController();

            ViewBag.DepartmentSelect = new SelectList(deptController.LookupDepartment(), "DepartmentID", "DepartmentName");
            return View(areaVM);
        }

        /// <summary>
        /// POST method for the Area Edit.cshtml Form, Saves edited Area information
        /// </summary>
        /// <remarks>Called when pressing the Save button</remarks>
        /// <param name="areaVM">viewModel for the information on the form</param>
        [HttpPost]
        public ActionResult Edit(int id, AreaViewModel areaVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                ModelState.Remove("Department");
                ModelState.Remove("AreaID");
                if (ModelState.IsValid)
                {

                    var areaDTO = new AreaDTO
                    {
                        AreaID = areaVM.AreaID,
                        AreaName = areaVM.AreaName,
                        DepartmentID = areaVM.DepartmentID,
                        Description = areaVM.Description,
                        ActivationDate = areaVM.ActivationDate,
                        DeactivationDate = areaVM.DeactivationDate,
                        Department = departmentController.LookupDepartment(areaVM.DepartmentID)
                    };
                    areaVM.Department = departmentController.LookupDepartment(areaVM.DepartmentID);
                    areaController.EditArea(areaDTO, IdentityHelper.GetEmployeeID());

                    TempData["message"] = "The area" + " " + "'" + areaDTO.AreaName + "'" + " " + "was successfully updated.";

                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentSelect = new SelectList(departmentController.LookupDepartment(), "DepartmentID", "DepartmentName");
                return View(areaVM);
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(areaVM);
            }
        }

        /// <summary>
        /// GET method for the Area Deactivate.cshtml Form
        /// </summary>
        /// <remarks>Displays details of the deleted Area as a confirmation</remarks>
        /// <param name="id">Key for TB_Capstone_AREA table</param>
        public ActionResult Deactivate(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            AreaDTO areaDTO = areaController.LookupArea(id.Value);
            if (areaDTO == null)
            {
                TempData["message"] = $"Area at index {id} not found.";
                return RedirectToAction("Index");
            }
            var areaVM = new AreaViewModel()
            {
                AreaID = areaDTO.AreaID,
                AreaName = areaDTO.AreaName,
                Description = areaDTO.Description,
                DepartmentName = areaDTO.DepartmentName,
                ActivationDate = areaDTO.ActivationDate,
                DeactivationDate = areaDTO.DeactivationDate,
                Department = areaDTO.Department
            };

            return View(areaVM);
        }



        /// <summary>
        /// POST method for the Area Deactivate.cshtml Form, deactivates selected Area information
        /// </summary>
        /// <remarks>Called when pressing the Deactivate button</remarks>
        /// <param name="id">Key for TB_Capstone_AREA table</param>
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var deactivatedArea = areaController.LookupArea(id);
            TempData["message"] = "The area" + " " + "'" + deactivatedArea.AreaName + "'" + " " + "was successfully deactivated.";

            areaController.DeactivateArea(id, IdentityHelper.GetEmployeeID());
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
