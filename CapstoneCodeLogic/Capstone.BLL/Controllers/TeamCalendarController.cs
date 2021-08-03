using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Capstone.BLL.DTOs;
using Capstone.BLL.DTOs.TeamCalendarDTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    ///<summary>
    ///Controller class for paid holidays
    /// </summary>
    public class TeamCalendarController
    {
        /// <summary>
        /// Creates a list of employees for a speific team and start date with their absences
        /// </summary>
        /// <param name="teamID">Team ID of the desired team</param>
        /// <param name="startDate">The date that you want to start the absences from</param>
        /// <returns></returns>
        public List<TeamCalendarAbsenceDetailDTO> LookUpTeamAbsences(int teamID, DateTime startDate)
        {
            using (var context = new CapstoneContext())
            {
                var currentEmployeeIDsList = (from history in context.TB_Capstone_TEAM_HISTORY
                                              where history.TeamID == teamID && history.DeactivationDate == null
                                              select history.EmployeeID).ToList();

                List<TeamCalendarAbsenceDetailDTO> teamCalendarAbsenceDetailDTOs = new List<TeamCalendarAbsenceDetailDTO>();
                foreach (var empl in currentEmployeeIDsList)
                {
                    var employeeAbsences = (from employee in context.TB_Capstone_EMPLOYEE
                                            where employee.EmployeeID == empl
                                            select new TeamCalendarAbsenceDetailDTO
                                            {
                                                TeamID = teamID,
                                                EmployeeID = employee.EmployeeID,
                                                FirstName = employee.FirstName,
                                                LastName = employee.LastName,
                                                Month = startDate.Month,
                                                Year = startDate.Year,
                                                Absences = (from singleAbsence in employee.TB_Capstone_ABSENCE_DETAIL
                                                            where singleAbsence.AbsenceDate.Month == startDate.Month && singleAbsence.AbsenceDate.Year == startDate.Year && singleAbsence.DeactivationDate == null
                                                            select new AbsencesDTO
                                                            {
                                                                AbsenceID = singleAbsence.AbsenceID,
                                                                OffDayID = singleAbsence.TB_Capstone_OFFDAY_TYPE.OffDayID,
                                                                OffDayAbbreviatedName = singleAbsence.TB_Capstone_OFFDAY_TYPE.AbbreviatedName,
                                                                HalfDay = singleAbsence.HalfDay,
                                                                AbsenceDate = singleAbsence.AbsenceDate,
                                                                Hours = singleAbsence.Hours,
                                                                Notes = singleAbsence.Notes,
                                                                Color = singleAbsence.TB_Capstone_OFFDAY_TYPE.Color,
                                                                ActivationDate = singleAbsence.ActivationDate,
                                                                DeactivationDate = singleAbsence.DeactivationDate
                                                            }).ToList()
                                            }).SingleOrDefault();
                    teamCalendarAbsenceDetailDTOs.Add(employeeAbsences);
                }
                return teamCalendarAbsenceDetailDTOs;
            }

        }

        /// <summary>
        /// Finds specific absence based on absence ID
        /// </summary>
        /// <param name="absenceID">The absence ID of the requested absence</param>
        /// <returns></returns>
        public AbsencesDTO LookUpAbsence(int absenceID)
        {
            var currentAbsence = new AbsencesDTO();
            using (var context = new CapstoneContext())
            {
                var absence = context.TB_Capstone_ABSENCE_DETAIL.Find(absenceID);
                currentAbsence.AbsenceDate = absence.AbsenceDate;
                currentAbsence.AbsenceID = absence.AbsenceID;
                currentAbsence.ActivationDate = absence.ActivationDate;
                currentAbsence.DeactivationDate = absence.DeactivationDate;
                currentAbsence.EmployeeID = absence.EmployeeID;
                currentAbsence.HalfDay = absence.HalfDay;
                currentAbsence.Hours = absence.Hours;
                currentAbsence.Notes = absence.Notes;
                currentAbsence.OffDayID = absence.OffDayID;
            }
            return currentAbsence;
        }

        /// <summary>
        /// Finds the specific entilted time off for the specifed employee
        /// </summary>
        /// <param name="offDayID">The off day ID of the time off desired</param>
        /// <param name="EmployeeID">The employee ID of the desired employee</param>
        /// <returns></returns>
        public EntiltedTimeOffDTO LookupEntiltedTimeOff(int offDayID, int EmployeeID)
        {
            using (var context = new CapstoneContext())
            {
                var currentEntiltedTimeOff = (from eTimeOff in context.TB_Capstone_ENTITLED_TIME_OFF
                                              where eTimeOff.OffDayID == offDayID && eTimeOff.EmployeeID == EmployeeID && eTimeOff.DeactivationDate == null
                                              select new EntiltedTimeOffDTO
                                              {
                                                  OffDayID = eTimeOff.OffDayID,
                                                  EmployeeID = eTimeOff.EmployeeID,
                                                  HoursAccumulated = eTimeOff.HoursAccumulated

                                              }).SingleOrDefault();
                return currentEntiltedTimeOff;
            }
        }

        /// <summary>
        /// Creates a new instance of an absence for the specified employee
        /// </summary>
        /// <param name="absencesDTO">A data transer object populated by the front end view model</param>
        /// <param name="employeeID">The ID of the currently logged in employee for reporting purpouses</param>
        public void CreateAbsence(AbsencesDTO absencesDTO, int employeeID)
        {
            var employeeController = new EmployeeController();
            var offDayController = new OffDayTypeController();
            var currentOffDay = offDayController.LookupOffDayType(absencesDTO.OffDayID);
            var currentEmployee = employeeController.LookupEmployee(absencesDTO.EmployeeID);
            var currentEntiltedTimeOff = LookupEntiltedTimeOff(absencesDTO.OffDayID, absencesDTO.EmployeeID);
            string hoursCheck = absencesDTO.Hours.ToString();
            bool correctValue = decimal.TryParse(hoursCheck, out decimal corrrectHours);
            decimal hoursAccumulated = 0;
            decimal totalDays = 0;
            if (currentEntiltedTimeOff == null)
            {
                hoursAccumulated = 0;
                totalDays = 0;
            }
            else
            {
                hoursAccumulated = currentEntiltedTimeOff.HoursAccumulated;
                if (hoursAccumulated == 0)
                {
                    totalDays = 0;
                }
                else
                {
                    totalDays = decimal.Round(hoursAccumulated / currentEmployee.ScheduleHours, 1);
                }
            }
            var errorList = new List<Exception>();
            if (absencesDTO.Hours > hoursAccumulated)
            {
                errorList.Add(new Exception(currentEmployee.FullName + " only has " + hoursAccumulated + " hours " + "(" + totalDays + " days)" + " for " + currentOffDay.Name + " absences. Not enough to cover " + absencesDTO.Hours + " hours."));
            }

            if (absencesDTO.HalfDay != null)
            {
                if (absencesDTO.HalfDay != "AM" && absencesDTO.HalfDay != "PM")
                {
                    errorList.Add(new Exception("Half day must be entered as 'AM' or 'PM'."));
                }

            }

            if (correctValue == false)
            {
                errorList.Add(new Exception("Hours must be number that is greater than 0."));
            }

            if (absencesDTO.Hours < 0 || absencesDTO.Hours > 24)
            {
                errorList.Add(new Exception("Hours must be greater than 0 and less than 24."));
            }

            if (absencesDTO.Notes != null && absencesDTO.Notes.Length > 100)
            {
                errorList.Add(new Exception("Notes cannot be longer than 100 characters."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var newTotalHoursTotal = hoursAccumulated - absencesDTO.Hours;
                var updatedEntitledTimeOff = context.TB_Capstone_ENTITLED_TIME_OFF.Find(currentEntiltedTimeOff.OffDayID, currentEntiltedTimeOff.EmployeeID);
                updatedEntitledTimeOff.HoursAccumulated = newTotalHoursTotal.Value;
                updatedEntitledTimeOff.UpdatedBy = employeeID;
                updatedEntitledTimeOff.UpdatedDate = DateTime.Now;
                context.Entry(updatedEntitledTimeOff).State = EntityState.Modified;
                var newAbsence = new TB_Capstone_ABSENCE_DETAIL
                {
                    OffDayID = absencesDTO.OffDayID,
                    EmployeeID = absencesDTO.EmployeeID,
                    HalfDay = absencesDTO.HalfDay,
                    AbsenceDate = absencesDTO.AbsenceDate,
                    Hours = absencesDTO.Hours,
                    Notes = absencesDTO.Notes,
                    ActivationDate = DateTime.Now,
                    CreatedBy = employeeID,
                    CreationDate = DateTime.Now
                };

                context.TB_Capstone_ABSENCE_DETAIL.Add(newAbsence);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Edits a current instance of an absence for the specified employee
        /// </summary>
        /// <param name="absenceDTO">A data transer object populated by the front end view model</param>
        /// <param name="employeeID">The ID of the currently logged in employee for reporting purpouses</param>
        public void EditAbsence(AbsencesDTO absenceDTO, int employeeID)
        {
            var employeeController = new EmployeeController();
            var offDayController = new OffDayTypeController();
            var currentOffDay = offDayController.LookupOffDayType(absenceDTO.OffDayID);
            var currentEmployee = employeeController.LookupEmployee(absenceDTO.EmployeeID);
            var currentEntiltedTimeOff = LookupEntiltedTimeOff(absenceDTO.OffDayID, absenceDTO.EmployeeID);
            decimal hoursAccumulated = 0;
            decimal totalDays = 0;
            string hoursCheck = absenceDTO.Hours.ToString();
            bool correctValue = decimal.TryParse(hoursCheck, out decimal corrrectHours);
            if (currentEntiltedTimeOff == null)
            {
                hoursAccumulated = 0;
                totalDays = 0;
            }
            else
            {
                hoursAccumulated = currentEntiltedTimeOff.HoursAccumulated;
                if (hoursAccumulated == 0)
                {
                    totalDays = 0;
                }
                else
                {
                    totalDays = decimal.Round(hoursAccumulated / currentEmployee.ScheduleHours, 1);
                }
            }

            var errorList = new List<Exception>();
            if (absenceDTO.Hours > hoursAccumulated)
            {
                errorList.Add(new Exception(currentEmployee.FullName + " only has " + hoursAccumulated + " hours " + "(" + totalDays + " days)" + " for " + currentOffDay.Name + " absences. Not enough to cover " + absenceDTO.Hours + " hours."));
            }

            if (absenceDTO.HalfDay != null)
            {
                if (absenceDTO.HalfDay != "AM" && absenceDTO.HalfDay != "PM")
                {
                    errorList.Add(new Exception("Half day must be entered as 'AM' or 'PM'"));
                }
            }

            if (correctValue == false)
            {
                errorList.Add(new Exception("Hours must be number that is greater than 0."));
            }

            if (absenceDTO.Hours < 0 || absenceDTO.Hours > 24)
            {
                errorList.Add(new Exception("Hours must be greater than 0 and less than 24."));
            }

            if (absenceDTO.Notes != null && absenceDTO.Notes.Length > 100)
            {
                errorList.Add(new Exception("Notes cannot be longer than 100 characters."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var currentAbsence = context.TB_Capstone_ABSENCE_DETAIL.Find(absenceDTO.AbsenceID);
                if (currentAbsence.OffDayID != absenceDTO.OffDayID)
                {
                    var increasedEntitledTimeOff = context.TB_Capstone_ENTITLED_TIME_OFF.Find(currentEntiltedTimeOff.OffDayID, currentEntiltedTimeOff.EmployeeID);
                    var decreasedEntitltedTimeOff = context.TB_Capstone_ENTITLED_TIME_OFF.Find(absenceDTO.OffDayID, absenceDTO.EmployeeID);
                    increasedEntitledTimeOff.HoursAccumulated = increasedEntitledTimeOff.HoursAccumulated + absenceDTO.Hours.Value;
                    decreasedEntitltedTimeOff.HoursAccumulated = decreasedEntitltedTimeOff.HoursAccumulated - absenceDTO.Hours.Value;
                    context.Entry(increasedEntitledTimeOff).State = EntityState.Modified;
                    context.Entry(decreasedEntitltedTimeOff).State = EntityState.Modified;
                }

                if (currentAbsence.Hours > absenceDTO.Hours)
                {
                    decimal increasedHours = currentAbsence.Hours.Value - absenceDTO.Hours.Value;
                    var increasedEntitledTimeOff = context.TB_Capstone_ENTITLED_TIME_OFF.Find(currentEntiltedTimeOff.OffDayID, currentEntiltedTimeOff.EmployeeID);
                    increasedEntitledTimeOff.HoursAccumulated = increasedEntitledTimeOff.HoursAccumulated + increasedHours;
                    increasedEntitledTimeOff.UpdatedBy = employeeID;
                    increasedEntitledTimeOff.UpdatedDate = DateTime.Now;
                    context.Entry(increasedEntitledTimeOff).State = EntityState.Modified;
                }
                currentAbsence.OffDayID = absenceDTO.OffDayID;
                currentAbsence.HalfDay = absenceDTO.HalfDay;
                currentAbsence.Hours = absenceDTO.Hours;
                currentAbsence.Notes = absenceDTO.Notes;
                currentAbsence.UpdatedBy = employeeID;
                currentAbsence.UpdatedDate = DateTime.Now;

                context.Entry(currentAbsence).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deactivates and instance of an absence for the specified employee
        /// </summary>
        /// <param name="absenceDTO">A data transer object populated by the front end view model</param>
        /// <param name="employeeID">The ID of the currently logged in employee for reporting purpouses</param>
        public void DeactivateAbsence(AbsencesDTO absenceDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (absenceDTO.AbsenceID == 0)
            {
                errorList.Add(new Exception("There is no absence to deactivate. Please try again."));
            }
            using (var context = new CapstoneContext())
            {
                var currentAbsence = context.TB_Capstone_ABSENCE_DETAIL.Find(absenceDTO.AbsenceID);
                currentAbsence.UpdatedBy = employeeID;
                currentAbsence.UpdatedDate = DateTime.Now;
                currentAbsence.DeactivationDate = DateTime.Now;

                var updatedEntiltedTimeOff = context.TB_Capstone_ENTITLED_TIME_OFF.Find(currentAbsence.OffDayID, currentAbsence.EmployeeID);
                updatedEntiltedTimeOff.HoursAccumulated = updatedEntiltedTimeOff.HoursAccumulated + currentAbsence.Hours.Value;
                updatedEntiltedTimeOff.UpdatedBy = employeeID;
                updatedEntiltedTimeOff.UpdatedDate = DateTime.Now;
                context.Entry(updatedEntiltedTimeOff).State = EntityState.Modified;
                context.Entry(currentAbsence).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }

}
