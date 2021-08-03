using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;
using Capstone.Web.Models;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs;
using Capstone.Web.Models.PersonalSchedule;
using Capstone.BLL.Security;
using System.Globalization;
using Capstone.BLL.DTOs.OvertimeRequestDTOs;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// View Controller for views in the Views/PersonalScheduleView folder
    /// </summary>
    public class PersonalScheduleViewController : Controller
    {
        // Initialize all BLL controllers used in this class
        private PersonalScheduleController personalScheduleController = new PersonalScheduleController();
        private SecurityController securityController = new SecurityController();
        private OvertimeRequestController overtimeRequestController = new OvertimeRequestController();
        private EmployeeController employeeController = new EmployeeController();
        private TeamController teamController = new TeamController();
        private OvertimeTypeController overtimeTypeController = new OvertimeTypeController();
        /// <summary>
        /// GET method for the Personal Schedule Index.cshtml listing
        /// </summary>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.EmployeeRole))
            {
                return RedirectToAction("Home");
            }
            else
            {

                // Attach values to elements and variables on the page.
                ViewBag.TeamList = new SelectList(teamController.LookupTeam(), "TeamID", "TeamName");
                ViewBag.OvertimeType = new SelectList(overtimeTypeController.LookupOvertimeType(), "OvertimeTypeID", "Name");
                ViewBag.LoggedEmployeeID = securityController.LookupSecurityInformation().EmployeeID;
                ViewBag.LoggedInRole = securityController.LookupSecurityInformation().RoleID;
                ViewBag.LoggedInTeam = securityController.LookupSecurityInformation().TeamID;

                return View();
            }

        }
        /// <summary>
        /// POST method for Creating Events. Called by an AJAX call on the Personal Schedule Index page.
        /// </summary>
        /// <param name="date">Date of the event created</param>
        /// <param name="start">Start time of the event created (HH:MM in a 24H format)</param>
        /// <param name="end">End time of the event created (HH:MM in a 24H format)</param>
        /// <param name="projectID">ID of the TB_Capstone_PROJECT entry that the event is created for</param>
        /// <param name="notes">Notes for the event created</param>
        /// <param name="employeeID">ID of the employee that is in the event</param>
        [HttpPost]
        public ActionResult CreateEvent(string date, string start, string end, int projectID, string notes, int employeeID)
        {
            bool success;
            try
            {

                //ModelState.AddModelError("", "Error");
                if (ModelState.IsValid)
                {
                    var dateEvent = DateTime.Parse(date);
                    var startEvent = dateEvent.Add(TimeSpan.Parse(start));
                    var endEvent = dateEvent.Add(TimeSpan.Parse(end));

                    var projectDetailDTO = new ProjectDetailDTO
                    {
                        EmployeeID = employeeID,
                        StartTime = startEvent,
                        EndTime = endEvent,
                        ProjectID = projectID,
                        Notes = notes
                    };
                    success = personalScheduleController.CreateTimesheetEntry(projectDetailDTO, securityController.LookupSecurityInformation().EmployeeID);

                    if (success)
                        return Content("Event created successfully");
                    else
                        return Content("Event creation was unsuccessful");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error! {ex.Message}");
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult CreateOvertimeRequest(string date, string start, string end, int projectID, int overtimeTypeID, string notes, int employeeID)
        {
            bool success;
            try
            {
                var dateEvent = DateTime.Parse(date);
                var startEvent = dateEvent.Add(TimeSpan.Parse(start));
                var endEvent = dateEvent.Add(TimeSpan.Parse(end));

                var overtimeRequestDTO = new TeamOvertimeRequestDTO()
                {
                    EmployeeID = employeeID,
                    StartTime = startEvent,
                    EndTime = endEvent,
                    ProjectID = projectID,
                    SubmissionNotes = notes,
                    OvertimeTypeID = overtimeTypeID,
                    Approved = "P",
                    SubmissionDate = DateTime.Now
                };

                success = overtimeRequestController.CreateOvertimeRequest(overtimeRequestDTO, securityController.LookupSecurityInformation().EmployeeID);

                if (success)
                    return Content("Overtime Request created successfully");
                else
                    return Content("Overtime Request was unsuccessful");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error! {ex.Message}");
            }
            return RedirectToAction("Index");

        }

        [HttpPost]

        [ValidateInput(false)]
        public ActionResult EditEvent(int eventId, string date, string start, string end, int projectID, string notes, int employeeID)
        {
            bool success;
            try
            {
                // if IE 11, remove 8206 ASCII character
                var dateString = date.Replace((char)8206, ' ').Replace(" ", "");

                var dateEvent = DateTime.ParseExact(dateString, "M/d/yyyy", CultureInfo.InvariantCulture);

                var startEvent = dateEvent.Add(TimeSpan.Parse(start));
                var endEvent = dateEvent.Add(TimeSpan.Parse(end));

                var projectDetailDTO = new ProjectDetailDTO
                {
                    ProjectDetailID = eventId,
                    EmployeeID = employeeID,
                    StartTime = startEvent,
                    EndTime = endEvent,
                    ProjectID = projectID,
                    Notes = notes
                };
                success = personalScheduleController.EditTimesheetEntry(projectDetailDTO, securityController.LookupSecurityInformation().EmployeeID);
                if (success)
                    return Content("Event edited successfully");
                else
                    return Content("Edit was unsuccessful");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error! {ex.Message}");
            }
            return RedirectToAction("Index");

        }

        [HttpPost]

        [ValidateInput(false)]
        public ActionResult DeleteEvent(int eventId)
        {
            bool success;
            try
            {
                success = personalScheduleController.DeleteTimesheetEntry(eventId, securityController.LookupSecurityInformation().EmployeeID);
                if (success)
                    return Content("Event deleted successfully");
                else
                    return Content("Event deletion was unsuccessful");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error! {ex.Message}");
            }
            return RedirectToAction("Index");

        }


        // Check that it is not referenced anymore and delete
        public ActionResult GetCalendarData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            int loggedInEmployeeID = securityController.LookupSecurityInformation().EmployeeID;
            try
            {
                List<EventDTO> events = new List<EventDTO>();
                foreach (ProjectDetailDTO detail in personalScheduleController.LookupProjectDetailsByEmployee(loggedInEmployeeID))
                {
                    List<KeyValueDTO> projectNames = personalScheduleController.LookupProjectsNames();
                    List<KeyValueDTO> projectColors = personalScheduleController.LookupProjectColors();
                    events.Add(new EventDTO
                    {
                        ID = detail.ProjectDetailID.ToString(),
                        Title = projectNames.Find(x => x.Key == detail.ProjectID).Value,
                        Start = detail.StartTime,
                        End = detail.EndTime,
                        BackgroundColor = projectColors.Find(x => x.Key == detail.ProjectID).Value,
                        Notes = detail.Notes
                    });
                }
                result = Json(events, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }
        public ActionResult PopulateProjectDropdown(int employeeID)
        {
            List<KeyValueDTO> projects = new List<KeyValueDTO>();
            foreach (int projectID in personalScheduleController.LookupAllocatedProjectsByEmployee(employeeID))
            {
                projects.Add(new KeyValueDTO
                {
                    Key = projectID,
                    Value = personalScheduleController.LookupProjectsNames().Find(item => item.Key == projectID).Value
                });
            }

            return Json(projects);
        }

        public ActionResult PopulateEmployeeDropdown(int teamID)
        {
            List<KeyValueDTO> employees = new List<KeyValueDTO>();
            foreach (var employee in employeeController.LookupEmployeeByTeam(teamID))
            {
                employees.Add(new KeyValueDTO
                {
                    Key = employee.EmployeeID,
                    Value = employee.FullName
                });
            }
            return Json(employees);
        }
        public ActionResult GetCalendarDataForEmployee(int employeeID)
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            try
            {
                List<EventDTO> events = new List<EventDTO>();
                foreach (ProjectDetailDTO detail in personalScheduleController.LookupProjectDetailsByEmployee(employeeID))
                {
                    List<KeyValueDTO> projectNames = personalScheduleController.LookupProjectsNames();
                    List<KeyValueDTO> projectColors = personalScheduleController.LookupProjectColors();
                    events.Add(new EventDTO
                    {
                        ID = detail.ProjectDetailID.ToString(),
                        Title = projectNames.Find(x => x.Key == detail.ProjectID).Value,
                        Start = detail.StartTime,
                        End = detail.EndTime,
                        BackgroundColor = projectColors.Find(x => x.Key == detail.ProjectID).Value,
                        Notes = detail.Notes
                    });
                }
                result = Json(events);


            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }


    }
}