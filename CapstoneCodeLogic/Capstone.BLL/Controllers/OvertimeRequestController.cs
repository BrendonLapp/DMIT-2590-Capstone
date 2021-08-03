using System;
using System.Collections.Generic;
using System.Linq;
using Capstone.BLL.DTOs.OvertimeRequestDTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;


namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// Overtime Requests CRUD Controller
    /// </summary>
    /// <remarks>
    public class OvertimeRequestController
    {
        /// <summary>
        /// Gets a list of all overtime requests based on a team
        /// </summary>
        /// <returns>A list of overtime requests based on a team ID as teamOvertimeRequests</returns>
        /// <param name="teamID">TB_Capstone_TEAMs team ID to filter the linq queries results with</param>
        public List<TeamOvertimeRequestDTO> LookupOvertimeByTeam(int teamID)
        {
            using (var context = new CapstoneContext())
            {
                List<EmployeeList> employeeLists = new List<EmployeeList>();

                List<TeamOvertimeRequestDTO> teamOvertimeRequests = new List<TeamOvertimeRequestDTO>();

                teamOvertimeRequests =
                                (from o in context.TB_Capstone_OVERTIME
                                 join e in context.TB_Capstone_EMPLOYEE on o.EmployeeID equals e.EmployeeID
                                 join t in context.TB_Capstone_TEAM_HISTORY on e.EmployeeID equals t.EmployeeID
                                 where t.TeamID == teamID
                                 && t.DeactivationDate == null
                                 && o.Approved != "A"
                                 && o.Approved != "D"
                                 select new TeamOvertimeRequestDTO
                                 {
                                     OvertimeID = o.OvertimeID,
                                     EmployeeID = o.EmployeeID,
                                     EmployeeName = e.FirstName + " " + e.LastName,
                                     //ProjectDetailID = o.ProjectDetailID,
                                     ProjectID = o.ProjectID.Value,
                                     ProjectName = (from n in context.TB_Capstone_PROJECT
                                                    where n.ProjectID == n.ProjectID
                                                    select n.ProjectName).FirstOrDefault(),
                                     OvertimeTypeID = o.OvertimeTypeID,
                                     OvertimeTypeName = (from n in context.TB_Capstone_OVERTIME_TYPE
                                                         where n.OvertimeTypeID == o.OvertimeTypeID
                                                         select n.Name).FirstOrDefault(),
                                     SubmissionDate = o.SubmissionDate,
                                     ReviewDate = o.ReviewDate,
                                     Amount = o.Amount / 60,
                                     StartTime = o.StartTime,
                                     EndTime = o.EndTime,
                                     SubmissionNotes = o.SubmissionNotes,
                                     ApprovalNotes = o.ApprovalNotes,
                                     Approved = o.Approved
                                 }).ToList();


                return teamOvertimeRequests;
            }
        }

        /// <summary>
        /// Create a new entry in the TB_Capstone_OVERTIME table
        /// </summary>
        /// <returns>True if the changes affected any records, False if no records were affected</returns>
        /// <param name="overtimeRequestDTO"> Overtime Request information</param>
        /// <param name="createdByEmployeeID">The employeeID of the currently logged in user</param>
        public bool CreateOvertimeRequest(TeamOvertimeRequestDTO overtimeRequestDTO, int createdByEmployeeID)
        {
            int rowsAffected = 0;
            using (var db = new CapstoneContext())
            {
                var overtimeRequest = new TB_Capstone_OVERTIME
                {
                    EmployeeID = overtimeRequestDTO.EmployeeID,
                    ProjectID = overtimeRequestDTO.ProjectID,
                    OvertimeTypeID = overtimeRequestDTO.OvertimeTypeID,
                    SubmissionDate = DateTime.Now,
                    Amount = Convert.ToDecimal((overtimeRequestDTO.EndTime.Subtract(overtimeRequestDTO.StartTime)).TotalMinutes),
                    StartTime = overtimeRequestDTO.StartTime,
                    EndTime = overtimeRequestDTO.EndTime,
                    SubmissionNotes = overtimeRequestDTO.SubmissionNotes,
                    Approved = overtimeRequestDTO.Approved,
                    ActivationDate = DateTime.Now,
                    CreatedBy = createdByEmployeeID,
                    CreationDate = DateTime.Now
                };
                db.TB_Capstone_OVERTIME.Add(overtimeRequest);
                rowsAffected = db.SaveChanges();
            }
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Approves an overtime request that was submitted by updating the existing record in TB_Capstone_OVERTIME
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="overTimeID">The key for the TB_Capstone_OVERTIME request to be approved</param>
        /// <param name="submissionMessage">The submission message to go along with the approval</param>
        /// <param name="EmployeeID">The key for the employee currently logged in</param>
        public void Approve(int overTimeID, string submissionMessage, int EmployeeID)
        {
            using (var context = new CapstoneContext())
            {
                var approvedOvertime = context.TB_Capstone_OVERTIME.Find(overTimeID);
                var errorList = new List<Exception>();

                if (approvedOvertime.EmployeeID == EmployeeID)
                {
                    errorList.Add(new Exception($"You may not approve your own overtime requests."));
                }
                if (errorList.Count > 0)
                {
                    throw new AggregateException("", errorList);
                }

                approvedOvertime.Approved = "A";
                approvedOvertime.ApprovalNotes = submissionMessage;
                approvedOvertime.UpdatedBy = EmployeeID;
                approvedOvertime.UpdatedDate = DateTime.Now;

                context.Entry(approvedOvertime).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Denies an overtime request that was submitted by updating the existing record in TB_Capstone_OVERTIME
        /// </summary>
        /// <returns>Returns nothing</returns>
        /// <param name="overTimeID">The key for the TB_Capstone_OVERTIME request to be denied</param>
        /// <param name="submissionMessage">The submission message to go along with the approval</param>
        /// <param name="EmployeeID">The key for the employee currently logged in</param>
        public void Deny(int overTimeID, string submissionMessage, int EmployeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deniedOvertime = context.TB_Capstone_OVERTIME.Find(overTimeID);

                if (deniedOvertime.EmployeeID == EmployeeID)
                {
                    //tell them no
                }


                deniedOvertime.Approved = "D";
                deniedOvertime.ApprovalNotes = submissionMessage;
                deniedOvertime.UpdatedBy = EmployeeID;
                deniedOvertime.UpdatedDate = DateTime.Now;

                context.Entry(deniedOvertime).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets a list of all overtime requests based on the logged in employee
        /// </summary>
        /// <returns>A list of overtime requests based on a employee ID as employeeOvertimeRequests</returns>
        /// <param name="employeeID">TB_Capstone_EMPLOYEEs employee ID to filter the linq queries results with</param>
        public List<TeamOvertimeRequestDTO> LookupOvertimeByEmployee(int employeeID)
        {
            using (var context = new CapstoneContext())
            {

                List<TeamOvertimeRequestDTO> employeeOvertimeRequests = new List<TeamOvertimeRequestDTO>();

                employeeOvertimeRequests = (from o in context.TB_Capstone_OVERTIME
                                            join e in context.TB_Capstone_EMPLOYEE on o.EmployeeID equals e.EmployeeID
                                            where e.EmployeeID == employeeID
                                            select new TeamOvertimeRequestDTO
                                            {
                                                OvertimeID = o.OvertimeID,
                                                EmployeeID = o.EmployeeID,
                                                EmployeeName = e.FirstName + " " + e.LastName,
                                                ProjectID = o.ProjectID.Value,
                                                ProjectName = (from n in context.TB_Capstone_PROJECT
                                                               where n.ProjectID == n.ProjectID
                                                               select n.ProjectName).FirstOrDefault(),
                                                OvertimeTypeID = o.OvertimeTypeID,
                                                OvertimeTypeName = (from n in context.TB_Capstone_OVERTIME_TYPE
                                                                    where n.OvertimeTypeID == o.OvertimeTypeID
                                                                    select n.Name).FirstOrDefault(),
                                                SubmissionDate = o.SubmissionDate,
                                                ReviewDate = o.ReviewDate,
                                                Amount = o.Amount / 60,
                                                StartTime = o.StartTime,
                                                EndTime = o.EndTime,
                                                SubmissionNotes = o.SubmissionNotes,
                                                ApprovalNotes = o.ApprovalNotes,
                                                Approved = o.Approved
                                            }).ToList();


                return employeeOvertimeRequests;
            }
        }

    }
}
