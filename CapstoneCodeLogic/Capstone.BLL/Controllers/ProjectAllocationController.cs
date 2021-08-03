using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Capstone.BLL.DTOs.ProjectAllocation;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    ///<summary>
    ///Controller class for Project Allocations
    ///</summary>
    public class ProjectAllocationController
    {
        /// <summary>
        /// Lookups all employees by team that are not on the selected project
        /// </summary>
        /// <returns>List of EmployeeAllocationDTOs as employeeList</returns>
        /// <param name="projectID">The Key for the project from TB_Capstone_PROJECT, used to filter results</param>
        /// <param name="teamID">The Key for the Team from TB_Capstone_TEAM, used to filter team members</param>
        /// <param name="year">The current year of the model</param>
        public List<EmployeeAllocationDTO> LookupEmployeesByTeam(int teamID, int projectID, int year)
        {
            using (var context = new CapstoneContext())
            {
                //var ignoredEmployees = new List<int>();

                //ignoredEmployees = LookupEmployeesForProject(projectID, year).Select(x => x.EmployeeID).ToList();

                var employeeList = (from emps in context.TB_Capstone_EMPLOYEE
                                    join team in context.TB_Capstone_TEAM_HISTORY on emps.EmployeeID equals team.EmployeeID
                                    join position in context.TB_Capstone_POSITION on emps.PositionID equals position.PositionID
                                    where team.TeamID == teamID
                                    //&& !ignoredEmployees.Contains(emps.EmployeeID)
                                    select new EmployeeAllocationDTO
                                    {
                                        EmployeeID = emps.EmployeeID,
                                        TeamID = team.TeamID,
                                        PositionID = position.PositionID,
                                        PositionName = position.PositionTitle,
                                        RoleName = team.TB_Capstone_ROLE.RoleTitle,
                                        FirstName = emps.FirstName,
                                        LastName = emps.LastName,
                                    }).ToList();

                return employeeList;
            }
        }

        /// <summary>
        /// Lookups all employees who have existing allocation to the current project. Also lookups all allocation across all projects.
        /// </summary>
        /// <returns>List of AllocatedEmployeeDTOs as empList</returns>
        /// <param name="projectID">The Key for the project from TB_Capstone_PROJECT, used to filter results</param>
        /// <param name="year">The current year of the model</param>
        public List<AllocatedEmployeesDTO> LookupEmployeesForProject(int projectID, int year)
        {
            using (var context = new CapstoneContext())
            {
                var empList = (from allo in context.TB_Capstone_ALLOCATION
                               join project in context.TB_Capstone_PROJECT on allo.ProjectID equals project.ProjectID
                               join employee in context.TB_Capstone_EMPLOYEE on allo.EmployeeID equals employee.EmployeeID
                               where allo.ProjectID == projectID
                               && allo.DeactivationDate == null
                               group allo by allo.EmployeeID into empgroup
                               select new AllocatedEmployeesDTO
                               {
                                   EmployeeID = empgroup.Key,
                                   EmployeeName = (from emp in context.TB_Capstone_EMPLOYEE
                                                   where emp.EmployeeID == empgroup.Key
                                                   select emp.FirstName + " " + emp.LastName).FirstOrDefault(),
                                   Year = year,
                                   AllocationID = empgroup.Select(x => x.AllocationID).FirstOrDefault(),
                                   allocatedDays = (from days in context.TB_Capstone_ALLOCATION
                                                    where days.Year == year
                                                    && days.EmployeeID == empgroup.Key
                                                    && days.DeactivationDate == null
                                                    group days by days.Year into g
                                                    select new AllocatedDaysDTO
                                                    {
                                                        Year = g.Key,
                                                        January = g.Sum(s => s.January),
                                                        February = g.Sum(s => s.February),
                                                        March = g.Sum(s => s.March),
                                                        April = g.Sum(s => s.April),
                                                        May = g.Sum(s => s.May),
                                                        June = g.Sum(s => s.June),
                                                        July = g.Sum(s => s.July),
                                                        August = g.Sum(s => s.August),
                                                        September = g.Sum(s => s.September),
                                                        October = g.Sum(s => s.October),
                                                        November = g.Sum(s => s.November),
                                                        December = g.Sum(s => s.December)
                                                    }).ToList(),
                                   projectDays = (from allocation in context.TB_Capstone_ALLOCATION
                                                  where allocation.Year == year
                                                  && allocation.ProjectID == projectID
                                                  && allocation.EmployeeID == empgroup.Key
                                                  && allocation.DeactivationDate == null
                                                  select new ProjectAllocatedDaysDTO
                                                  {
                                                      Year = allocation.Year,
                                                      EmployeeID = allocation.EmployeeID,
                                                      AllocationID = allocation.AllocationID,
                                                      ProjectJanuary = allocation.January,
                                                      ProjectFebruary = allocation.February,
                                                      ProjectMarch = allocation.March,
                                                      ProjectApril = allocation.April,
                                                      ProjectMay = allocation.May,
                                                      ProjectJune = allocation.June,
                                                      ProjectJuly = allocation.July,
                                                      ProjectAugust = allocation.August,
                                                      ProjectSeptember = allocation.September,
                                                      ProjectOctober = allocation.October,
                                                      ProjectNovember = allocation.November,
                                                      ProjectDecember = allocation.December
                                                  }).ToList()
                               }).ToList();

                return empList;
            }
        }

        /// <summary>
        /// Updates all employees to a project. Stores an old record of the allocation before it creates a new one of the incoming allocation.
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="projectAllocatedDaysDTOs">All the values from the viewModels on the _AllocationPartial</param>
        /// <param name="projectID">The Key for the project from TB_Capstone_Project, used to assign the project</param>
        /// <param name="employeeID">The currently logged in user</param>
        public void UpdateAllocation(List<ProjectAllocatedDaysDTO> projectAllocatedDaysDTOs, int projectID, int employeeID)
        {
            var errorList = new List<Exception>();
            #region validation
            if (projectID == 0)
            {
                errorList.Add(new Exception("You must select a project."));
            }

            foreach (var item in projectAllocatedDaysDTOs)
            {
                var employeeName = string.Empty;
                employeeName = LookupEmployeeName(item.EmployeeID);

                #region Checking if incoming allocation is greater than month length
                if (item.ProjectJanuary > 31 || item.ProjectJanuary > 31.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for January cannot be greater than 31."));
                }
                else if (DateTime.IsLeapYear(item.Year))
                {
                    if (item.ProjectFebruary > 29 || item.ProjectFebruary > 29.0m)
                    {
                        errorList.Add(new Exception($"{employeeName}'s allocation for February cannot be greater than 29."));
                    }
                }
                else if (!DateTime.IsLeapYear(item.Year))
                {
                    if (item.ProjectFebruary > 28 || item.ProjectFebruary > 28.0m)
                    {
                        errorList.Add(new Exception($"{employeeName}'s allocation for February cannot be greater than 28."));
                    }
                }
                else if (item.ProjectMarch > 31 || item.ProjectMarch > 31.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for March cannot be greater than 31."));
                }
                else if (item.ProjectApril > 30 || item.ProjectApril > 30.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for April cannot be greater than 30."));
                }
                else if (item.ProjectMay > 31 || item.ProjectMay > 31.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for May cannot be greater than 31."));
                }
                else if (item.ProjectJune > 30 || item.ProjectJune > 30.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for June cannot be greater than 30."));
                }
                else if (item.ProjectJuly > 31 || item.ProjectMay > 31.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for July cannot be greater than 31."));
                }
                else if (item.ProjectAugust > 31 || item.ProjectMay > 31.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for August cannot be greater than 31."));
                }
                else if (item.ProjectSeptember > 30 || item.ProjectJune > 30.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for September cannot be greater than 30."));
                }
                else if (item.ProjectOctober > 31 || item.ProjectMay > 31.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for October cannot be greater than 31."));
                }
                else if (item.ProjectNovember > 30 || item.ProjectJune > 30.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for November cannot be greater than 30."));
                }
                else if (item.ProjectDecember > 30 || item.ProjectJune > 30.0m)
                {
                    errorList.Add(new Exception($"{employeeName}'s allocation for December cannot be greater than 31."));
                }
                #endregion

                #region Check if Greater than month length
                //var userAllocation = LookupEmployeesAllocation(item.Year, projectID, item.EmployeeID);

                //if (userAllocation != null)
                //{
                //    if (item.ProjectJanuary + userAllocation.January > 31)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for January cannot be greater than 31."));
                //    }
                //    else if (DateTime.IsLeapYear(item.Year))
                //    {
                //        if (item.ProjectFebruary + userAllocation.February > 29)
                //        {
                //            errorList.Add(new Exception($"{employeeName}'s allocation for February cannot be greater than 29."));
                //        }
                //    }
                //    else if (!DateTime.IsLeapYear(item.Year))
                //    {
                //        if (item.ProjectFebruary + userAllocation.February > 28)
                //        {
                //            errorList.Add(new Exception($"{employeeName}'s allocation for February cannot be greater than 28."));
                //        }
                //    }
                //    else if (item.ProjectMarch + userAllocation.March > 31)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for March cannot be greater than 31."));
                //    }
                //    else if (item.ProjectApril + userAllocation.April > 30)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for April cannot be greater than 30."));
                //    }
                //    else if (item.ProjectMay + userAllocation.May > 31)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for May cannot be greater than 31."));
                //    }
                //    else if (item.ProjectJune + userAllocation.June > 30)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for June cannot be greater than 30."));
                //    }
                //    else if (item.ProjectJuly + userAllocation.July > 31)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for July cannot be greater than 31."));
                //    }
                //    else if (item.ProjectAugust + userAllocation.August > 31)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for August cannot be greater than 31."));
                //    }
                //    else if (item.ProjectSeptember + userAllocation.September > 30)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for September cannot be greater than 30."));
                //    }
                //    else if (item.ProjectOctober + userAllocation.October > 31)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for October cannot be greater than 31."));
                //    }
                //    else if (item.ProjectNovember + userAllocation.November > 30)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for November cannot be greater than 30."));
                //    }
                //    else if (item.ProjectDecember + userAllocation.December > 30)
                //    {
                //        errorList.Add(new Exception($"{employeeName}'s allocation for December cannot be greater than 31."));
                //    }
                //}
                #endregion
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            #endregion

            using (var context = new CapstoneContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    bool changes = false;

                    try
                    {
                        foreach (var item in projectAllocatedDaysDTOs)
                        {
                            var currentInstance = context.TB_Capstone_ALLOCATION.Find(item.AllocationID);

                            if (item.ProjectJanuary != currentInstance.January ||
                                item.ProjectFebruary != currentInstance.February ||
                                item.ProjectMarch != currentInstance.March ||
                                item.ProjectApril != currentInstance.April ||
                                item.ProjectMay != currentInstance.May ||
                                item.ProjectJune != currentInstance.June ||
                                item.ProjectJuly != currentInstance.July ||
                                item.ProjectAugust != currentInstance.August ||
                                item.ProjectSeptember != currentInstance.September ||
                                item.ProjectOctober != currentInstance.October ||
                                item.ProjectNovember != currentInstance.November ||
                                item.ProjectDecember != currentInstance.December)
                            {
                                currentInstance.DeactivationDate = DateTime.Now;
                                currentInstance.UpdatedBy = employeeID;
                                currentInstance.UpdatedDate = DateTime.Now;

                                context.Entry(currentInstance).State = EntityState.Modified;
                                context.SaveChanges();

                                context.TB_Capstone_ALLOCATION.Add(new TB_Capstone_ALLOCATION
                                {
                                    EmployeeID = item.EmployeeID,
                                    Year = item.Year,
                                    ProjectID = projectID,
                                    January = item.ProjectJanuary,
                                    February = item.ProjectFebruary,
                                    March = item.ProjectMarch,
                                    April = item.ProjectApril,
                                    May = item.ProjectMay,
                                    June = item.ProjectJune,
                                    July = item.ProjectJuly,
                                    August = item.ProjectAugust,
                                    September = item.ProjectSeptember,
                                    October = item.ProjectOctober,
                                    November = item.ProjectNovember,
                                    December = item.ProjectDecember,
                                    AllocatedDays = item.ProjectJanuary + item.ProjectFebruary + item.ProjectMarch + item.ProjectApril + item.ProjectMay + item.ProjectJune + item.ProjectJuly +
                                                    item.ProjectAugust + item.ProjectSeptember + item.ProjectOctober + item.ProjectNovember + item.ProjectDecember,
                                    ActivationDate = DateTime.Now,
                                    CreatedBy = employeeID,
                                    CreationDate = DateTime.Now
                                });
                                context.SaveChanges();
                                changes = true;
                            }
                        }
                        if (changes == true)
                        {
                            transaction.Commit();
                        }

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// Lookups all the allocation to a specific employee. Used as a supporting method in UpdateAllocations validation
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="year">The current year of the model/param>
        /// <param name="projectID">The Key for the project from TB_Capstone_Project, used to assign the project</param>
        /// <param name="employeeID">The currently logged in user</param>
        public AllocatedDaysDTO LookupEmployeesAllocation(int year, int projectID, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var allocationDays = (from days in context.TB_Capstone_ALLOCATION
                                      where days.Year == year
                                      && days.EmployeeID == employeeID
                                      && days.DeactivationDate == null
                                      && days.ProjectID != projectID
                                      group days by days.Year into g
                                      select new AllocatedDaysDTO
                                      {
                                          Year = g.Key,
                                          January = g.Sum(s => s.January),
                                          February = g.Sum(s => s.February),
                                          March = g.Sum(s => s.March),
                                          April = g.Sum(s => s.April),
                                          May = g.Sum(s => s.May),
                                          June = g.Sum(s => s.June),
                                          July = g.Sum(s => s.July),
                                          August = g.Sum(s => s.August),
                                          September = g.Sum(s => s.September),
                                          October = g.Sum(s => s.October),
                                          November = g.Sum(s => s.November),
                                          December = g.Sum(s => s.December)
                                      }).FirstOrDefault();

                return allocationDays;
            }
        }

        /// <summary>
        /// Supporting method to lookup an employees name, used in Update Allocation
        /// </summary>
        /// <returns>Returns a string of the selected employee Name as employeeName</returns>
        /// <param name="employeeID">The EmployeeID of the selected employee from the TB_Capstone_EMPLOYEE table</param>
        public string LookupEmployeeName(int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var employeeName = (from emps in context.TB_Capstone_EMPLOYEE
                                    where emps.EmployeeID == employeeID
                                    select emps.FirstName + " " + emps.LastName).FirstOrDefault();

                return employeeName;
            }
        }

        /// <summary>
        /// Creates a new Allocation record for the selected employee
        /// </summary>
        /// <returns>Returns a List of AllocatedEmployeesDTOs as AllocatedEmployees</returns>
        /// <param name="year">The current year of the model/param>
        /// <param name="projectID">The Key for the project from TB_Capstone_Project, used to assign the project</param>
        /// <param name="createdEmployeeID">The currently logged in user</param>
        /// <param name="employeeID">The EmployeeID of the selected employee from the TB_Capstone_EMPLOYEE table</param>
        public List<AllocatedEmployeesDTO> CreateNewAllocation(int employeeID, int year, int projectID, int createdEmployeeID)
        {
            using (var context = new CapstoneContext())
            {
                var alreadyExisting = (from item in context.TB_Capstone_ALLOCATION
                                       where item.DeactivationDate == null
                                       && item.Year == year
                                       && item.ProjectID == projectID
                                       select item.EmployeeID).ToList();

                var AllocatedEmployees = new List<AllocatedEmployeesDTO>();
                if (alreadyExisting.Contains(employeeID))
                {
                    AllocatedEmployees = LookupEmployeesForProject(projectID, year);
                }
                else
                {
                    var newAllocation = new TB_Capstone_ALLOCATION();
                    newAllocation.EmployeeID = employeeID;
                    newAllocation.Year = year;
                    newAllocation.ProjectID = projectID;
                    newAllocation.January = 0.0m;
                    newAllocation.February = 0.0m;
                    newAllocation.March = 0.0m;
                    newAllocation.April = 0.0m;
                    newAllocation.May = 0.0m;
                    newAllocation.June = 0.0m;
                    newAllocation.July = 0.0m;
                    newAllocation.August = 0.0m;
                    newAllocation.September = 0.0m;
                    newAllocation.October = 0.0m;
                    newAllocation.November = 0.0m;
                    newAllocation.December = 0.0m;
                    newAllocation.CreatedBy = createdEmployeeID;
                    newAllocation.CreationDate = DateTime.Now;
                    newAllocation.ActivationDate = DateTime.Now;

                    context.TB_Capstone_ALLOCATION.Add(newAllocation);
                    context.SaveChanges();

                    AllocatedEmployees = LookupEmployeesForProject(projectID, year);
                }

                return AllocatedEmployees;
            }
        }

        /// <summary>
        /// Deactivates an employee allocation by updating the existing record in TB_Capstone_ALLOCATION's DeactivationDate to the current day
        /// </summary>
        /// <returns>Returns a List of AllocatedEmployeesDTOs as AllocatedEmployees</returns>
        /// <param name="year">The current year of the model/param>
        /// <param name="projectID">The Key for the project from TB_Capstone_Project, used to assign the project</param>
        /// <param name="employeeID">The currently logged in user</param>
        /// <param name="allocationID">The key for the TB_Capstone_ALLOCATION record that will be deactivated</param>
        public List<AllocatedEmployeesDTO> DeactivateAllocation(int allocationID, int employeeID, int projectID, int year)
        {
            using (var context = new CapstoneContext())
            {
                var deactivateAllocation = context.TB_Capstone_ALLOCATION.Find(allocationID);

                deactivateAllocation.DeactivationDate = DateTime.Now;
                deactivateAllocation.UpdatedBy = employeeID;
                deactivateAllocation.UpdatedDate = DateTime.Now;

                context.Entry(deactivateAllocation).State = EntityState.Modified;
                context.SaveChanges();

                var AllocatedEmployees = LookupEmployeesForProject(projectID, year);

                return AllocatedEmployees;
            }
        }

    }
}
