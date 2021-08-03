using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs;
using Capstone.Web.Admin;
using Capstone.Web.Models;
using Capstone.Web.Models.Employee;
using Capstone.Web.Models.TeamAllocation;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// View Controller for views in the Views/TeamView folder
    /// </summary>
    public class TeamViewController : Controller
    {

        private TeamController controller = new TeamController();
        private UnitController unitController = new UnitController();
        private EmployeeController employeeController = new EmployeeController();
        /// <summary>
        /// GET method for the Team Index.cshtml listing
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (!User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                return RedirectToAction("Details", "TeamView", new { id = IdentityHelper.GetTeamID() });
            }
            var teamVMList = new List<TeamViewModel>();
            foreach (TeamDTO team in controller.LookupTeam())
            {
                teamVMList.Add(new TeamViewModel
                {
                    TeamID = team.TeamID,
                    UnitID = team.UnitID,
                    TeamName = team.TeamName,
                    ActivationDate = team.ActivationDate,
                    DeactivationDate = team.DeactivationDate,
                    Unit = team.Unit
                });

            }
            return View(teamVMList);
        }

        /// <summary>
        /// GET method for the Team TeamDetails.cshtml Form
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDTO teamDTO = controller.LookupTeam(id.Value);
            var teamVM = new TeamViewModel()
            {
                TeamID = teamDTO.TeamID,
                TeamName = teamDTO.TeamName,
                UnitID = teamDTO.UnitID,
                ActivationDate = teamDTO.ActivationDate,
                DeactivationDate = teamDTO.DeactivationDate,
                UnitName = teamDTO.UnitName
            };
            var currentTeamViewList = new List<TeamAssignmentViewModel>();
            List<EmployeeWithForignKeyNamesDTO> currentTeamList = employeeController.LookupEmployeeByTeam(teamVM.TeamID);
            foreach (var item in currentTeamList)
            {
                var currentTeamEmployee = new TeamAssignmentViewModel
                {
                    EmployeeID = item.EmployeeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PositionID = item.PositionID,
                    RoleID = item.RoleID,
                    TeamID = item.TeamID,
                    TeamName = item.TeamName,
                    UserID = item.UserID,
                    PositionTitle = item.PositionTitle,
                    RoleTitle = item.RoleTitle,
                    IsUnsaved = false
                };
                currentTeamViewList.Add(currentTeamEmployee);
            }
            teamVM.CurrentTeam = currentTeamViewList;
            if (teamVM == null)
            {
                return HttpNotFound();
            }

            return View(teamVM);
        }


        /// <summary>
        /// GET method for the Team CreateTeam.cshtml Form
        /// </summary>
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (!User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                return RedirectToAction("Details", "TeamView", new { id = IdentityHelper.GetTeamID() });
            }
            var teamVM = new TeamViewModel
            {
                CurrentTeam = new List<TeamAssignmentViewModel>(),
                PotentialTeam = new List<TeamAssignmentPotentialViewModel>(),
                UnsavedAssignments = new List<UnsavedAssignmentDataClass>(),
                PotentialTeamID = 0
            };
            teamVM.PotentialTeam = GetPotentialEmployeesByTeam(teamVM.PotentialTeamID.Value, teamVM.UnsavedAssignments);
            teamVM.ActivationDate = null;
            ViewBag.UnitID = new SelectList(unitController.LookupUnit(), "UnitID", "UnitName");
            ViewBag.PotentialTeamID = new SelectList(controller.LookupTeamDropdownWithoutCurrentTeam(teamVM.TeamID), "Key", "Value");
            return View(teamVM);
        }

        /// <summary>
        /// POST method for the Team CreateTeam.cshtml Form
        /// </summary>
        /// <param name="teamVM">viewModel for the information on the form</param>
        /// <param name="currentRemoveButton">Stores employee ID information for the selected employee and tracks whether the button pressed was the "-" button</param>
        /// <param name="potentialAddButton">Stores employee ID information for the selected employee and tracks whether the button pressed was the "" button</param>
        /// <param name="submitButton">Trackes whether the button pressed was the "Create" Button</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamViewModel teamVM, int? currentRemoveButton, int? potentialAddButton, string submitButton)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (!User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                return RedirectToAction("Details", "TeamView", new { id = IdentityHelper.GetTeamID() });
            }
            try
            {
                if (potentialAddButton != null)
                {
                    EmployeeWithForignKeyNamesDTO potentialEmployee = employeeController.LookupEmployee(potentialAddButton.Value);
                    if (teamVM.CurrentTeam == null)
                    {
                        teamVM.CurrentTeam = new List<TeamAssignmentViewModel>();
                    }
                    if (teamVM.UnsavedAssignments == null)
                    {
                        teamVM.UnsavedAssignments = new List<UnsavedAssignmentDataClass>();
                    }
                    teamVM.UnsavedAssignments.Add(new UnsavedAssignmentDataClass(potentialEmployee.EmployeeID, potentialEmployee.RoleID));
                    var potentialTeamEmployee = new TeamAssignmentViewModel
                    {
                        EmployeeID = potentialEmployee.EmployeeID,
                        FirstName = potentialEmployee.FirstName,
                        LastName = potentialEmployee.LastName,
                        PositionID = potentialEmployee.PositionID,
                        RoleID = potentialEmployee.RoleID,
                        TeamID = potentialEmployee.TeamID,
                        TeamName = potentialEmployee.TeamName,
                        UserID = potentialEmployee.UserID,
                        PositionTitle = potentialEmployee.PositionTitle,
                        RoleTitle = potentialEmployee.RoleTitle,
                        IsUnsaved = true
                    };
                    teamVM.PotentialTeam.Remove(teamVM.PotentialTeam.Find(x => x.EmployeeID == potentialAddButton));
                    teamVM.CurrentTeam.Add(potentialTeamEmployee);
                }
                else if (currentRemoveButton != null)
                {
                    UnsavedAssignmentDataClass removedUnsavedAssignment = teamVM.UnsavedAssignments.Find(x => x.EmployeeID == currentRemoveButton.Value);
                    TeamAssignmentViewModel removedEmployee = teamVM.CurrentTeam.Find(x => x.EmployeeID == currentRemoveButton.Value);
                    teamVM.PotentialTeam = GetPotentialEmployeesByTeam(teamVM.PotentialTeamID.Value, teamVM.UnsavedAssignments);
                    teamVM.CurrentTeam.Remove(removedEmployee);
                    teamVM.UnsavedAssignments.Remove(removedUnsavedAssignment);
                }
                else if (submitButton != null)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {

                            var teamDTO = new TeamDTO
                            {
                                TeamID = teamVM.TeamID,
                                TeamName = teamVM.TeamName,
                                UnitID = teamVM.UnitID,
                                ActivationDate = teamVM.ActivationDate.Value,
                                DeactivationDate = teamVM.DeactivationDate,
                                Unit = unitController.LookupUnit(teamVM.UnitID),
                                NewTeamMembers = teamVM.UnsavedAssignments
                            };
                            //teamVM.Unit = unitController.LookupUnit(teamVM.UnitID);
                            controller.CreateTeam(teamDTO, IdentityHelper.GetEmployeeID());

                            TempData["message"] = "The team" + " " + "'" + teamDTO.TeamName + "'" + " " + "was successfully created.";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (AggregateException ex)
                    {

                        foreach (Exception exception in ex.InnerExceptions)
                        {
                            ModelState.AddModelError(string.Empty, exception.Message);
                        }
                        return View(teamVM);
                    }
                }
                teamVM.PotentialTeam = GetPotentialEmployeesByTeam(teamVM.PotentialTeamID.Value, teamVM.UnsavedAssignments);
                if (teamVM.CurrentTeam == null)
                {
                    teamVM.CurrentTeam = new List<TeamAssignmentViewModel>();
                }
                if (teamVM.UnsavedAssignments == null)
                {
                    teamVM.UnsavedAssignments = new List<UnsavedAssignmentDataClass>();
                }
                ViewBag.UnitID = new SelectList(unitController.LookupUnit(), "UnitID", "UnitName");
                ViewBag.PotentialTeamID = new SelectList(controller.LookupTeamDropdownWithoutCurrentTeam(teamVM.TeamID), "Key", "Value", teamVM.PotentialTeamID);
                return View(teamVM);

            }

            catch (AggregateException ex)
            {

                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(teamVM);
            }
        }

        /// <summary>
        /// GET method for the Team EditTeam.cshtml Form
        /// </summary>
        /// <param name="id">Key for TB_Capstone_UNIT table</param>
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDTO teamDTO = controller.LookupTeam(id.Value);
            var teamVM = new TeamViewModel
            {
                TeamID = teamDTO.TeamID,
                TeamName = teamDTO.TeamName,
                UnitID = teamDTO.UnitID,
                ActivationDate = teamDTO.ActivationDate,
                DeactivationDate = teamDTO.DeactivationDate,
                UnsavedAssignments = new List<UnsavedAssignmentDataClass>(),
                CurrentTeam = new List<TeamAssignmentViewModel>(),
                PotentialTeam = new List<TeamAssignmentPotentialViewModel>(),
                PotentialTeamID = 0
            };
            var currentTeamViewList = new List<TeamAssignmentViewModel>();
            List<EmployeeWithForignKeyNamesDTO> currentTeamList = employeeController.LookupEmployeeByTeam(teamVM.TeamID);
            foreach (var item in currentTeamList)
            {
                var currentTeamEmployee = new TeamAssignmentViewModel
                {
                    EmployeeID = item.EmployeeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PositionID = item.PositionID,
                    RoleID = item.RoleID,
                    TeamID = item.TeamID,
                    TeamName = item.TeamName,
                    UserID = item.UserID,
                    PositionTitle = item.PositionTitle,
                    RoleTitle = item.RoleTitle,
                    IsUnsaved = false
                };
                currentTeamViewList.Add(currentTeamEmployee);
            }
            teamVM.CurrentTeam = currentTeamViewList;
            teamVM.PotentialTeam = GetPotentialEmployeesByTeam(teamVM.PotentialTeamID.Value, teamVM.UnsavedAssignments);
            if (teamVM == null)
            {
                return HttpNotFound();
            }

            ViewBag.UnitID = new SelectList(unitController.LookupUnit(), "UnitID", "UnitName");
            ViewBag.PotentialTeamID = new SelectList(controller.LookupTeamDropdownWithoutCurrentTeam(teamVM.TeamID), "Key", "Value");
            return View(teamVM);
        }

        /// <summary>
        /// POST method for the Team EditTeam.cshtml Form, Saves edited Team information
        /// </summary>
        /// <remarks>Called when pressing the Save button</remarks>
        /// <param name="teamVM">viewModel for the information on the form</param>
        /// <param name="currentRemoveButton">Stores employee ID information for the selected employee and tracks whether the button pressed was the "-" button</param>
        /// <param name="potentialAddButton">Stores employee ID information for the selected employee and tracks whether the button pressed was the "" button</param>
        /// <param name="submitButton">Trackes whether the button pressed was the "Create" Button</param>
        [HttpPost]
        public ActionResult Edit(TeamViewModel teamVM, int? currentRemoveButton, int? potentialAddButton, /*int teamID*/ string submitButton)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            try
            {
                if (potentialAddButton != null)
                {
                    EmployeeWithForignKeyNamesDTO potentialEmployee = employeeController.LookupEmployee(potentialAddButton.Value);
                    if (teamVM.CurrentTeam == null)
                    {
                        teamVM.CurrentTeam = new List<TeamAssignmentViewModel>();
                    }
                    if (teamVM.UnsavedAssignments == null)
                    {
                        teamVM.UnsavedAssignments = new List<UnsavedAssignmentDataClass>();
                    }
                    teamVM.UnsavedAssignments.Add(new UnsavedAssignmentDataClass(potentialEmployee.EmployeeID, potentialEmployee.RoleID));
                    var potentialTeamEmployee = new TeamAssignmentViewModel
                    {
                        EmployeeID = potentialEmployee.EmployeeID,
                        FirstName = potentialEmployee.FirstName,
                        LastName = potentialEmployee.LastName,
                        PositionID = potentialEmployee.PositionID,
                        RoleID = potentialEmployee.RoleID,
                        TeamID = potentialEmployee.TeamID,
                        TeamName = potentialEmployee.TeamName,
                        UserID = potentialEmployee.UserID,
                        PositionTitle = potentialEmployee.PositionTitle,
                        RoleTitle = potentialEmployee.RoleTitle,
                        IsUnsaved = true
                    };
                    teamVM.PotentialTeam.Remove(teamVM.PotentialTeam.Find(x => x.EmployeeID == potentialAddButton));
                    teamVM.CurrentTeam.Add(potentialTeamEmployee);
                }
                else if (currentRemoveButton != null)
                {
                    UnsavedAssignmentDataClass removedUnsavedAssignment = teamVM.UnsavedAssignments.Find(x => x.EmployeeID == currentRemoveButton.Value);
                    TeamAssignmentViewModel removedEmployee = teamVM.CurrentTeam.Find(x => x.EmployeeID == currentRemoveButton.Value);
                    teamVM.PotentialTeam = GetPotentialEmployeesByTeam(teamVM.PotentialTeamID.Value, teamVM.UnsavedAssignments);
                    teamVM.CurrentTeam.Remove(removedEmployee);
                    teamVM.UnsavedAssignments.Remove(removedUnsavedAssignment);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Could not move user. Please contact a system administrator.");
            }
            if (submitButton != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        var teamDTO = new TeamDTO
                        {
                            TeamID = teamVM.TeamID,
                            TeamName = teamVM.TeamName,
                            UnitID = teamVM.UnitID,
                            ActivationDate = teamVM.ActivationDate.Value,
                            DeactivationDate = teamVM.DeactivationDate,
                            Unit = unitController.LookupUnit(teamVM.UnitID),
                            NewTeamMembers = teamVM.UnsavedAssignments
                        };
                        teamVM.Unit = unitController.LookupUnit(teamVM.UnitID);
                        controller.EditTeam(teamDTO, IdentityHelper.GetEmployeeID());

                        TempData["message"] = "The team" + " " + "'" + teamDTO.TeamName + "'" + " " + "was successfully updated.";
                        return RedirectToAction("Index");
                    }
                }
                catch (AggregateException ex)
                {

                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                    return View(teamVM);
                }
            }
            teamVM.PotentialTeam = GetPotentialEmployeesByTeam(teamVM.PotentialTeamID.Value, teamVM.UnsavedAssignments);
            ViewBag.UnitID = new SelectList(unitController.LookupUnit(), "UnitID", "UnitName");
            ViewBag.PotentialTeamID = new SelectList(controller.LookupTeamDropdownWithoutCurrentTeam(teamVM.TeamID), "Key", "Value", teamVM.PotentialTeamID);
            return View(teamVM);
        }

        /// <summary>
        /// Grabs a list of employees from a specific team, from the database, while filtering out a list of inputed employees
        /// </summary>
        /// <param name="teamID">Team ID that corrosponds to the team needed </param>
        /// <param name="UnsavedAssignments">List of employees to filter out of the search</param>
        /// <returns></returns>
        private List<TeamAssignmentPotentialViewModel> GetPotentialEmployeesByTeam(int teamID, List<UnsavedAssignmentDataClass> UnsavedAssignments)
        {
            List<EmployeeWithForignKeyNamesDTO> employeeList = employeeController.LookupEmployeeByTeam(teamID, UnsavedAssignments);
            var employeeViewList = new List<TeamAssignmentPotentialViewModel>();
            var teamAssignmentViewModel = new TeamViewModel();
            foreach (var item in employeeList)
            {
                var employeeViewItem = new TeamAssignmentPotentialViewModel();

                employeeViewItem.UserID = item.UserID;
                employeeViewItem.EmployeeID = item.EmployeeID;
                employeeViewItem.FirstName = item.FirstName;
                employeeViewItem.LastName = item.LastName;
                employeeViewItem.PositionID = item.PositionID;
                employeeViewItem.RoleID = item.RoleID;
                employeeViewItem.TeamID = item.TeamID;
                employeeViewItem.RoleTitle = item.RoleTitle;
                employeeViewItem.PositionTitle = item.PositionTitle;
                employeeViewItem.TeamName = item.TeamName;

                employeeViewList.Add(employeeViewItem);
            }
            teamAssignmentViewModel.PotentialTeam = employeeViewList;
            return employeeViewList;
        }

        /// <summary>
        /// GET method for the Team DeactivateTeam.cshtml Form
        /// </summary>
        /// <remarks>Displays details of the deleted Team as a confirmation</remarks>
        /// <param name="id">Key for TB_Capstone_UNIT table</param>
        public ActionResult Deactivate(int? id)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (!User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                return RedirectToAction("Details", "TeamView", new { id = IdentityHelper.GetTeamID() });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDTO teamDTO = controller.LookupTeam(id.Value);
            var teamVM = new TeamViewModel()
            {
                TeamID = teamDTO.TeamID,
                TeamName = teamDTO.TeamName,
                UnitID = teamDTO.UnitID,
                ActivationDate = teamDTO.ActivationDate,
                DeactivationDate = teamDTO.DeactivationDate,
                UnitName = teamDTO.UnitName
            };
            if (teamVM == null)
            {
                return HttpNotFound();
            }
            var currentTeamViewList = new List<TeamAssignmentViewModel>();
            List<EmployeeWithForignKeyNamesDTO> currentTeamList = employeeController.LookupEmployeeByTeam(teamVM.TeamID);
            foreach (var item in currentTeamList)
            {
                var currentTeamEmployee = new TeamAssignmentViewModel
                {
                    EmployeeID = item.EmployeeID,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PositionID = item.PositionID,
                    RoleID = item.RoleID,
                    TeamID = item.TeamID,
                    TeamName = item.TeamName,
                    UserID = item.UserID,
                    PositionTitle = item.PositionTitle,
                    RoleTitle = item.RoleTitle,
                    IsUnsaved = false
                };
                currentTeamViewList.Add(currentTeamEmployee);
            }
            teamVM.CurrentTeam = currentTeamViewList;

            return View(teamVM);
        }



        /// <summary>
        /// POST method for the Team DeactivateTeam.cshtml Form, deactivates selected Team information
        /// </summary>
        /// <remarks>Called when pressing the Deactivate button</remarks>
        /// <param name="id">Key for TB_Capstone_TEAM table</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(TeamViewModel teamVM)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (!User.IsInRole(SecurityRoles.GlobalAdminRole))
            {
                return RedirectToAction("Details", "TeamView", new { id = IdentityHelper.GetTeamID() });
            }
            try
            {
                var deactivatedTeam = controller.LookupTeam(teamVM.TeamID);
                controller.DeactivateTeam(teamVM.TeamID, IdentityHelper.GetEmployeeID());
                TempData["message"] = "The team" + " " + "'" + deactivatedTeam.TeamName + "'" + " " + "was successfully deactivated.";


                return RedirectToAction("Index");
            }
            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                TeamDTO teamDTO = controller.LookupTeam(teamVM.TeamID);

                teamVM.TeamID = teamDTO.TeamID;
                teamVM.TeamName = teamDTO.TeamName;
                teamVM.UnitID = teamDTO.UnitID;
                teamVM.ActivationDate = teamDTO.ActivationDate;
                teamVM.DeactivationDate = teamDTO.DeactivationDate;
                teamVM.UnitName = teamDTO.UnitName;
                return View(teamVM);
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
