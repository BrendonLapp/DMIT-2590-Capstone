using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs;
using Capstone.Web.Models.Position;
using Capstone.Web.Admin;
using Capstone.BLL.Security;

namespace Capstone.Web.Views
{
    /// <summary>
    /// View Controller for views in the Views/Position folder
    /// </summary>
    public class PositionViewController : Controller
    {
        //private CapstoneContext db = new CapstoneContext();
        private PositionController controller = new PositionController();


        /// <summary>
        /// GET method for the Position Index.cshtml page
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var positionList = new List<PositionViewModel>();
            foreach (var position in controller.LookUpPosition())
            {
                positionList.Add(new PositionViewModel
                {
                    PositionID = position.PositionID,
                    PositionTitle = position.PositionTitle,
                    Description = position.Description,
                    ActivationDate = position.ActivationDate,
                    DeactivationDate = position.DeactivationDate

                });
            }
            return View(positionList);

        }

        /// <summary>
        /// GET method for the Position Details.cshtml Page
        /// </summary>
        /// <param name="id">The id of the selected position</param>
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

            var positionDTO = controller.LookUpPosition(id.Value);
            var positionViewModel = new PositionViewModelEdit
            {
                PositionID = positionDTO.PositionID,
                PositionTitle = positionDTO.PositionTitle,
                Description = positionDTO.Description,
                ActivationDate = positionDTO.ActivationDate,
                DeactivationDate = positionDTO.DeactivationDate,
                CreationDate = positionDTO.CreationDate,
                UpdatedDate = positionDTO.UpdateDate
            };

            if (positionViewModel == null)
            {
                return HttpNotFound();
            }

            return View(positionViewModel);

        }

        /// <summary>
        /// GET method for the Position Create.cshtml Form
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
        /// POST method for the Position Create.cshtml Form
        /// </summary>
        /// <param name="positionViewModel">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PositionViewModel positionViewModel)
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
                    var positionDTO = new PositionDTO
                    {
                        PositionID = positionViewModel.PositionID,
                        PositionTitle = positionViewModel.PositionTitle,
                        Description = positionViewModel.Description,
                        ActivationDate = positionViewModel.ActivationDate,
                        DeactivationDate = positionViewModel.DeactivationDate
                    };
                    controller.CreatePosition(positionDTO, IdentityHelper.GetEmployeeID());
                    TempData["message"] = "The position" + " " + "'" + positionDTO.PositionTitle + "'" + " " + "was successfully created.";
                    return RedirectToAction("Index");
                }

                return View(positionViewModel);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(positionViewModel);
            }
        }


        /// <summary>
        /// GET method for the Position Edit.cshtml Form
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var positionDTO = controller.LookUpPosition(id.Value);
            var positionViewModel = new PositionViewModelEdit
            {
                PositionID = positionDTO.PositionID,
                PositionTitle = positionDTO.PositionTitle,
                Description = positionDTO.Description,
                ActivationDate = positionDTO.ActivationDate,
                DeactivationDate = positionDTO.DeactivationDate,
            };

            if (positionViewModel == null)
            {
                return HttpNotFound();
            }

            return View(positionViewModel);
        }

        /// <summary>
        /// POST method for the Position Edit.cshtml Form
        /// </summary>
        /// <param name="positionViewModelEdit">viewModel for the information on the form</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "PositionID,PositionTitle,Description,ActivationDate,DeactivationDate")]*/ PositionViewModelEdit positionViewModelEdit)
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
                    var positionEditDTO = new PositionEditDTO
                    {
                        PositionID = positionViewModelEdit.PositionID,
                        PositionTitle = positionViewModelEdit.PositionTitle,
                        Description = positionViewModelEdit.Description,
                        ActivationDate = positionViewModelEdit.ActivationDate,
                        DeactivationDate = positionViewModelEdit.DeactivationDate
                    };
                    controller.EditPosition(positionEditDTO, IdentityHelper.GetEmployeeID());
                    TempData["message"] = "The position" + " " + "'" + positionEditDTO.PositionTitle + "'" + " " + "was successfully updated.";
                    return RedirectToAction("index");

                }
                return View(positionViewModelEdit);
            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(positionViewModelEdit);
            }


        }



        /// <summary>
        /// GET method for the Position Deactivate.cshtml Form
        /// </summary>
        /// <param name="id">The id of the selected position</param>
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
            var positionEditDTO = controller.LookUpPosition(id.Value);
            var postitionViewModelEdit = new PositionViewModelEdit
            {
                PositionID = positionEditDTO.PositionID,
                PositionTitle = positionEditDTO.PositionTitle,
                Description = positionEditDTO.Description,
                ActivationDate = positionEditDTO.ActivationDate,
                DeactivationDate = positionEditDTO.DeactivationDate,
                CreationDate = positionEditDTO.CreationDate,
                UpdatedDate = positionEditDTO.UpdateDate

            };

            if (postitionViewModelEdit == null)
            {
                return HttpNotFound();
            }
            return View(postitionViewModelEdit);
        }

        /// <summary>
        /// POST method for the Position Deactivate.cshtml Form
        /// </summary>
        /// <param name="id">the id of the position that is going to be deactivated</param>
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var deactivatedPosition = controller.LookUpPosition(id);
            TempData["message"] = "The position" + " " + "'" + deactivatedPosition.PositionTitle + "'" + " " + "was successfully deactivated.";

            controller.DeactivatePosition(id, IdentityHelper.GetEmployeeID());
            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;


            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandling/Index.cshtml",

            };
        }
    }
}
