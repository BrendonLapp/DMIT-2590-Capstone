using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs.OvertimeRequestDTOs;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.Web.Models.OvertimeRequst;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// View Controller for views in the Views/OvertimeRequest folder
    /// </summary>
    public class OvertimeRequestViewController : Controller
    {
        /// <summary>
        /// GET method for the OvertimeRequest Index.cshtml page. Loads the page based on a team ID
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            List<TeamOvertimeViewModel> overtimeViewModels = new List<TeamOvertimeViewModel>();
            List<TeamOvertimeRequestDTO> requestDTOs = new List<TeamOvertimeRequestDTO>();
            OvertimeRequestController controller = new OvertimeRequestController();

            requestDTOs = controller.LookupOvertimeByTeam(IdentityHelper.GetTeamID());

            foreach (var request in requestDTOs)
            {
                overtimeViewModels.Add(new TeamOvertimeViewModel
                {
                    OvertimeID = request.OvertimeID,
                    OvertimeTypeID = request.OvertimeTypeID,
                    OvertimeTypeName = request.OvertimeTypeName,
                    EmployeeID = request.EmployeeID,
                    ProjectDetailID = request.ProjectID,
                    EmployeeName = request.EmployeeName,
                    //ProjectDetailID = request.ProjectDetailID,
                    ProjectName = request.ProjectName,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    SubmissionDate = request.SubmissionDate,
                    ReviewDate = request.ReviewDate,
                    Amount = request.Amount,
                    Approved = request.Approved,
                    ApprovalNotes = request.ApprovalNotes,
                    SubmissionNotes = request.SubmissionNotes
                });
            }

            return View(overtimeViewModels);
        }

        /// <summary>
        /// Post method for the OvertimeRequest Index.cshtml page. Handles either an approve or deny button click to update the overtime request
        /// </summary>
        /// <param name="buttonName">The name of the button that was pressed</param>
        /// <param name="hiddenOvertimeID">The hidden value that holds the chosen row's overtime ID to update</param>
        /// <param name="submissionMessage">The message inputted by the user to go along with the approval or denial</param>
        [HttpPost]
        public ActionResult Index(int? hiddenOvertimeID, string submissionMessage, string buttonName)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (hiddenOvertimeID == null)
            {
                ModelState.AddModelError(string.Empty, "Overtime does not exist.");
                return Index();
            }

            var controller = new OvertimeRequestController();

            if (buttonName == "Approve")
            {
                try
                {
                    controller.Approve(hiddenOvertimeID.Value, submissionMessage, IdentityHelper.GetEmployeeID());
                    return Index();
                }
                catch (AggregateException ex)
                {
                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
            }
            else if (buttonName == "Deny")
            {
                try
                {
                    controller.Deny(hiddenOvertimeID.Value, submissionMessage, IdentityHelper.GetEmployeeID());
                    return Index();
                }
                catch (AggregateException ex)
                {
                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
            }

            return Index();
        }

        /// <summary>
        /// GET method for the OvertimeRequest Index.cshtml page. Loads the page based on an employeeID
        /// </summary>
        public ActionResult PersonalOvertimeRequests()
        {
            List<TeamOvertimeViewModel> overtimeViewModels = new List<TeamOvertimeViewModel>();
            List<TeamOvertimeRequestDTO> requestDTOs = new List<TeamOvertimeRequestDTO>();
            OvertimeRequestController controller = new OvertimeRequestController();

            requestDTOs = controller.LookupOvertimeByEmployee(IdentityHelper.GetEmployeeID());

            foreach (var request in requestDTOs)
            {
                overtimeViewModels.Add(new TeamOvertimeViewModel
                {
                    OvertimeID = request.OvertimeID,
                    OvertimeTypeID = request.OvertimeTypeID,
                    OvertimeTypeName = request.OvertimeTypeName,
                    EmployeeID = request.EmployeeID,
                    ProjectDetailID = request.ProjectID,
                    EmployeeName = request.EmployeeName,
                    //ProjectDetailID = request.ProjectDetailID,
                    ProjectName = request.ProjectName,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    SubmissionDate = request.SubmissionDate,
                    ReviewDate = request.ReviewDate,
                    Amount = request.Amount,
                    Approved = request.Approved,
                    ApprovalNotes = request.ApprovalNotes,
                    SubmissionNotes = request.SubmissionNotes
                });
            }

            return View(overtimeViewModels);
        }
    }
}