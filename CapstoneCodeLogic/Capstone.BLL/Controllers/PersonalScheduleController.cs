using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{    /// <summary>
     /// Personal Schedule Controller
     /// </summary>
    public class PersonalScheduleController
    {
        /// <summary>
        /// Get all Project Details for a specific employee
        /// </summary>
        /// <returns>Listing of project details of the employee</returns>
        /// <param name="employeeID">EmployeeID of employee that the project details are requested for</param>
        public List<ProjectDetailDTO> LookupProjectDetailsByEmployee(int employeeID)
        {
            using (var db = new CapstoneContext())
            {
                return (from detail in db.TB_Capstone_PROJECT_DETAIL
                        where detail.EmployeeID == employeeID && (detail.DeactivationDate > DateTime.Now || detail.DeactivationDate == null)
                        select new ProjectDetailDTO
                        {
                            ProjectDetailID = detail.ProjectDetailID,
                            EmployeeID = detail.EmployeeID,
                            StartTime = detail.StartTime,
                            EndTime = detail.EndTime,
                            Notes = detail.Notes,
                            ProjectID = detail.ProjectID,
                            ActivationDate = detail.ActivationDate,
                            DeactivationDate = detail.DeactivationDate
                        }).ToList();
            }
        }
        /// <summary>
        /// Get the projects that the employee is currently allocated too
        /// </summary>
        /// <param name="employeeID">The EmployeeID of the allocated employee</param>
        /// <returns>List of projectIDs</returns>
        public List<int> LookupAllocatedProjectsByEmployee(int employeeID)
        {
            using (var db = new CapstoneContext())
            {
                return db.TB_Capstone_ALLOCATION
                    .Where(allocation => (allocation.EmployeeID == employeeID && allocation.DeactivationDate == null) || (allocation.TB_Capstone_PROJECT.TB_Capstone_PROJECT_CATEGORY.Global == true) &&
                    (allocation.TB_Capstone_PROJECT.TB_Capstone_PROJECT_CATEGORY.DeactivationDate == null
                    || allocation.TB_Capstone_PROJECT.TB_Capstone_PROJECT_CATEGORY.DeactivationDate > DateTime.Now))
                    .Select(allocation => allocation.ProjectID).ToList();
            }
        }
        /// <summary>
        /// Get all the Project names that exist in the system
        /// </summary>
        /// <returns>Listing of key value pairs where the key is Project ID, and the value is Project Name</returns>
        public List<KeyValueDTO> LookupProjectsNames()
        {
            using (var db = new CapstoneContext())
            {
                return (from project in db.TB_Capstone_PROJECT
                        select new KeyValueDTO
                        {
                            Key = project.ProjectID,
                            Value = project.ProjectName
                        }).ToList();
            }
        }

        /// <summary>
        /// Get the colors associated with each project in the system
        /// </summary>
        /// <returns>Listing of key value pairs where the key is Project ID, and the value is Color</returns>
        public List<KeyValueDTO> LookupProjectColors()
        {
            using (var db = new CapstoneContext())
            {
                return (from project in db.TB_Capstone_PROJECT
                        select new KeyValueDTO
                        {
                            Key = project.ProjectID,
                            Value = project.TB_Capstone_PROJECT_CATEGORY.Color
                        }).ToList();
            }
        }
        /// <summary>
        /// Create a new entry in the TB_Capstone_PROJECT_DETAIL table
        /// </summary>
        /// <returns>True if the changes affected any records, False if no records were affected</returns>
        /// <param name="details">Information of the Project Detail to be created</param>
        /// <param name="createdByEmpID">The EmployeeID of the employee that is creating the new entry</param>
        public bool CreateTimesheetEntry(ProjectDetailDTO details, int createdByEmpID)
        {
            int rowsAffected = 0;
            using (var db = new CapstoneContext())
            {
                db.TB_Capstone_PROJECT_DETAIL.Add(new TB_Capstone_PROJECT_DETAIL
                {
                    ActivationDate = DateTime.Now,
                    CreatedBy = createdByEmpID,
                    CreationDate = DateTime.Now,
                    EmployeeID = details.EmployeeID,
                    EndTime = details.EndTime,
                    Notes = details.Notes,
                    ProjectID = details.ProjectID,
                    StartTime = details.StartTime

                });
                rowsAffected = db.SaveChanges();
            }
            if (rowsAffected > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Edit an existing entry in the TB_Capstone_PROJECT_DETAIL table
        /// </summary>
        /// <returns>True if the changes affected any records, False if no records were affected</returns>
        /// <param name="details">Edited Project Detail information</param>
        /// <param name="updatedByEmpID">The EmployeeID of the Employee making the edits</param>
        public bool EditTimesheetEntry(ProjectDetailDTO details, int updatedByEmpID)
        {
            int rowsAffected = 0;
            using (var db = new CapstoneContext())
            {
                TB_Capstone_PROJECT_DETAIL editDetail = db.TB_Capstone_PROJECT_DETAIL.Find(details.ProjectDetailID);
                editDetail.StartTime = details.StartTime;
                editDetail.EndTime = details.EndTime;
                editDetail.ProjectID = details.ProjectID;
                editDetail.Notes = details.Notes;
                editDetail.UpdatedBy = updatedByEmpID;
                editDetail.UpdatedDate = DateTime.Now;
                rowsAffected = db.SaveChanges();
            }
            if (rowsAffected > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Logical delete of a TB_Capstone_PROJECT_DETAIL entry, sets deactivation date to current date
        /// </summary>
        /// <returns>True if the changes affected any records, False if no records were affected</returns>
        /// <param name="timesheetID">TB_Capstone_PROJECT_DETAIL Primary Key Value for deactivated entry</param>
        /// <param name="updatedByEmpID">The employeeID of the employee that is making the delete</param>
        public bool DeleteTimesheetEntry(int timesheetID, int updatedByEmpID)
        {
            int rowsAffected = 0;
            using (var db = new CapstoneContext())
            {
                TB_Capstone_PROJECT_DETAIL editDetail = db.TB_Capstone_PROJECT_DETAIL.Find(timesheetID);
                editDetail.DeactivationDate = DateTime.Now;
                editDetail.UpdatedBy = updatedByEmpID;
                editDetail.UpdatedDate = DateTime.Now;
                rowsAffected = db.SaveChanges();
            }
            if (rowsAffected > 0)
                return true;
            else
                return false;
        }


    }
}
