using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs.TeamCalendarDTOs;
using Capstone.BLL.Security;
using Capstone.Web.Admin;
using Capstone.Web.Models;
using Capstone.Web.Models.TeamCalendar;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// View Controller for views in the Views/TeamCalendar folder
    /// </summary>
    ///
    public class TeamCalendarViewController : Controller
    {
        /// <summary>
        /// GET method for the TeamCalendar Index.cshtml page
        /// </summary>
        public ActionResult Index()
        {
            var teamController = new TeamController();
            var teamCalendarViewModel = new TeamCalendarViewModel();
            teamCalendarViewModel.StartDate = DateTime.Now;
            teamCalendarViewModel.TeamID = IdentityHelper.GetTeamID();
            ViewBag.TeamSelect = new SelectList(teamController.LookupTeam(), "TeamID", "TeamName");

            return View(teamCalendarViewModel);
        }

        /// <summary>
        /// Partial view for displaying team calendar
        /// </summary>
        /// <param name="teamID">The team ID of the currently logged in employee</param>
        /// <param name="startDate">Start date of the absences NOTE: Start date input not working will default to Datetime.now for first load</param>
        /// <param name="startDateString">Nullable string that is used in the event of a team swap or month swap determined by Javascript on the front end</param>
        /// <returns></returns>
        public ActionResult _TeamAbsenceCalendar(int teamID, DateTime? startDate, string startDateString)
        {


            if (!string.IsNullOrEmpty(startDateString))
            {
                var dateString = startDateString.Replace((char)8206, ' ').Replace(" ", "");
                startDate = DateTime.Parse(dateString);
            }

            int month = startDate.Value.Month;
            int year = startDate.Value.Year;
            List<TeamCalendarTeamMemberModel> listAbsenceViewModel = new List<TeamCalendarTeamMemberModel>();
            List<TeamCalendarAbsenceDetailDTO> listAbscenceDTOs = new List<TeamCalendarAbsenceDetailDTO>();
            TeamCalendarController controller = new TeamCalendarController();
            int catchID = IdentityHelper.GetTeamID();
            if (teamID == 0)
            {
                listAbscenceDTOs = controller.LookUpTeamAbsences(catchID, startDate.Value);
            }

            else
            {
                listAbscenceDTOs = controller.LookUpTeamAbsences(teamID, startDate.Value);
            }

            foreach (var absence in listAbscenceDTOs)
            {

                listAbsenceViewModel.Add(new TeamCalendarTeamMemberModel
                {
                    TeamID = absence.TeamID,
                    TeamName = absence.TeamName,
                    EmployeeID = absence.EmployeeID,
                    EmployeeRole = absence.EmployeeRole,
                    FirstName = absence.FirstName,
                    LastName = absence.LastName,
                    Absences = absence.Absences,
                    Month = month,
                    Year = year
                });
            }
            return PartialView("_TeamAbsenceCalendar", listAbsenceViewModel);
        }

        /// <summary>
        /// Partial view for creating new absences
        /// </summary>
        /// <param name="employeeID">The currently logged in employee ID</param>
        /// <param name="day">Day for date creation grabbed by javascript on the front end</param>
        /// <param name="month">Month for date creation grabbed by javascript on the front end</param>
        /// <param name="year">Year for date creation grabbed by javascript on the front end</param>
        /// <returns></returns>
        public ActionResult _AbsenceCreate(int? employeeID, int? day, int? month, int? year)
        {
            int currentEmployeeID = IdentityHelper.GetEmployeeID();
            var employeeController = new EmployeeController();
            var currentEmployee = employeeController.LookupEmployee(currentEmployeeID);
            var absenceEmployee = employeeController.LookupEmployee(employeeID.Value);
            var offDayController = new OffDayTypeController();
            var absenceViewModel = new TeamCalendarAbsenceViewModel();
            if (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole))
            {
                if (currentEmployeeID != employeeID)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('You may only create absences for yourself.');</script>");
                }
            }
            if (day == null)
            {
                day = DateTime.Now.Day;
            }
            if (month == null)
            {
                month = DateTime.Now.Month;
            }
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            string stringDate = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
            DateTime absenceDate = DateTime.Parse(stringDate);

            if (absenceDate.Month < DateTime.Now.Month && absenceDate.Year <= DateTime.Now.Year)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('You may only create absences from this month forward.');</script>");
            }

            absenceViewModel.EmployeeID = employeeID.Value;
            absenceViewModel.AbsenceDate = absenceDate;
            absenceViewModel.EmployeeName = absenceEmployee.FullName;
            ViewBag.OffDaySelect = new SelectList(offDayController.LookupOffDayType(), "OffDayID", "Name");
            return PartialView("_AbsenceCreate", absenceViewModel);
        }

        /// <summary>
        /// Method to create new absence called by the parial view
        /// </summary>
        /// <param name="offDayID">The offday ID of the absence</param>
        /// <param name="absenceDate">The date of the absence</param>
        /// <param name="employeeID">The currently logged in employee ID</param>
        /// <param name="halfDay">Determines if the absence was a half day or not</param>
        /// <param name="hours">The amount of hours that the employee used to take the time off</param>
        /// <param name="notes">Any notes that the creator of the absence wishes to enter</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAbsence(int offDayID, DateTime absenceDate, int employeeID, string halfDay, decimal? hours, string notes)
        {
            try
            {

                int CurrentEmployeeID = IdentityHelper.GetEmployeeID();
                var absenceController = new TeamCalendarController();
                var employeeController = new EmployeeController();
                var offDayController = new OffDayTypeController();
                var currentOffDay = offDayController.LookupOffDayType(offDayID);
                var Currentemployee = employeeController.LookupEmployee(employeeID);
                var absenceDTO = new AbsencesDTO();
                absenceDTO.OffDayID = offDayID;
                absenceDTO.AbsenceDate = absenceDate;
                absenceDTO.EmployeeID = employeeID;
                if (halfDay == "")
                {
                    absenceDTO.HalfDay = null;
                }
                else
                {
                    absenceDTO.HalfDay = halfDay;
                }
                absenceDTO.Hours = hours;
                if (notes == "")
                {
                    absenceDTO.Notes = null;
                }
                else
                {
                    absenceDTO.Notes = notes;
                }
                absenceController.CreateAbsence(absenceDTO, CurrentEmployeeID);
                TempData["message"] = "New" + " " + "'" + currentOffDay.Name + "'" + " abscence made on " + absenceDate.ToShortDateString() + " for" + " " + Currentemployee.FullName + ".";

                return new EmptyResult();
            }

            catch (AggregateException ex)
            {
                string errorMessage = "";
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    errorMessage += exception.Message;
                }
                //return PartialView("_AbsenceCreate", errorViewModel);
                TempData["errorMessage"] = errorMessage;
                return new EmptyResult();
            }

        }

        /// <summary>
        /// Partial view for absence editing
        /// </summary>
        /// <param name="absenceID">The current absence ID of the absence that is being edited</param>
        /// <returns></returns>
        public ActionResult _AbsenceEdit(int absenceID)
        {
            int currentEmployeeID = IdentityHelper.GetEmployeeID();
            var employeeController = new EmployeeController();
            var currentEmployee = employeeController.LookupEmployee(currentEmployeeID);
            var teamCalendarController = new TeamCalendarController();
            var offDayController = new OffDayTypeController();
            var currentAbsence = teamCalendarController.LookUpAbsence(absenceID);
            var absenceEmployee = employeeController.LookupEmployee(currentAbsence.EmployeeID);
            var absenceViewModel = new TeamCalendarAbsenceViewModel();
            ViewBag.OffDaySelect = new SelectList(offDayController.LookupOffDayType(), "OffDayID", "Name");
            if (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole))
            {
                if (currentEmployeeID != currentAbsence.EmployeeID)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('You may only edit absences for yourself.');</script>");
                }
            }

            if (currentAbsence.AbsenceDate.Month < DateTime.Now.Month && currentAbsence.AbsenceDate.Year <= DateTime.Now.Year)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('You may only edit absences from this month forward.');</script>");
            }

            absenceViewModel.AbsenceID = currentAbsence.AbsenceID;
            absenceViewModel.AbsenceDate = currentAbsence.AbsenceDate;
            absenceViewModel.EmployeeID = currentAbsence.EmployeeID;
            absenceViewModel.HalfDay = currentAbsence.HalfDay;
            absenceViewModel.Hours = currentAbsence.Hours;
            absenceViewModel.Notes = currentAbsence.Notes;
            absenceViewModel.OffDayID = currentAbsence.OffDayID;
            absenceViewModel.AbsenceDate = currentAbsence.AbsenceDate;
            absenceViewModel.EmployeeName = absenceEmployee.FullName;

            return PartialView("_AbsenceEdit", absenceViewModel);
        }

        /// <summary>
        /// Method called by the edit partial view to submit the edit of the absence
        /// </summary>
        /// <param name="absenceID">Current absence ID of the edited absence</param>
        /// <param name="absenceDate">Current absence date of the edited absence</param>
        /// <param name="offDayID">Current off day ID of the edited absence</param>
        /// <param name="employeeID">Employee ID of the currently logged in user</param>
        /// <param name="halfDay">Determines if the absence is a half day</param>
        /// <param name="hours">The amount of hours the employee used to take the time off</param>
        /// <param name="notes">Any notes that the creator of the absence wihses to enter</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditAbsence(int absenceID, DateTime absenceDate, int offDayID, int employeeID, string halfDay, decimal? hours, string notes)
        {
            try
            {
                int currentEmployeeID = IdentityHelper.GetEmployeeID();
                var currentEmployeeRole = IdentityHelper.GetRoleTitle();
                var absenceController = new TeamCalendarController();
                var employeeController = new EmployeeController();
                var currentAbsence = new AbsencesDTO();
                currentAbsence.AbsenceID = absenceID;
                currentAbsence.OffDayID = offDayID;
                currentAbsence.EmployeeID = employeeID;
                currentAbsence.AbsenceDate = absenceDate;
                if (halfDay == "")
                {
                    currentAbsence.HalfDay = null;
                }
                else
                {
                    currentAbsence.HalfDay = halfDay;
                }
                currentAbsence.Hours = hours;
                if (notes == "")
                {
                    currentAbsence.Notes = null;
                }
                else
                {
                    currentAbsence.Notes = notes;
                }
                absenceController.EditAbsence(currentAbsence, currentEmployeeID);
                var currentEmployee = employeeController.LookupEmployee(employeeID);
                var updatedAbsence = absenceController.LookUpAbsence(absenceID);
                TempData["message"] = currentEmployee.FullName + "'s" + " absence on " + updatedAbsence.AbsenceDate.ToShortDateString() + " was updated.";
                return new EmptyResult();
            }

            catch (AggregateException ex)
            {
                string errorMessage = "";
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    errorMessage += exception.Message;
                }

                TempData["errorMessage"] = errorMessage;
                return new EmptyResult();
            }

        }

        /// <summary>
        /// Method to deactivate and absence called by the edit partial view
        /// </summary>
        /// <param name="absenceID">The absence ID of the current absence</param>
        /// <returns></returns>
        public ActionResult DeactivateAbsence(int absenceID)
        {
            try
            {
                int currentEmployeeID = IdentityHelper.GetEmployeeID();
                var absenceController = new TeamCalendarController();
                var employeeController = new EmployeeController();
                var offDayController = new OffDayTypeController();
                var currentAbsence = new AbsencesDTO();
                currentAbsence.AbsenceID = absenceID;
                absenceController.DeactivateAbsence(currentAbsence, currentEmployeeID);
                var deactivatedAbsence = absenceController.LookUpAbsence(absenceID);
                var employeeForAbsence = employeeController.LookupEmployee(deactivatedAbsence.EmployeeID);
                var offDayType = offDayController.LookupOffDayType(deactivatedAbsence.OffDayID);
                TempData["message"] = "The " + "'" + offDayType.Name + "'" + " absence on " + deactivatedAbsence.AbsenceDate.ToShortDateString() + " for " + employeeForAbsence.FullName + " was deactivated.";
                return new EmptyResult();
            }

            catch (AggregateException ex)
            {
                string errorMessage = "";
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    errorMessage += exception.Message;
                }
                TempData["errorMessage"] = errorMessage;
                return new EmptyResult();
            }

        }
    }
}