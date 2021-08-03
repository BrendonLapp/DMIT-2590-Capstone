using System.Collections.Generic;
using System.Linq;
using Capstone.BLL.DTOs.PersonalProjects;
using Capstone.DAL.Context;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// ProjectCategory CRUD Controller
    /// </summary>
    public class PersonalProjectsController
    {
        /// <summary>
        /// Gets a list of Projects and employee is assigned to
        /// </summary>
        /// <returns>A list of projects an employee is assigned to/returns>
        /// <param name="employeeID">TB_Capstone_ALLOCATIONs employee ID to conduct the search on</param>
        public List<PersonalProjectDTO> LookupPersonalProjects(int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                List<PersonalProjectDTO> personalProjectDTOs = new List<PersonalProjectDTO>();

                personalProjectDTOs = (from projects in context.TB_Capstone_ALLOCATION
                                       where projects.EmployeeID == employeeID
                                       select new PersonalProjectDTO
                                       {
                                           ProjectID = projects.ProjectID,
                                           EmployeeID = projects.EmployeeID,
                                           ProjectName = projects.TB_Capstone_PROJECT.ProjectName,
                                           AllocatedDays = projects.AllocatedDays,
                                           AllocationID = projects.AllocationID,
                                           ActivationDate = projects.ActivationDate,
                                           DeactivationDate = projects.DeactivationDate,
                                           StartDate = (from allocation in context.TB_Capstone_ALLOCATION
                                                        where allocation.EmployeeID == employeeID && allocation.ProjectID == projects.ProjectID
                                                        orderby allocation.Year
                                                        select allocation.Year).FirstOrDefault()
                                       }).ToList();

                return personalProjectDTOs;
            }
        }

        /// <summary>
        /// Gets an individual project an employee is assigned to
        /// </summary>
        /// <returns>A projects standard information/returns>
        /// <param name="projectID">TB_Capstone_ALLOCATIONs project ID to filter the result on</param>
        /// <param name="employeeID">TB_Capstone_ALLOCATIONs employee ID To filter the result on</param>
        public PersonalProjectBreakdownDTO LookupPersonalProject(int projectID, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                PersonalProjectBreakdownDTO breakdownDTO = new PersonalProjectBreakdownDTO();

                breakdownDTO = (from project in context.TB_Capstone_ALLOCATION
                                where project.EmployeeID == employeeID
                                    && project.ProjectID == projectID
                                orderby project.Year
                                select new PersonalProjectBreakdownDTO
                                {
                                    AllocationID = project.AllocationID,
                                    ProjectID = project.ProjectID,
                                    EmployeeID = project.EmployeeID,
                                    ProjectName = project.TB_Capstone_PROJECT.ProjectName,
                                    CategoryName = project.TB_Capstone_PROJECT.TB_Capstone_PROJECT_CATEGORY.CategoryName,
                                    Description = project.TB_Capstone_PROJECT.Description,
                                    Startdate = project.TB_Capstone_PROJECT.StartDate,
                                    ForecastedEndDate = project.TB_Capstone_PROJECT.ProjectedEndDate,
                                    Year = project.Year
                                }).FirstOrDefault();

                return breakdownDTO;
            }
        }

        /// <summary>
        /// Gets an individual projects monthly allocation for a calendar year based on an employee ID
        /// </summary>
        /// <returns>A projects monthly allocation per a calendar year /returns>
        /// <param name="projectID">TB_Capstone_ALLOCATIONs project ID to filter the result on</param>
        /// <param name="employeeID">TB_Capstone_ALLOCATIONs employee ID To filter the result on</param>
        /// <param name="Year">TB_Capstone_ALLOCATIONs year that will filter what is in the DTO</param>
        public YearBreakdownDTO LookupYearBreakdownForProject(int EmployeeID, int ProjectID, int Year)
        {
            using (var context = new CapstoneContext())
            {
                YearBreakdownDTO yearBreakdownDTO = new YearBreakdownDTO();

                yearBreakdownDTO = (from project in context.TB_Capstone_ALLOCATION
                                    where project.EmployeeID == EmployeeID
                                    && project.ProjectID == ProjectID
                                    && project.Year == Year
                                    select new YearBreakdownDTO
                                    {
                                        ProjectID = project.ProjectID,
                                        EmployeeID = project.EmployeeID,
                                        Year = project.Year,
                                        January = project.January,
                                        Febuary = project.February,
                                        March = project.March,
                                        April = project.April,
                                        May = project.May,
                                        June = project.June,
                                        July = project.July,
                                        August = project.August,
                                        September = project.September,
                                        October = project.October,
                                        November = project.November,
                                        December = project.December
                                    }).FirstOrDefault();

                return yearBreakdownDTO;
            }
        }

    }
}
