using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.Web.Models;
using Capstone.BLL.DTOs;
using Capstone.Web.Admin;
using System;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// View Controller for views in the Views/DepartmentView folder
    /// </summary>
    public class DepartmentViewController : Controller
    {
        internal class StatusMessageData
        {
            public string StatusMessage { get; set; }
            public string MessageStyle { get; set; }

            public StatusMessageData()
            {

            }

            public StatusMessageData(string statusMessage, string messageStyle)
            {
                StatusMessage = statusMessage;
                MessageStyle = messageStyle;
            }
        }

        //private CapstoneContext db = new CapstoneContext();
        private DepartmentController controller = new DepartmentController();

        /// <summary>
        /// GET method for the Department Index.cshtml listing
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var message = TempData["statusMessage"] as StatusMessageData;
            var departmentVMList = new List<DepartmentViewModel>();
            foreach (var dept in controller.LookupDepartment())
            {
                departmentVMList.Add(new DepartmentViewModel
                {
                    DepartmentID = dept.DepartmentID,
                    DepartmentName = dept.DepartmentName,
                    Description = dept.Description,
                    ActivationDate = dept.ActivationDate,
                    DeactivationDate = dept.DeactivationDate
                });
            }
            return View(departmentVMList);
        }

        /// <summary>
        /// GET method for the Department Details.cshtml Form
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

            var departmentDTO = controller.LookupDepartment(id.Value);
            var departmentViewModel = new DepartmentViewModel
            {
                DepartmentID = departmentDTO.DepartmentID,
                DepartmentName = departmentDTO.DepartmentName,
                Description = departmentDTO.Description,
                ActivationDate = departmentDTO.ActivationDate,
                DeactivationDate = departmentDTO.DeactivationDate
            };
            if (departmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(departmentViewModel);
        }

        /// <summary>
        /// GET method for the Department Create.cshtml Form
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
        /// POST method for the Department Create.cshtml Form
        /// </summary>
        /// <param name="departmentViewModel">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel departmentViewModel)
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
                    var departmentDTO = new DepartmentDTO
                    {
                        DepartmentID = departmentViewModel.DepartmentID,
                        DepartmentName = departmentViewModel.DepartmentName,
                        Description = departmentViewModel.Description,
                        ActivationDate = departmentViewModel.ActivationDate,
                        DeactivationDate = departmentViewModel.DeactivationDate
                    };
                    controller.CreateDepartment(departmentDTO, IdentityHelper.GetEmployeeID());
                    TempData["message"] = "The department" + " " + "'" + departmentDTO.DepartmentName + "'" + " " + "was created successfully.";
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

            return View(departmentViewModel);

        }

        /// <summary>
        /// GET method for the Department Edit.cshtml Form
        /// </summary>
        /// <param name="id">Key for TB_Capstone_DEPARTMENT table</param>
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
            var departmentDTO = controller.LookupDepartment(id.Value);
            var departmentViewModel = new DepartmentViewModel
            {
                DepartmentID = departmentDTO.DepartmentID,
                DepartmentName = departmentDTO.DepartmentName,
                Description = departmentDTO.Description,
                ActivationDate = departmentDTO.ActivationDate,
                DeactivationDate = departmentDTO.DeactivationDate
            };
            if (departmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(departmentViewModel);
        }

        /// <summary>
        /// POST method for the Department Edit.cshtml Form, Saves edited Department information
        /// </summary>
        /// <remarks>Called when pressing the Save button</remarks>
        /// <param name="departmentViewModel">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel departmentViewModel)
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
                    var departmentDTO = new DepartmentDTO
                    {
                        DepartmentID = departmentViewModel.DepartmentID,
                        DepartmentName = departmentViewModel.DepartmentName,
                        Description = departmentViewModel.Description,
                        ActivationDate = departmentViewModel.ActivationDate,
                        DeactivationDate = departmentViewModel.DeactivationDate
                    };
                    controller.EditDepartment(departmentDTO, IdentityHelper.GetEmployeeID());
                    //db.Entry(tB_Capstone_DEPARTMENT).State = EntityState.Modified;
                    //db.SaveChanges();

                    TempData["message"] = "The department" + " " + "'" + departmentDTO.DepartmentName + "'" + " " + "was updated successfully.";
                    return RedirectToAction("Index");
                }
                return View(departmentViewModel);
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(departmentViewModel);
        }

        /// <summary>
        /// GET method for the Department Deactivate.cshtml Form
        /// </summary>
        /// <remarks>Displays details of the deleted department as a confirmation</remarks>
        /// <param name="id">Key for TB_Capstone_DEPARTMENT table</param>
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
            var departmentDTO = controller.LookupDepartment(id.Value);
            var departmentViewModel = new DepartmentViewModel
            {
                DepartmentID = departmentDTO.DepartmentID,
                DepartmentName = departmentDTO.DepartmentName,
                Description = departmentDTO.Description,
                ActivationDate = departmentDTO.ActivationDate,
                DeactivationDate = departmentDTO.DeactivationDate
            };
            if (departmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(departmentViewModel);
        }

        /// <summary>
        /// POST method for the Department Deactivate.cshtml Form, deactivates selected Department information
        /// </summary>
        /// <remarks>Called when pressing the Deactivate button</remarks>
        /// <param name="id">Key for TB_Capstone_DEPARTMENT table</param>
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var deactivatedDepartment = controller.LookupDepartment(id);
            TempData["message"] = "The department" + " " + "'" + deactivatedDepartment.DepartmentName + "'" + " " + "was deactivated successfully.";

            controller.DeactivateDepartment(id, IdentityHelper.GetEmployeeID());
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
