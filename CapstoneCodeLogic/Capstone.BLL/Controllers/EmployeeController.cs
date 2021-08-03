using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// Employee CRUD Controller
    /// </summary>
    /// <remarks>
    public class EmployeeController
    {
        /// <summary>
        /// Gets a listing of all active employees
        /// </summary>
        /// <returns>A listing of all employees that either don't have a deactivation date, or have not reached their deactivation date</returns>
        public List<EmployeeWithForignKeyNamesDTO> LookupEmployee()
        {
            using (var context = new CapstoneContext())
            {
                var employeeList =
                    from person in context.TB_Capstone_EMPLOYEE
                    let history =
                        (from team in person.TB_Capstone_TEAM_HISTORY
                         where (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                               team.ActivationDate <= DateTime.Now
                         select new
                         {
                             team.TeamID,
                             team.TB_Capstone_TEAM.TeamName
                         }).FirstOrDefault()
                    where (person.DeactivationDate == null || person.DeactivationDate > DateTime.Now)
                    select new EmployeeWithForignKeyNamesDTO()
                    {
                        EmployeeID = person.EmployeeID,
                        PositionID = person.PositionID,
                        PositionTitle = person.TB_Capstone_POSITION.PositionTitle,
                        UserID = person.UserID,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PhoneNumber = person.PhoneNumber,
                        AlternatePhoneNumber = person.AlternatePhoneNumber,
                        StationNumber = person.StationNumber,
                        ComputerNumber = person.ComputerNumber,
                        CompanyPhoneNumber = person.CompanyPhoneNumber,
                        BirthDate = person.BirthDate,
                        ActivationDate = person.ActivationDate,
                        DeactivationDate = person.DeactivationDate,
                        EmergencyContactName1 = person.EmergencyContactName1,
                        EmergencyContactPhoneNumber1 = person.EmergencyContactPhoneNumber1,
                        EmergencyContactName2 = person.EmergencyContactName2,
                        EmergencyContactPhoneNumber2 = person.EmergencyContactPhoneNumber2,
                        TeamID = history.TeamID,
                        TeamName = history.TeamName
                    };
                var emplList = employeeList.ToList();
                return emplList;
            }
        }

        /// <summary>
        /// Gets a single Employee
        /// </summary>
        /// <param name="employeeID">TB_Capstone_EMPLOYEE Primary Key value</param>
        /// <returns>Employee that matches the entered primary key</returns>
        public EmployeeWithForignKeyNamesDTO LookupEmployee(int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var employee =
                    (from person in context.TB_Capstone_EMPLOYEE
                     let history =
                         (from team in person.TB_Capstone_TEAM_HISTORY
                          where (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                                team.ActivationDate <= DateTime.Now
                          select new
                          {
                              team.TB_Capstone_TEAM.TeamName,
                              team.TeamID,
                              team.RoleID,
                              team.TB_Capstone_ROLE.RoleTitle
                          }).FirstOrDefault()
                     let schedule =
                         (from time in person.TB_Capstone_SCHEDULE_TYPE_DETAILS
                          where (time.DeactivationDate == null || time.DeactivationDate > DateTime.Now) &&
                                time.ActivationDate <= DateTime.Now
                          select new
                          {
                              time.ScheduleTypeID,
                              time.TB_Capstone_SCHEDULE_TYPE.Name,
                              time.TB_Capstone_SCHEDULE_TYPE.HoursPerDay
                          }).FirstOrDefault()
                     where person.EmployeeID == employeeID
                     select new EmployeeWithForignKeyNamesDTO()
                     {
                         EmployeeID = person.EmployeeID,
                         TeamID = history.TeamID,
                         PositionID = person.PositionID,
                         PositionTitle = person.TB_Capstone_POSITION.PositionTitle,
                         UserID = person.UserID,
                         RoleID = history.RoleID,
                         RoleTitle = history.RoleTitle,
                         FirstName = person.FirstName,
                         LastName = person.LastName,
                         PhoneNumber = person.PhoneNumber,
                         AlternatePhoneNumber = person.AlternatePhoneNumber,
                         StationNumber = person.StationNumber,
                         ComputerNumber = person.ComputerNumber,
                         CompanyPhoneNumber = person.CompanyPhoneNumber,
                         BirthDate = person.BirthDate,
                         ScheduleTypeID = schedule.ScheduleTypeID,
                         ActivationDate = person.ActivationDate,
                         DeactivationDate = person.DeactivationDate,
                         EmergencyContactName1 = person.EmergencyContactName1,
                         EmergencyContactPhoneNumber1 = person.EmergencyContactPhoneNumber1,
                         EmergencyContactName2 = person.EmergencyContactName2,
                         EmergencyContactPhoneNumber2 = person.EmergencyContactPhoneNumber2,
                         TeamName = history.TeamName,
                         ScheduleTypeName = schedule.Name,
                         ScheduleHours = schedule.HoursPerDay
                     }).FirstOrDefault();

                return employee;
            }
        }

        /// <summary>
        /// Gets a list of active employees that exist on a specified team
        /// </summary>
        /// <param name="teamID">TB_Capstone_TEAM Primary Key value</param>
        /// <returns>Employee that exist on the team that matches the entered primary key</returns>
        public List<EmployeeWithForignKeyNamesDTO> LookupEmployeeByTeam(int teamID)
        {
            using (var context = new CapstoneContext())
            {
                var employeeList =
                    from person in context.TB_Capstone_EMPLOYEE
                    join team in context.TB_Capstone_TEAM_HISTORY on person.EmployeeID equals team.EmployeeID
                    where (person.DeactivationDate == null || person.DeactivationDate > DateTime.Now) &&
                          (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                           team.ActivationDate <= DateTime.Now &&
                           team.TeamID == teamID
                    select new EmployeeWithForignKeyNamesDTO()
                    {
                        EmployeeID = person.EmployeeID,
                        PositionID = person.PositionID,
                        PositionTitle = person.TB_Capstone_POSITION.PositionTitle,
                        UserID = person.UserID,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PhoneNumber = person.PhoneNumber,
                        AlternatePhoneNumber = person.AlternatePhoneNumber,
                        StationNumber = person.StationNumber,
                        ComputerNumber = person.ComputerNumber,
                        CompanyPhoneNumber = person.CompanyPhoneNumber,
                        BirthDate = person.BirthDate,
                        //StartDate = person.StartDate,
                        ActivationDate = person.ActivationDate,
                        DeactivationDate = person.DeactivationDate,
                        EmergencyContactName1 = person.EmergencyContactName1,
                        EmergencyContactPhoneNumber1 = person.EmergencyContactPhoneNumber1,
                        EmergencyContactName2 = person.EmergencyContactName2,
                        EmergencyContactPhoneNumber2 = person.EmergencyContactPhoneNumber2,
                        TeamID = team.TeamID,
                        TeamName = team.TB_Capstone_TEAM.TeamName,
                        RoleID = team.RoleID,
                        RoleTitle = team.TB_Capstone_ROLE.RoleTitle
                    };

                return employeeList.ToList();
            }
        }

        /// <summary>
        /// Gets a list of active employees that exist on a specified team
        /// </summary>
        /// <param name="teamID">TB_Capstone_TEAM Primary Key value</param>
        /// <param name="unsavedAssignment">List of UnsavedAssignmentDataClass that tells the search what to filter out</param>
        /// <returns>Employee that exist on the team that matches the entered primary key, minus the filtered items</returns>
        public List<EmployeeWithForignKeyNamesDTO> LookupEmployeeByTeam(int teamID, List<UnsavedAssignmentDataClass> unsavedAssignment)
        {
            var ignoredEmployeeList = new List<int>();
            if (unsavedAssignment != null)
            {
                ignoredEmployeeList = unsavedAssignment.Select(x => x.EmployeeID).ToList();
            }
            using (var context = new CapstoneContext())
            {
                var employeeList =
                    from person in context.TB_Capstone_EMPLOYEE
                    join team in context.TB_Capstone_TEAM_HISTORY on person.EmployeeID equals team.EmployeeID
                    where (person.DeactivationDate == null || person.DeactivationDate > DateTime.Now) &&
                          (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                           team.ActivationDate <= DateTime.Now &&
                           team.TeamID == teamID &&
                           !ignoredEmployeeList.Contains(person.EmployeeID)
                    select new EmployeeWithForignKeyNamesDTO()
                    {
                        EmployeeID = person.EmployeeID,
                        PositionID = person.PositionID,
                        PositionTitle = person.TB_Capstone_POSITION.PositionTitle,
                        UserID = person.UserID,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PhoneNumber = person.PhoneNumber,
                        AlternatePhoneNumber = person.AlternatePhoneNumber,
                        StationNumber = person.StationNumber,
                        ComputerNumber = person.ComputerNumber,
                        CompanyPhoneNumber = person.CompanyPhoneNumber,
                        BirthDate = person.BirthDate,
                        //StartDate = person.StartDate,
                        ActivationDate = person.ActivationDate,
                        DeactivationDate = person.DeactivationDate,
                        EmergencyContactName1 = person.EmergencyContactName1,
                        EmergencyContactPhoneNumber1 = person.EmergencyContactPhoneNumber1,
                        EmergencyContactName2 = person.EmergencyContactName2,
                        EmergencyContactPhoneNumber2 = person.EmergencyContactPhoneNumber2,
                        TeamID = team.TeamID,
                        TeamName = team.TB_Capstone_TEAM.TeamName,
                        RoleTitle = team.TB_Capstone_ROLE.RoleTitle
                    };

                return employeeList.ToList();
            }
        }

        /// <summary>
        /// Gets a list of active employees that exist on a specified team
        /// </summary>
        /// <param name="teamID"></param>
        /// <param name="getAllEmployeeInfo"></param>
        /// <returns></returns>
        public List<EmployeeWithForignKeyNamesDTO> LookupEmployeeByTeam(int teamID, bool getAllEmployeeInfo)
        {
            using (var context = new CapstoneContext())
            {
                var employeeList =
                    from person in context.TB_Capstone_EMPLOYEE
                    join team in context.TB_Capstone_TEAM_HISTORY on person.EmployeeID equals team.EmployeeID
                    where (person.DeactivationDate == null || person.DeactivationDate > DateTime.Now) &&
                          (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                           team.ActivationDate <= DateTime.Now &&
                           team.TeamID == teamID
                    select new EmployeeWithForignKeyNamesDTO()
                    {
                        EmployeeID = person.EmployeeID,
                        PositionID = person.PositionID,
                        PositionTitle = person.TB_Capstone_POSITION.PositionTitle,
                        UserID = person.UserID,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PhoneNumber = person.PhoneNumber,
                        AlternatePhoneNumber = person.AlternatePhoneNumber,
                        StationNumber = person.StationNumber,
                        ComputerNumber = person.ComputerNumber,
                        CompanyPhoneNumber = person.CompanyPhoneNumber,
                        BirthDate = person.BirthDate,
                        //StartDate = person.StartDate,
                        ActivationDate = person.ActivationDate,
                        DeactivationDate = person.DeactivationDate,
                        EmergencyContactName1 = person.EmergencyContactName1,
                        EmergencyContactPhoneNumber1 = person.EmergencyContactPhoneNumber1,
                        EmergencyContactName2 = person.EmergencyContactName2,
                        EmergencyContactPhoneNumber2 = person.EmergencyContactPhoneNumber2,
                        TeamID = team.TeamID,
                        TeamName = team.TB_Capstone_TEAM.TeamName
                    };

                return employeeList.ToList();
            }
        }

        /// <summary>
        /// Gets a listing of all active Positions, designed for a drop down
        /// </summary>
        /// <returns>A listing of all positions that either don't have a deactivation date, or have not reached their deactivation date</returns>
        public List<KeyValueDTO> LookupPosition()
        {
            using (var context = new DAL.Context.CapstoneContext())
            {
                var positionList =
                    from position in context.TB_Capstone_POSITION
                    where position.DeactivationDate == null || position.DeactivationDate > DateTime.Now
                    select new KeyValueDTO
                    {
                        Key = position.PositionID,
                        Value = position.PositionTitle
                    };

                return positionList.ToList();
            }
        }

        /// <summary>
        /// Gets a listing of all active teams
        /// </summary>
        /// <returns>A listing of all teams that either don't have a deactivation date, or have not reached their deactivation date</returns>
        public List<KeyValueDTO> LookupTeam()
        {
            using (var context = new DAL.Context.CapstoneContext())
            {
                var teamList =
                    from team in context.TB_Capstone_TEAM
                    where team.DeactivationDate == null || team.DeactivationDate > DateTime.Now
                    select new KeyValueDTO
                    {
                        Key = team.TeamID,
                        Value = team.TeamName
                    };

                return teamList.ToList();
            }
        }

        /// <summary>
        /// Gets a listing of all active roles
        /// </summary>
        /// <returns>A listing of all roles that either don't have a deactivation date, or have not reached their deactivation date</returns>
        public List<KeyValueDTO> LookupRole(int currentRole)
        {
            using (var context = new DAL.Context.CapstoneContext())
            {
                var roleList =
                    (from role in context.TB_Capstone_ROLE
                     where role.DeactivationDate == null || role.DeactivationDate > DateTime.Now
                     select new KeyValueDTO
                     {
                         Key = role.RoleID,
                         Value = role.RoleTitle
                     });
                switch (currentRole)
                {
                    case 1:
                        roleList = roleList.Where(x => x.Key != 2 || x.Key != 3 || x.Key != 4);
                        break;
                    case 2:
                        roleList = roleList.Where(x => x.Key != 4);
                        break;
                    case 3:
                        roleList = roleList.Where(x => x.Key != 2 || x.Key != 4);
                        break;
                    default:
                        break;
                }

                return roleList.ToList();
            }
        }

        /// <summary>
        /// Gets a listing of all active Schedule Types
        /// </summary>
        /// <returns>A listing of all Schedule Types that either don't have a deactivation date, or have not reached their deactivation date</returns>
        public List<KeyValueDTO> LookupScheduleType()
        {
            using (var context = new DAL.Context.CapstoneContext())
            {
                var typeList =
                    (from type in context.TB_Capstone_SCHEDULE_TYPE
                     where type.DeactivationDate == null || type.DeactivationDate > DateTime.Now
                     select new KeyValueDTO
                     {
                         Key = type.ScheduleTypeID,
                         Value = type.Name + " | " + type.HoursPerDay + " hours per day"
                     }).ToList();

                return typeList.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeEntry"></param>
        /// <param name="employeeID"></param>
        /// <returns>employeeID of the new employee</returns>
        public void CreateEmployee(EmployeeDTO employeeEntry, int employeeID)
        {

            var errorList = new List<Exception>();

            var birthYear = employeeEntry.BirthDate.Year;
            var startYear = employeeEntry.ActivationDate.Year;
            var phoneFormat = new Regex(@"\+[0-9]\([0-9][0-9][0-9]\)-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]");


            if (startYear - birthYear < 18)
            {
                var currentBirthDate = employeeEntry.BirthDate;
                errorList.Add(new Exception("Employees must be at least 18 years old."));
            }

            if (employeeEntry.DeactivationDate < employeeEntry.ActivationDate)
            {
                errorList.Add(new Exception("The start date must come before the end date."));
            }
            if (!phoneFormat.Match(employeeEntry.PhoneNumber).Success)
            {
                errorList.Add(new Exception("The Phone Number must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
            }
            if (employeeEntry.AlternatePhoneNumber != null)
            {
                if (!phoneFormat.Match(employeeEntry.AlternatePhoneNumber).Success)
                {
                    errorList.Add(new Exception("The Alternate Phone Number must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                }
            }
            if (!phoneFormat.Match(employeeEntry.CompanyPhoneNumber).Success)
            {
                errorList.Add(new Exception("The Company Phone Number must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
            }
            if (!phoneFormat.Match(employeeEntry.EmergencyContactPhoneNumber1).Success)
            {
                errorList.Add(new Exception("The Emergency Contact Phone Number 1 must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
            }
            if (employeeEntry.EmergencyContactPhoneNumber2 != null)
            {
                if (!phoneFormat.Match(employeeEntry.EmergencyContactPhoneNumber2).Success)
                {
                    errorList.Add(new Exception("The Emergency Contact Phone Number 2 must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                }
            }
            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var scope = new TransactionScope())
            {
                using (var context = new CapstoneContext())
                {
                    var newEmployee = new TB_Capstone_EMPLOYEE();
                    newEmployee.UserID = employeeEntry.UserID;
                    newEmployee.FirstName = employeeEntry.FirstName;
                    newEmployee.LastName = employeeEntry.LastName;
                    newEmployee.ActivationDate = employeeEntry.ActivationDate;
                    newEmployee.AlternatePhoneNumber = employeeEntry.AlternatePhoneNumber;
                    newEmployee.BirthDate = employeeEntry.BirthDate;
                    newEmployee.CompanyPhoneNumber = employeeEntry.CompanyPhoneNumber;
                    newEmployee.ComputerNumber = employeeEntry.ComputerNumber;
                    newEmployee.EmergencyContactName1 = employeeEntry.EmergencyContactName1;
                    newEmployee.EmergencyContactName2 = employeeEntry.EmergencyContactName2;
                    newEmployee.EmergencyContactPhoneNumber1 = employeeEntry.EmergencyContactPhoneNumber1;
                    newEmployee.EmergencyContactPhoneNumber2 = employeeEntry.EmergencyContactPhoneNumber2;
                    newEmployee.PhoneNumber = employeeEntry.PhoneNumber;
                    newEmployee.PositionID = employeeEntry.PositionID;
                    newEmployee.StationNumber = employeeEntry.StationNumber;
                    newEmployee.CreatedBy = employeeID;
                    newEmployee.CreationDate = DateTime.Now;

                    context.TB_Capstone_EMPLOYEE.Add(newEmployee);
                    context.SaveChanges();

                    var newTeamHistory = new TB_Capstone_TEAM_HISTORY();
                    newTeamHistory.RoleID = employeeEntry.RoleID;
                    newTeamHistory.TeamID = employeeEntry.TeamID;
                    newTeamHistory.EmployeeID = newEmployee.EmployeeID;
                    newTeamHistory.CreatedBy = employeeID;
                    newTeamHistory.CreationDate = DateTime.Now;
                    newTeamHistory.ActivationDate = DateTime.Now;

                    var newScheduleTypeDetail = new TB_Capstone_SCHEDULE_TYPE_DETAILS();
                    newScheduleTypeDetail.ScheduleTypeID = employeeEntry.ScheduleTypeID;
                    newScheduleTypeDetail.EmployeeID = newEmployee.EmployeeID;
                    newScheduleTypeDetail.CreatedBy = employeeID;
                    newScheduleTypeDetail.CreationDate = DateTime.Now;
                    newScheduleTypeDetail.ActivationDate = DateTime.Now;

                    var offDayController = new OffDayTypeController();
                    List<OffDayTypeDTO> offDayTypeList = offDayController.LookupOffDayType();
                    foreach (var item in offDayTypeList)
                    {
                        var entitledItem = new TB_Capstone_ENTITLED_TIME_OFF
                        {
                            EmployeeID = newEmployee.EmployeeID,
                            OffDayID = item.OffDayID,
                            HoursAccumulated = 0,
                            ActivationDate = DateTime.Now,
                            CreatedBy = employeeID,
                            CreationDate = DateTime.Now
                        };
                        context.TB_Capstone_ENTITLED_TIME_OFF.Add(entitledItem);
                    }

                    context.TB_Capstone_TEAM_HISTORY.Add(newTeamHistory);
                    context.TB_Capstone_SCHEDULE_TYPE_DETAILS.Add(newScheduleTypeDetail);
                    context.SaveChanges();
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// Edits a single employee by changing values
        /// </summary>
        /// <param name="employee">Data Class for the edited employee information</param>
        /// <param name="employeeID">ID of the employee who is editing this employee</param>
        /// <returns>employeeID of the new employee</returns>
        public void EditEmployee(EmployeeDTO employee, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var errorList = new List<Exception>();

                var birthYear = employee.BirthDate.Year;
                var startYear = employee.ActivationDate.Year;
                var phoneFormat = new Regex(@"\+[0-9]\([0-9][0-9][0-9]\)-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]");

                if (startYear - birthYear < 18)
                {
                    errorList.Add(new Exception("Employees must be at least 18 years old."));
                }

                if (employee.DeactivationDate < employee.ActivationDate)
                {
                    errorList.Add(new Exception("The start date must come before the end date."));
                }

                if (!phoneFormat.Match(employee.PhoneNumber).Success)
                {
                    errorList.Add(new Exception("The Phone Number must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                }
                if (employee.AlternatePhoneNumber != null)
                {
                    if (!phoneFormat.Match(employee.AlternatePhoneNumber).Success)
                    {
                        errorList.Add(new Exception("The Alternate Phone Number must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                    }
                }
                if (!phoneFormat.Match(employee.CompanyPhoneNumber).Success)
                {
                    errorList.Add(new Exception("The Company Phone Number must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                }
                if (!phoneFormat.Match(employee.EmergencyContactPhoneNumber1).Success)
                {
                    errorList.Add(new Exception("The Emergency Contact Phone Number 1 must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                }
                if (employee.EmergencyContactPhoneNumber2 != null)
                {
                    if (!phoneFormat.Match(employee.EmergencyContactPhoneNumber2).Success)
                    {
                        errorList.Add(new Exception("The Emergency Contact Phone Number 2 must be in format \"+N(NNN)-NNN-NNNN\", where each N is a number."));
                    }
                }

                if (errorList.Count > 0)
                {
                    throw new AggregateException("", errorList);
                }

                TB_Capstone_EMPLOYEE editedEmployee = context.TB_Capstone_EMPLOYEE.Find(employee.EmployeeID);

                editedEmployee.EmployeeID = employee.EmployeeID;
                editedEmployee.PositionID = employee.PositionID;
                editedEmployee.FirstName = employee.FirstName;
                editedEmployee.LastName = employee.LastName;
                editedEmployee.UserID = employee.UserID;
                editedEmployee.PhoneNumber = employee.PhoneNumber;
                editedEmployee.AlternatePhoneNumber = employee.AlternatePhoneNumber;
                editedEmployee.StationNumber = employee.StationNumber;
                editedEmployee.ComputerNumber = employee.ComputerNumber;
                editedEmployee.CompanyPhoneNumber = employee.CompanyPhoneNumber;
                editedEmployee.BirthDate = employee.BirthDate;
                editedEmployee.DeactivationDate = employee.DeactivationDate;
                editedEmployee.EmergencyContactName1 = employee.EmergencyContactName1;
                editedEmployee.EmergencyContactPhoneNumber1 = employee.EmergencyContactPhoneNumber1;
                editedEmployee.EmergencyContactName2 = employee.EmergencyContactName2;
                editedEmployee.EmergencyContactPhoneNumber2 = employee.EmergencyContactPhoneNumber2;
                editedEmployee.UpdatedBy = employeeID;
                editedEmployee.UpdatedDate = DateTime.Now;



                TB_Capstone_SCHEDULE_TYPE_DETAILS currentScheduleType =
                    context.TB_Capstone_SCHEDULE_TYPE_DETAILS.Where
                    (
                        x => x.EmployeeID == employee.EmployeeID &&
                        (x.DeactivationDate == null || x.DeactivationDate > DateTime.Now) &&
                        x.ActivationDate <= DateTime.Now
                    ).FirstOrDefault();

                TB_Capstone_TEAM_HISTORY currentRole =
                    context.TB_Capstone_TEAM_HISTORY.Where
                    (
                        x => x.EmployeeID == employee.EmployeeID &&
                        (x.DeactivationDate == null || x.DeactivationDate > DateTime.Now) &&
                        x.ActivationDate <= DateTime.Now
                    ).FirstOrDefault();

                if (currentScheduleType.ScheduleTypeID != employee.ScheduleTypeID)
                {
                    currentScheduleType.DeactivationDate = DateTime.Now;
                    currentScheduleType.UpdatedBy = employeeID;
                    currentScheduleType.UpdatedDate = DateTime.Now;

                    var newScheduleTypeDetail = new TB_Capstone_SCHEDULE_TYPE_DETAILS();
                    newScheduleTypeDetail.ScheduleTypeID = employee.ScheduleTypeID;
                    newScheduleTypeDetail.EmployeeID = employee.EmployeeID;
                    newScheduleTypeDetail.CreatedBy = employeeID;
                    newScheduleTypeDetail.CreationDate = DateTime.Now;
                    newScheduleTypeDetail.ActivationDate = DateTime.Now;

                    context.Entry(currentScheduleType).State = EntityState.Modified;
                    context.TB_Capstone_SCHEDULE_TYPE_DETAILS.Add(newScheduleTypeDetail);
                }
                if (currentRole.RoleID != employee.RoleID)
                {
                    currentRole.DeactivationDate = DateTime.Now;
                    currentRole.UpdatedBy = employeeID;
                    currentRole.UpdatedDate = DateTime.Now;

                    var newRole = new TB_Capstone_TEAM_HISTORY();
                    newRole.RoleID = employee.RoleID;
                    newRole.TeamID = currentRole.TeamID;
                    newRole.EmployeeID = employee.EmployeeID;
                    newRole.CreatedBy = employeeID;
                    newRole.CreationDate = DateTime.Now;
                    newRole.ActivationDate = DateTime.Now;

                    context.Entry(currentRole).State = EntityState.Modified;
                    context.TB_Capstone_TEAM_HISTORY.Add(newRole);
                }

                context.Entry(editedEmployee).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeactivateEmployee(int employeeID, int modifyingEmployeeID, DateTime deactivationDate)
        {
            var errorList = new List<Exception>();
            if (employeeID == modifyingEmployeeID)
            {
                errorList.Add(new Exception("You can not deactivate yourself."));
            }
            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var context = new CapstoneContext())
            {
                var deactivatedEmployee = context.TB_Capstone_EMPLOYEE.Find(employeeID);

                var employeeHistory = context.TB_Capstone_TEAM_HISTORY.Where(team => (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                         team.ActivationDate <= DateTime.Now &&
                         team.EmployeeID == deactivatedEmployee.EmployeeID).FirstOrDefault();

                var employeeSchedule = context.TB_Capstone_SCHEDULE_TYPE_DETAILS.Where(time => (time.DeactivationDate == null || time.DeactivationDate > DateTime.Now) &&
                         time.ActivationDate <= DateTime.Now &&
                         time.EmployeeID == deactivatedEmployee.EmployeeID).FirstOrDefault();

                var employeeEntitlments = context.TB_Capstone_ENTITLED_TIME_OFF.Where(offDay => offDay.EmployeeID == deactivatedEmployee.EmployeeID);

                var employeeAllocations = context.TB_Capstone_ALLOCATION.Where(allo => (allo.DeactivationDate == null || allo.DeactivationDate > DateTime.Now) &&
                         allo.ActivationDate <= DateTime.Now &&
                         allo.EmployeeID == deactivatedEmployee.EmployeeID);

                deactivatedEmployee.DeactivationDate = deactivationDate;
                deactivatedEmployee.UpdatedBy = modifyingEmployeeID;
                deactivatedEmployee.UpdatedDate = DateTime.Now;

                employeeHistory.DeactivationDate = deactivationDate;
                employeeHistory.UpdatedBy = modifyingEmployeeID;
                employeeHistory.UpdatedDate = DateTime.Now;

                employeeSchedule.DeactivationDate = deactivationDate;
                employeeSchedule.UpdatedBy = modifyingEmployeeID;
                employeeSchedule.UpdatedDate = DateTime.Now;

                context.Entry(deactivatedEmployee).State = EntityState.Modified;
                context.Entry(employeeSchedule).State = EntityState.Modified;
                context.Entry(employeeSchedule).State = EntityState.Modified;

                if (employeeEntitlments != null)
                {
                    foreach (var item in employeeEntitlments)
                    {
                        item.DeactivationDate = deactivationDate;
                        item.UpdatedBy = modifyingEmployeeID;
                        item.UpdatedDate = DateTime.Now;
                        context.Entry(item).State = EntityState.Modified;
                    }
                }
                if (employeeAllocations != null)
                {
                    foreach (var item in employeeAllocations)
                    {
                        item.DeactivationDate = deactivationDate;
                        item.UpdatedBy = modifyingEmployeeID;
                        item.UpdatedDate = DateTime.Now;
                        context.Entry(item).State = EntityState.Modified;
                    }
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the entitled time off for a single employee
        /// </summary>
        /// <param name="employeeID">Key Value for TB_Capstone_EMPLOYEE Table</param>
        /// <returns>A list of entitled time off data for entered employee</returns>
        public List<EntiltedTimeOffDTO> LookupEntitledTimeOff(int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var entitlements =
                    (from timeOff in context.TB_Capstone_ENTITLED_TIME_OFF
                     where (timeOff.DeactivationDate == null || timeOff.DeactivationDate > DateTime.Now) &&
                             timeOff.ActivationDate <= DateTime.Now &&
                             timeOff.EmployeeID == employeeID
                     select new EntiltedTimeOffDTO
                     {
                         EmployeeID = timeOff.EmployeeID,
                         OffDayTypeDescription = timeOff.TB_Capstone_OFFDAY_TYPE.Name,
                         OffDayID = timeOff.OffDayID,
                         HoursAccumulated = timeOff.HoursAccumulated
                     }).ToList();

                return entitlements;
            }
        }

        /// <summary>
        /// Updates a list of TB_Capstone_ENTITLED_TIME_OFF to newer values
        /// </summary>
        /// <param name="entitlededTimeOffs">List of entitled time off changes</param>
        /// <param name="employeeID">Employee Id of the user who made the change</param>
        public void EditEntitledTimeOff(List<EntiltedTimeOffDTO> entitlements, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                foreach (var item in entitlements)
                {
                    var entitledTimeOff = new TB_Capstone_ENTITLED_TIME_OFF();
                    entitledTimeOff = context.TB_Capstone_ENTITLED_TIME_OFF.Find(item.OffDayID, item.EmployeeID);
                    entitledTimeOff.HoursAccumulated = item.HoursAccumulated;
                    entitledTimeOff.UpdatedBy = employeeID;
                    entitledTimeOff.UpdatedDate = DateTime.Now;

                    context.Entry(entitledTimeOff).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }

    }
}