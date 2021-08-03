using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// OffDay Type CRUD Controller
    /// </summary>
    public class OffDayTypeController
    {
        /// <summary>
        /// Gets a listing of all active OffDay Types
        /// </summary>
        /// <returns>A listing of all Types that do not have a deactivation date, or have not reached their deactivation date</returns>
        public List<OffDayTypeDTO> LookupOffDayType()
        {
            using (var context = new CapstoneContext())
            {
                var offDayTypes =
                    from types in context.TB_Capstone_OFFDAY_TYPE
                    where types.DeactivationDate == null || types.DeactivationDate > DateTime.Now
                    select new OffDayTypeDTO
                    {
                        OffDayID = types.OffDayID,
                        Name = types.Name,
                        AbbreviatedName = types.AbbreviatedName,
                        Description = types.Description,
                        PTO = types.PTO,
                        Notes = types.Notes,
                        ColorDisplayed = types.Color,
                        ActivationDate = types.ActivationDate,
                        DeactivationDate = types.DeactivationDate
                    };

                return offDayTypes.ToList();
            }
        }

        /// <summary>
        /// gets information from a single entry in the TB_Capstone_OFFDAY_TYPE table
        /// </summary>
        /// <param name="offDayID">TB_Capstone_OFFDAY_TYPE primary key value</param>
        /// <returns>OffDay Type that matches the primary Key</returns>
        public OffDayTypeDTO LookupOffDayType(int offDayID)
        {
            using (var context = new CapstoneContext())
            {
                var offDayType =
                    from type in context.TB_Capstone_OFFDAY_TYPE
                    where type.OffDayID == offDayID
                    select new OffDayTypeDTO
                    {
                        OffDayID = type.OffDayID,
                        Name = type.Name,
                        AbbreviatedName = type.AbbreviatedName,
                        Description = type.Description,
                        PTO = type.PTO,
                        Notes = type.Notes,
                        ColorDisplayed = type.Color,
                        ActivationDate = type.ActivationDate,
                        DeactivationDate = type.DeactivationDate
                    };

                return offDayType.FirstOrDefault();
            }
        }

        /// <summary>
        /// Creates a new entry in the TB_Capstone_OFFDAY_TYPE table
        /// </summary>
        /// <param name="newOffDayType">Information for a newly created Offday Type</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateOffDayType(OffDayTypeDTO newOffDayType, int employeeID)
        {
            var errorList = new List<Exception>();
            if (newOffDayType.DeactivationDate < newOffDayType.ActivationDate)
            {
                errorList.Add(new Exception("Activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var scope = new TransactionScope())
            {
                using (var context = new CapstoneContext())
                {
                    var newOffDayTypeEntry = new TB_Capstone_OFFDAY_TYPE();
                    newOffDayTypeEntry.AbbreviatedName = newOffDayType.AbbreviatedName;
                    newOffDayTypeEntry.Description = newOffDayType.Description;
                    newOffDayTypeEntry.PTO = newOffDayType.PTO;
                    newOffDayTypeEntry.Notes = newOffDayType.Notes;
                    newOffDayTypeEntry.Color = newOffDayType.ColorDisplayed;
                    newOffDayTypeEntry.ActivationDate = DateTime.Now;
                    newOffDayTypeEntry.CreatedBy = employeeID;
                    newOffDayTypeEntry.CreationDate = DateTime.Now;
                    newOffDayTypeEntry.Name = newOffDayType.AbbreviatedName;

                    context.TB_Capstone_OFFDAY_TYPE.Add(newOffDayTypeEntry);
                    context.SaveChanges();
                    var employeeController = new EmployeeController();
                    List<EmployeeWithForignKeyNamesDTO> employeeList = employeeController.LookupEmployee();
                    if (employeeList != null)
                    {
                        foreach (var item in employeeList)
                        {
                            var employeeEntitlement = new TB_Capstone_ENTITLED_TIME_OFF
                            {
                                EmployeeID = item.EmployeeID,
                                OffDayID = newOffDayTypeEntry.OffDayID,
                                HoursAccumulated = 0.0m,
                                ActivationDate = DateTime.Now,
                                CreatedBy = employeeID,
                                CreationDate = DateTime.Now
                            };
                            context.TB_Capstone_ENTITLED_TIME_OFF.Add(employeeEntitlement);
                        }
                        context.SaveChanges();
                    }
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// Edits an existing entry in the TB_Capstone_OFFDAY_TYPE table
        /// </summary>
        /// <param name="editedOffDayType">Information for the updated Off Day Type, including table key</param>
        /// <param name="employeeID">The user ID of the currently logged in user</param>
        public void EditOffDayType(OffDayTypeDTO editedOffDayType, int employeeID)
        {
            var errorList = new List<Exception>();
            if (editedOffDayType.DeactivationDate < editedOffDayType.ActivationDate)
            {
                errorList.Add(new Exception("Activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var modifiedOffDayType = context.TB_Capstone_OFFDAY_TYPE.Find(editedOffDayType.OffDayID);

                modifiedOffDayType.AbbreviatedName = editedOffDayType.AbbreviatedName;
                modifiedOffDayType.Description = editedOffDayType.Description;
                modifiedOffDayType.PTO = editedOffDayType.PTO;
                modifiedOffDayType.Notes = editedOffDayType.Notes;
                modifiedOffDayType.Color = editedOffDayType.ColorDisplayed;
                modifiedOffDayType.DeactivationDate = editedOffDayType.DeactivationDate;
                modifiedOffDayType.UpdatedBy = employeeID;
                modifiedOffDayType.UpdatedDate = DateTime.Now;
                modifiedOffDayType.Name = editedOffDayType.AbbreviatedName;

                context.Entry(modifiedOffDayType).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Sets the Deactivation date for an entry in the TB_Capstone_OFFDAY_TYPE table to the current date
        /// </summary>
        /// <param name="offDayID">TB_Capstone_OFFDAY_TYPE Primary Key value for deactivated row</param>
        /// <param name="employeeID">The user ID of the currently logged in user</param>
        public void DeactivateOffDayType(int offDayID, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deactivatedOffDayType = context.TB_Capstone_OFFDAY_TYPE.Find(offDayID);

                deactivatedOffDayType.DeactivationDate = DateTime.Now;
                deactivatedOffDayType.UpdatedBy = employeeID;
                deactivatedOffDayType.UpdatedDate = DateTime.Now;

                context.Entry(deactivatedOffDayType).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}