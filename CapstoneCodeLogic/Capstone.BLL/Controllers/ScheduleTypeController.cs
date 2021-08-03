using System;
using System.Collections.Generic;
using System.Linq;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL
{
    /// <summary>
    /// ScheduleType CRUD Controller
    /// </summary>
    public class ScheduleTypeController
    {
        /// <summary>
        /// Gets a list of Schedule Types
        /// </summary>
        /// <returns>A list of Schedule Types<returns>
        public List<ScheduleTypeDTO> LookupScheduleTypes()
        {
            using (var context = new CapstoneContext())
            {
                List<ScheduleTypeDTO> scheduleTypes = new List<ScheduleTypeDTO>();
                scheduleTypes = (from item in context.TB_Capstone_SCHEDULE_TYPE
                                 where (item.DeactivationDate == null || item.DeactivationDate > DateTime.Now)
                                        && item.ActivationDate <= DateTime.Now
                                 select new ScheduleTypeDTO
                                 {
                                     ScheduleTypeID = item.ScheduleTypeID,
                                     Name = item.Name,
                                     Description = item.Description,
                                     HoursPerDay = item.HoursPerDay,
                                     ActivationDate = item.ActivationDate,
                                     DeactivationDate = item.DeactivationDate
                                 }).ToList();

                return scheduleTypes;
            }
        }

        /// <summary>
        /// Gets a single Schedule Type
        /// </summary>
        /// <param name="scheduleTypeID">TB_Capstone_SCHEDULE_TYPE Primary Key value</param>
        /// <returns>Schedule Type that matches the entered primary key</returns>
        public ScheduleTypeDTO LookupScheduleType(int scheduleTypeID)
        {
            using (var context = new CapstoneContext())
            {
                ScheduleTypeDTO scheduleTypeDTO = new ScheduleTypeDTO();

                scheduleTypeDTO = (from item in context.TB_Capstone_SCHEDULE_TYPE
                                   where item.ScheduleTypeID == scheduleTypeID
                                   select new ScheduleTypeDTO
                                   {
                                       ScheduleTypeID = item.ScheduleTypeID,
                                       Name = item.Name,
                                       Description = item.Description,
                                       HoursPerDay = item.HoursPerDay,
                                       ActivationDate = item.ActivationDate,
                                       DeactivationDate = item.DeactivationDate
                                   }).FirstOrDefault();

                return scheduleTypeDTO;
            }
        }

        /// <summary>
        /// Creates a new entry in the TB_Capstone_SCHEDULE_TYPE table
        /// </summary>
        /// <param name="newScheduleTypeDTO">Information for a newly created Schedule Type</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateNewScheduleType(ScheduleTypeDTO newScheduleTypeDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var errorList = new List<Exception>();
                if (newScheduleTypeDTO.DeactivationDate < newScheduleTypeDTO.ActivationDate)
                {
                    errorList.Add(new Exception("The activation date must come before the deactivation date."));
                }

                if (newScheduleTypeDTO.HoursPerDay > 24)
                {
                    errorList.Add(new Exception("Schedule types cannot exceed 24 hours."));
                }

                if (newScheduleTypeDTO.HoursPerDay <= 0)
                {
                    errorList.Add(new Exception("Schedule types cannot be less than or equal to 0"));
                }

                if (errorList.Count > 0)
                {

                    throw new AggregateException("", errorList);
                }

                var newScheduleType = new TB_Capstone_SCHEDULE_TYPE();

                newScheduleType.Name = newScheduleTypeDTO.Name;
                newScheduleType.Description = newScheduleTypeDTO.Description;
                newScheduleType.HoursPerDay = newScheduleTypeDTO.HoursPerDay;
                newScheduleType.ActivationDate = newScheduleTypeDTO.ActivationDate;
                newScheduleType.DeactivationDate = newScheduleTypeDTO.DeactivationDate;
                newScheduleType.CreatedBy = employeeID;
                newScheduleType.CreationDate = System.DateTime.Now;
                newScheduleType.UpdatedBy = null;
                newScheduleType.UpdatedDate = null;
                context.TB_Capstone_SCHEDULE_TYPE.Add(newScheduleType);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Creates a new entry in the TB_Capstone_SCHEDULE_TYPE table
        /// </summary>
        /// <param name="scheduleTypeDTO">Information for the Schedule Type being edited</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        /// <returns>A single instance of a schedule type dto</returns>
        public ScheduleTypeDTO UpdateScheduleType(ScheduleTypeDTO scheduleTypeDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {

                var errorList = new List<Exception>();
                if (scheduleTypeDTO.DeactivationDate < scheduleTypeDTO.ActivationDate)
                {
                    errorList.Add(new Exception("The activation date must come before the deactivation date."));
                }

                if (scheduleTypeDTO.HoursPerDay > 24)
                {
                    errorList.Add(new Exception("Schedule types cannot exceed 24 hours."));
                }

                if (scheduleTypeDTO.HoursPerDay <= 0)
                {
                    errorList.Add(new Exception("Schedule types cannot be less than or equal to 0"));
                }

                if (errorList.Count > 0)
                {

                    throw new AggregateException("", errorList);
                }

                var updatedScheduleType = context.TB_Capstone_SCHEDULE_TYPE.Find(scheduleTypeDTO.ScheduleTypeID);

                updatedScheduleType.Name = scheduleTypeDTO.Name;
                updatedScheduleType.Description = scheduleTypeDTO.Description;
                updatedScheduleType.HoursPerDay = scheduleTypeDTO.HoursPerDay;
                updatedScheduleType.ActivationDate = scheduleTypeDTO.ActivationDate;
                updatedScheduleType.DeactivationDate = scheduleTypeDTO.DeactivationDate;
                updatedScheduleType.UpdatedBy = employeeID;
                updatedScheduleType.UpdatedDate = DateTime.Now;


                context.Entry(updatedScheduleType).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return scheduleTypeDTO;
            }
        }

        /// <summary>
        /// Deactivates the selected schedule type
        /// </summary>
        /// <param name="scheduleTypeDTO">Information from the ScheduleType Delete.cshtml form</param>
        /// <param name="employeeID">The logged in users ID</param>
        public void DeactivateScheduleType(ScheduleTypeDTO scheduleTypeDTO, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deactivateScheduleType = context.TB_Capstone_SCHEDULE_TYPE.Find(scheduleTypeDTO.ScheduleTypeID);

                deactivateScheduleType.DeactivationDate = System.DateTime.Now;
                deactivateScheduleType.UpdatedBy = employeeID;
                deactivateScheduleType.UpdatedDate = System.DateTime.Now;

                context.Entry(deactivateScheduleType).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
