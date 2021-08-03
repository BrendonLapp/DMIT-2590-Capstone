using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs;
using Capstone.Web.Models.Employee;
using Capstone.Web.Admin;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    public class EmployeeViewController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var teamController = new TeamController();
            var employeeController = new EmployeeController();
            var employeeViewList = new List<ListEmployeeViewModel>();
            List<EmployeeWithForignKeyNamesDTO> employeeList = employeeController.LookupEmployeeByTeam(IdentityHelper.GetTeamID());
            foreach (var item in employeeList)
            {
                var employeeViewItem = new ListEmployeeViewModel();

                employeeViewItem.UserID = item.UserID;
                employeeViewItem.EmployeeID = item.EmployeeID;
                employeeViewItem.FirstName = item.FirstName;
                employeeViewItem.LastName = item.LastName;
                employeeViewItem.ActivationDate = item.ActivationDate;
                employeeViewItem.AlternatePhoneNumber = item.AlternatePhoneNumber;
                employeeViewItem.BirthDate = item.BirthDate;
                employeeViewItem.ComputerNumber = item.ComputerNumber;
                employeeViewItem.EmergencyContactName1 = item.EmergencyContactName1;
                employeeViewItem.EmergencyContactName2 = item.EmergencyContactName2;
                employeeViewItem.EmergencyContactPhoneNumber1 = item.EmergencyContactPhoneNumber1;
                employeeViewItem.EmergencyContactPhoneNumber2 = item.EmergencyContactPhoneNumber2;
                employeeViewItem.PhoneNumber = item.PhoneNumber;
                employeeViewItem.PositionID = item.PositionID;
                employeeViewItem.RoleID = item.RoleID;
                employeeViewItem.StationNumber = item.StationNumber;
                employeeViewItem.TeamID = item.TeamID;
                employeeViewItem.RoleTitle = item.RoleTitle;
                employeeViewItem.PositionTitle = item.PositionTitle;
                employeeViewItem.TeamName = item.TeamName;
                employeeViewItem.CompanyPhoneNumber = item.CompanyPhoneNumber;

                employeeViewList.Add(employeeViewItem);
            }

            ViewBag.TeamID = new SelectList(teamController.LookupTeam(), "TeamID", "TeamName", IdentityHelper.GetTeamID());
            return View(employeeViewList);

        }

        //GET: EmployeeView
        public ActionResult GetEmployeesByTeam(int teamID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new EmployeeController();
            List<EmployeeWithForignKeyNamesDTO> employeeList = controller.LookupEmployeeByTeam(teamID);
            var employeeViewList = new List<ListEmployeeViewModel>();

            foreach (var item in employeeList)
            {
                var employeeViewItem = new ListEmployeeViewModel();

                employeeViewItem.UserID = item.UserID;
                employeeViewItem.EmployeeID = item.EmployeeID;
                employeeViewItem.FirstName = item.FirstName;
                employeeViewItem.LastName = item.LastName;
                employeeViewItem.ActivationDate = item.ActivationDate;
                employeeViewItem.AlternatePhoneNumber = item.AlternatePhoneNumber;
                employeeViewItem.BirthDate = item.BirthDate;
                employeeViewItem.ComputerNumber = item.ComputerNumber;
                employeeViewItem.EmergencyContactName1 = item.EmergencyContactName1;
                employeeViewItem.EmergencyContactName2 = item.EmergencyContactName2;
                employeeViewItem.EmergencyContactPhoneNumber1 = item.EmergencyContactPhoneNumber1;
                employeeViewItem.EmergencyContactPhoneNumber2 = item.EmergencyContactPhoneNumber2;
                employeeViewItem.PhoneNumber = item.PhoneNumber;
                employeeViewItem.PositionID = item.PositionID;
                employeeViewItem.RoleID = item.RoleID;
                employeeViewItem.StationNumber = item.StationNumber;
                employeeViewItem.TeamID = item.TeamID;
                employeeViewItem.RoleTitle = item.RoleTitle;
                employeeViewItem.PositionTitle = item.PositionTitle;
                employeeViewItem.TeamName = item.TeamName;
                employeeViewItem.CompanyPhoneNumber = item.CompanyPhoneNumber;

                employeeViewList.Add(employeeViewItem);
            }

            return PartialView("_EmployeeView", employeeViewList);
        }

        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new EmployeeController();

            ViewBag.ScheduleTypeID = new SelectList(controller.LookupScheduleType(), "Key", "Value");
            ViewBag.PositionID = new SelectList(controller.LookupPosition(), "Key", "Value");
            ViewBag.RoleID = new SelectList(controller.LookupRole(IdentityHelper.GetRoleID()), "Key", "Value");
            ViewBag.TeamID = new SelectList(controller.LookupTeam(), "Key", "Value");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListEmployeeViewModel employee)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new EmployeeController();
            ViewBag.ScheduleTypeID = new SelectList(controller.LookupScheduleType(), "Key", "Value", employee.ScheduleTypeID);
            ViewBag.PositionID = new SelectList(controller.LookupPosition(), "Key", "Value", employee.PositionID);
            ViewBag.RoleID = new SelectList(controller.LookupRole(IdentityHelper.GetRoleID()), "Key", "Value", employee.RoleID);
            ViewBag.TeamID = new SelectList(controller.LookupTeam(), "Key", "Value", employee.TeamID);
            try
            {
                ModelState.Remove("EmployeeID");

                if (ModelState.IsValid)
                {
                    //This creates a new employeee entry with the correct information
                    var newEmployee = new EmployeeDTO();
                    newEmployee.UserID = employee.UserID;
                    newEmployee.EmployeeID = employee.EmployeeID;
                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.ActivationDate = employee.ActivationDate;
                    newEmployee.AlternatePhoneNumber = employee.AlternatePhoneNumber;
                    newEmployee.BirthDate = employee.BirthDate;
                    newEmployee.CompanyPhoneNumber = employee.CompanyPhoneNumber;
                    newEmployee.ComputerNumber = employee.ComputerNumber;
                    newEmployee.EmergencyContactName1 = employee.EmergencyContactName1;
                    newEmployee.EmergencyContactName2 = employee.EmergencyContactName2;
                    newEmployee.EmergencyContactPhoneNumber1 = employee.EmergencyContactPhoneNumber1;
                    newEmployee.EmergencyContactPhoneNumber2 = employee.EmergencyContactPhoneNumber2;
                    newEmployee.PhoneNumber = employee.PhoneNumber;
                    newEmployee.PositionID = employee.PositionID;
                    newEmployee.RoleID = employee.RoleID;
                    newEmployee.StationNumber = employee.StationNumber;
                    newEmployee.TeamID = employee.TeamID;
                    newEmployee.ScheduleTypeID = employee.ScheduleTypeID;
                    controller.CreateEmployee(newEmployee, IdentityHelper.GetEmployeeID());
                    TempData["message"] = newEmployee.FirstName + " " + newEmployee.LastName + " " + "was successfully created.";
                    return RedirectToAction("Index");
                }


                return View(employee);
            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(employee);
        }

        /// <summary>
        /// GET method for EmployeeView/Edit.cshtml Page
        /// </summary>
        /// <param name="employeeID">Key Value for the TB_Capstone_EMPLOYEE PAGE</param>
        /// <returns></returns>
        public ActionResult Edit(int? employeeID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new EmployeeController();
            if (employeeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeWithForignKeyNamesDTO employee = controller.LookupEmployee(employeeID.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var employeeView = new ListEmployeeViewModel();

            employeeView.EmployeeID = employee.EmployeeID;
            employeeView.UserID = employee.UserID;
            employeeView.EmployeeID = employee.EmployeeID;
            employeeView.FirstName = employee.FirstName;
            employeeView.LastName = employee.LastName;
            employeeView.ActivationDate = employee.ActivationDate;
            employeeView.AlternatePhoneNumber = employee.AlternatePhoneNumber;
            employeeView.BirthDate = employee.BirthDate;
            employeeView.CompanyPhoneNumber = employee.CompanyPhoneNumber;
            employeeView.ComputerNumber = employee.ComputerNumber;
            employeeView.EmergencyContactName1 = employee.EmergencyContactName1;
            employeeView.EmergencyContactName2 = employee.EmergencyContactName2;
            employeeView.EmergencyContactPhoneNumber1 = employee.EmergencyContactPhoneNumber1;
            employeeView.EmergencyContactPhoneNumber2 = employee.EmergencyContactPhoneNumber2;
            employeeView.PhoneNumber = employee.PhoneNumber;
            employeeView.PositionID = employee.PositionID;
            employeeView.RoleID = employee.RoleID;
            employeeView.StationNumber = employee.StationNumber;
            employeeView.TeamID = employee.TeamID;

            ViewBag.ScheduleTypeID = new SelectList(controller.LookupScheduleType(), "Key", "Value", employee.ScheduleTypeID);
            ViewBag.PositionID = new SelectList(controller.LookupPosition(), "Key", "Value", employee.PositionID);
            ViewBag.RoleID = new SelectList(controller.LookupRole(IdentityHelper.GetRoleID()), "Key", "Value", employee.RoleID);
            return View(employeeView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListEmployeeViewModel employeeView)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new EmployeeController();
            ViewBag.PositionID = new SelectList(controller.LookupPosition(), "Key", "Value", employeeView.PositionID);
            ViewBag.ScheduleTypeID = new SelectList(controller.LookupScheduleType(), "Key", "Value", employeeView.ScheduleTypeID);
            ViewBag.RoleID = new SelectList(controller.LookupRole(IdentityHelper.GetRoleID()), "Key", "Value", employeeView.RoleID);
            try
            {
                if (ModelState.IsValid)
                {
                    var editedEmployee = new EmployeeDTO();

                    editedEmployee.EmployeeID = employeeView.EmployeeID;
                    //editedEmployee.TeamID = employeeView.TeamID;
                    editedEmployee.RoleID = employeeView.RoleID;
                    editedEmployee.UserID = employeeView.UserID;
                    editedEmployee.PositionID = employeeView.PositionID;
                    editedEmployee.FirstName = employeeView.FirstName;
                    editedEmployee.LastName = employeeView.LastName;
                    editedEmployee.PhoneNumber = employeeView.PhoneNumber;
                    editedEmployee.AlternatePhoneNumber = employeeView.AlternatePhoneNumber;
                    editedEmployee.StationNumber = employeeView.StationNumber;
                    editedEmployee.ComputerNumber = employeeView.ComputerNumber;
                    editedEmployee.CompanyPhoneNumber = employeeView.CompanyPhoneNumber;
                    editedEmployee.BirthDate = employeeView.BirthDate;
                    editedEmployee.ActivationDate = employeeView.ActivationDate;
                    editedEmployee.DeactivationDate = employeeView.DeactivationDate;
                    editedEmployee.EmergencyContactName1 = employeeView.EmergencyContactName1;
                    editedEmployee.EmergencyContactPhoneNumber1 = employeeView.EmergencyContactPhoneNumber1;
                    editedEmployee.EmergencyContactName2 = employeeView.EmergencyContactName2;
                    editedEmployee.EmergencyContactPhoneNumber2 = employeeView.EmergencyContactPhoneNumber2;
                    editedEmployee.ScheduleTypeID = employeeView.ScheduleTypeID;
                    if (editedEmployee.DeactivationDate != null)
                    {
                        controller.DeactivateEmployee(editedEmployee.EmployeeID, IdentityHelper.GetEmployeeID(), editedEmployee.DeactivationDate.Value);
                    }

                    controller.EditEmployee(editedEmployee, IdentityHelper.GetEmployeeID());
                    TempData["message"] = editedEmployee.FirstName + " " + editedEmployee.LastName + " " + "was successfully updated.";
                    return RedirectToAction("Entitlements", "EmployeeView", new { employeeID = editedEmployee.EmployeeID });
                }

            }

            catch (AggregateException ex)
            {
                foreach (Exception exception in ex.InnerExceptions)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(employeeView);
        }

        public ActionResult Details(int? employeeID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (employeeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var controller = new EmployeeController();
            EmployeeWithForignKeyNamesDTO employee = controller.LookupEmployee(employeeID.Value);
            var employeeView = new ListEmployeeViewModel
            {
                EmployeeID = employee.EmployeeID,
                UserID = employee.UserID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ActivationDate = employee.ActivationDate,
                AlternatePhoneNumber = employee.AlternatePhoneNumber,
                BirthDate = employee.BirthDate,
                CompanyPhoneNumber = employee.CompanyPhoneNumber,
                ComputerNumber = employee.ComputerNumber,
                EmergencyContactName1 = employee.EmergencyContactName1,
                EmergencyContactName2 = employee.EmergencyContactName2,
                EmergencyContactPhoneNumber1 = employee.EmergencyContactPhoneNumber1,
                EmergencyContactPhoneNumber2 = employee.EmergencyContactPhoneNumber2,
                PhoneNumber = employee.PhoneNumber,
                PositionID = employee.PositionID,
                RoleID = employee.RoleID,
                StationNumber = employee.StationNumber,
                TeamID = employee.TeamID,
                TeamName = employee.TeamName,
                RoleTitle = employee.RoleTitle,
                ScheduleTypeName = employee.ScheduleTypeName,
                PositionTitle = employee.PositionTitle
            };

            if (employeeView == null)
            {
                return HttpNotFound();
            }

            return View(employeeView);
        }

        public ActionResult Deactivate(int? employeeID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (employeeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var controller = new EmployeeController();
            EmployeeWithForignKeyNamesDTO employee = controller.LookupEmployee(employeeID.Value);
            var employeeView = new ListEmployeeViewModel();

            employeeView.EmployeeID = employee.EmployeeID;
            employeeView.UserID = employee.UserID;
            employeeView.EmployeeID = employee.EmployeeID;
            employeeView.FirstName = employee.FirstName;
            employeeView.LastName = employee.LastName;
            employeeView.ActivationDate = employee.ActivationDate;
            employeeView.AlternatePhoneNumber = employee.AlternatePhoneNumber;
            employeeView.BirthDate = employee.BirthDate;
            employeeView.CompanyPhoneNumber = employee.CompanyPhoneNumber;
            employeeView.ComputerNumber = employee.ComputerNumber;
            employeeView.EmergencyContactName1 = employee.EmergencyContactName1;
            employeeView.EmergencyContactName2 = employee.EmergencyContactName2;
            employeeView.EmergencyContactPhoneNumber1 = employee.EmergencyContactPhoneNumber1;
            employeeView.EmergencyContactPhoneNumber2 = employee.EmergencyContactPhoneNumber2;
            employeeView.PhoneNumber = employee.PhoneNumber;
            employeeView.PositionID = employee.PositionID;
            employeeView.RoleID = employee.RoleID;
            employeeView.StationNumber = employee.StationNumber;
            employeeView.TeamID = employee.TeamID;
            employeeView.TeamName = employee.TeamName;
            employeeView.RoleTitle = employee.RoleTitle;
            employeeView.ScheduleTypeName = employee.ScheduleTypeName;
            employeeView.PositionTitle = employee.PositionTitle;


            if (employeeView == null)
            {
                return HttpNotFound();
            }

            return View(employeeView);
        }

        /// <summary>
        /// POST method for Delete.cshtml, deactivates the selected overtime type
        /// </summary>
        /// <param name="overtimeTypeView">View model for the selected Overtime Type</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(ListEmployeeViewModel employeeView)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            else
            {
                try
                {
                    var controller = new EmployeeController();
                    EmployeeWithForignKeyNamesDTO currentEmployee = controller.LookupEmployee(employeeView.EmployeeID);
                    TempData["message"] = $"\"{currentEmployee.FullName}\" was deactivated successfully";
                    controller.DeactivateEmployee(employeeView.EmployeeID, IdentityHelper.GetEmployeeID(), DateTime.Now);
                    return RedirectToAction("Index");
                }
                catch (AggregateException ex)
                {
                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
                return View(employeeView);

            }
        }

        /// <summary>
        /// GET method for EmployeeView/Entitlements.cshtml
        /// </summary>
        /// <param name="employeeID">passed in employeeID for getting items</param>
        /// <returns></returns>
        public ActionResult Entitlements(int? employeeID)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            if (employeeID == null)
            {
                return RedirectToAction("Index");
            }
            var controller = new EmployeeController();
            EmployeeWithForignKeyNamesDTO employee = controller.LookupEmployee(employeeID.Value);
            List<EntiltedTimeOffDTO> entitlements = controller.LookupEntitledTimeOff(employeeID.Value);
            var entitltementsView = new EmployeeEntitledTimeOffViewModel();
            if (entitlements != null)
            {
                entitltementsView.EmployeeName = employee.FullName;
                entitltementsView.EmployeeID = employee.EmployeeID;
                foreach (var item in entitlements)
                {
                    var entitledViewItem = new EntitledTimeOffViewModel
                    {
                        HoursAccumulated = decimal.Round(item.HoursAccumulated / employee.ScheduleHours, 2),
                        OffDayTypeID = item.OffDayID,
                        OffDayTypeName = item.OffDayTypeDescription
                    };
                    entitltementsView.EmployeeEntitlements.Add(entitledViewItem);
                }

                return View(entitltementsView);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entitlements(EmployeeEntitledTimeOffViewModel entitlmentsVM)
        {
            if (!User.Identity.IsAuthenticated || (!User.IsInRole(SecurityRoles.SupervisorRole) && !User.IsInRole(SecurityRoles.TeamAdminRole)))
            {
                Response.StatusCode = 403;
                return new EmptyResult();
            }
            var controller = new EmployeeController();
            var entitlements = new List<EntiltedTimeOffDTO>();
            EmployeeWithForignKeyNamesDTO employeeInfo = controller.LookupEmployee(entitlmentsVM.EmployeeID);

            try
            {
                var errorList = new List<Exception>();
                if (ModelState.IsValid)
                {
                    foreach (var item in entitlmentsVM.EmployeeEntitlements)
                    {
                        var entitlementItem = new EntiltedTimeOffDTO
                        {
                            EmployeeID = entitlmentsVM.EmployeeID,
                            HoursAccumulated = decimal.Round(item.HoursAccumulated * employeeInfo.ScheduleHours, 2),
                            OffDayID = item.OffDayTypeID,
                            OffDayTypeDescription = item.OffDayTypeName
                        };
                        entitlements.Add(entitlementItem);
                    }
                    controller.EditEntitledTimeOff(entitlements, IdentityHelper.GetEmployeeID());
                    TempData["message"] = employeeInfo.FirstName + " " + employeeInfo.LastName + "'s " + "entitlements were successfully updated.";
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

            return View(entitlmentsVM);
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.ExceptionHandled)
            {
                return;
            }

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandling/Index.cshtml"
            };

            filterContext.ExceptionHandled = true;
        }
    }
}