using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    public class PaidHolidayController
    {
        ///<summary>
        ///Controller class for paid holidays
        /// </summary>

        ///<summary>
        ///Looks up all paid holidays
        /// </summary>
        public List<PaidHolidayDTO> LookUpPaidHoliday()
        {
            using (var context = new CapstoneContext())
            {
                List<PaidHolidayDTO> paidHolidays = (from holiday in context.TB_Capstone_PAID_HOLIDAY
                                                     where holiday.DeactivationDate == null || holiday.DeactivationDate > DateTime.Now
                                                     select new PaidHolidayDTO
                                                     {
                                                         PaidHolidayID = holiday.PaidHolidayID,
                                                         HolidayName = holiday.HolidayName,
                                                         HolidayDate = holiday.HolidayDate,
                                                         ActivationDate = holiday.ActivationDate,
                                                         DeactivationDate = holiday.DeactivationDate
                                                     }).ToList();
                return paidHolidays;
            }
        }

        ///<summary>
        ///Find a specific paid holiday
        /// </summary>
        /// <param name="paidHolidayID"></param> The paid holiday id passed from the view
        public PaidHolidayEditDTO LookUpPaidHoliday(int paidHolidayID)
        {
            using (var context = new CapstoneContext())
            {
                var currentPaidHoliday = context.TB_Capstone_PAID_HOLIDAY.Find(paidHolidayID);
                var currentPaidHolidayEditDTO = new PaidHolidayEditDTO
                {
                    PaidHolidayID = currentPaidHoliday.PaidHolidayID,
                    HolidayName = currentPaidHoliday.HolidayName,
                    HolidayDate = currentPaidHoliday.HolidayDate,
                    Notes = currentPaidHoliday.Notes,
                    ActivationDate = currentPaidHoliday.ActivationDate,
                    DeactivationDate = currentPaidHoliday.DeactivationDate,
                    CreationDate = currentPaidHoliday.CreationDate,
                    UpdatedDate = currentPaidHoliday.UpdatedDate
                };

                return currentPaidHolidayEditDTO;

            }
        }
        ///<summary>
        ///Create a new paidholiday
        /// </summary>
        /// <param name="paidHolidayDTO"></param> an instance of a basic position dto
        /// <param name="employeeID"></param> the username of the currently logged in employee
        public void CreatePaidHoliday(PaidHolidayDTO paidHolidayDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (paidHolidayDTO.DeactivationDate < paidHolidayDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var newPaidHoliday = new TB_Capstone_PAID_HOLIDAY
                {
                    HolidayName = paidHolidayDTO.HolidayName,
                    HolidayDate = paidHolidayDTO.HolidayDate,
                    ActivationDate = paidHolidayDTO.ActivationDate,
                    DeactivationDate = paidHolidayDTO.DeactivationDate,
                    CreatedBy = employeeID,
                    CreationDate = DateTime.Now
                };

                context.TB_Capstone_PAID_HOLIDAY.Add(newPaidHoliday);
                context.SaveChanges();
            }
        }


        ///<summary>
        ///Edit a position
        /// </summary>
        /// <param name="paidHolidayEditDTO"></param> an instance of the more complex paidholiday dto
        /// <param name="employeeID"></param> the username of the currently logged in employee
        public void EditPaidHoliday(PaidHolidayEditDTO paidHolidayEditDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (paidHolidayEditDTO.DeactivationDate < paidHolidayEditDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var editPaidHoliday = context.TB_Capstone_PAID_HOLIDAY.Find(paidHolidayEditDTO.PaidHolidayID);
                editPaidHoliday.HolidayName = paidHolidayEditDTO.HolidayName;
                editPaidHoliday.HolidayDate = paidHolidayEditDTO.HolidayDate;
                editPaidHoliday.Notes = paidHolidayEditDTO.Notes;
                editPaidHoliday.ActivationDate = paidHolidayEditDTO.ActivationDate;
                editPaidHoliday.DeactivationDate = paidHolidayEditDTO.DeactivationDate;
                editPaidHoliday.UpdatedBy = employeeID;
                editPaidHoliday.UpdatedDate = DateTime.Now;

                context.Entry(editPaidHoliday).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        ///<summary>
        ///Deactivates, instead of deleting, a given paidholiday record
        /// </summary>
        /// <param name="paidHolidayID"></param> the position id of the record we are going to deactivate
        /// <param name="employeeID"></param> the user id of the employee deactivating the position
        public void DeactivatePaidHoliday(int paidHolidayID, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deactivatedPaidHoliday = context.TB_Capstone_PAID_HOLIDAY.Find(paidHolidayID);
                deactivatedPaidHoliday.DeactivationDate = DateTime.Now;
                deactivatedPaidHoliday.UpdatedBy = employeeID;
                deactivatedPaidHoliday.UpdatedDate = DateTime.Now;

                context.Entry(deactivatedPaidHoliday).State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
