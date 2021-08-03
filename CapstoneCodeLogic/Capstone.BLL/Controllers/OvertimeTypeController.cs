using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// OvertimeType CRUD Controller
    /// </summary>
    /// <remarks>
    public class OvertimeTypeController
    {
        /// <summary>
        /// Gets a listing of all active overtime types
        /// </summary>
        /// <returns>A listing of all roles that either don't have a deactivation date, or have not reached their deactivation date</returns>
        public List<OvertimeTypeDTO> LookupOvertimeType()
        {
            using (var context = new CapstoneContext())
            {
                var overtimeTypeList =
                    from types in context.TB_Capstone_OVERTIME_TYPE
                    where types.DeactivationDate == null || types.DeactivationDate > DateTime.Now
                    select new OvertimeTypeDTO
                    {
                        OvertimeTypeID = types.OvertimeTypeID,
                        Description = types.Description,
                        Name = types.Name,
                        PayMultiplier = types.PayMultiplier,
                        ActivationDate = types.ActivationDate,
                        DeactivationDate = types.DeactivationDate
                    };

                return overtimeTypeList.ToList();
            }
        }

        /// <summary>
        /// Gets a single Overtime Type
        /// </summary>
        /// <param name="overtimeTypeID">TB_Capstone_OVERTIME_TYPE Primary Key value</param>
        /// <returns>Overtime type that matches the entered primary key</returns>
        public OvertimeTypeDTO LookupOvertimeType(int overtimeTypeID)
        {
            using (var context = new CapstoneContext())
            {
                var overtimeType =
                    from type in context.TB_Capstone_OVERTIME_TYPE
                    where type.OvertimeTypeID == overtimeTypeID
                    select new OvertimeTypeDTO
                    {
                        OvertimeTypeID = type.OvertimeTypeID,
                        Description = type.Description,
                        Name = type.Name,
                        PayMultiplier = type.PayMultiplier,
                        ActivationDate = type.ActivationDate,
                        DeactivationDate = type.DeactivationDate
                    };

                return overtimeType.FirstOrDefault();
            }
        }

        /// <summary>
        /// Creates a new entry in the TB_Capstone_OVERTIME_TYPE table
        /// </summary>
        /// <param name="newOvertimeType">Information for a newly created Overtime Type</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateOvertimeType(OvertimeTypeDTO newOvertimeType, int employeeID)
        {
            var errorList = new List<Exception>();
            if (newOvertimeType.DeactivationDate < newOvertimeType.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (newOvertimeType.PayMultiplier <= 0)
            {
                errorList.Add(new Exception("Pay multipliers must be greater than zero."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var newOvertimeTypeEntry = new TB_Capstone_OVERTIME_TYPE();
                newOvertimeTypeEntry.Name = newOvertimeType.Name;
                newOvertimeTypeEntry.Description = newOvertimeType.Description;
                newOvertimeTypeEntry.PayMultiplier = newOvertimeType.PayMultiplier;
                newOvertimeTypeEntry.ActivationDate = DateTime.Now;
                newOvertimeTypeEntry.CreatedBy = employeeID;
                newOvertimeTypeEntry.CreationDate = DateTime.Now;

                context.TB_Capstone_OVERTIME_TYPE.Add(newOvertimeTypeEntry);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Edits an existing entry in the TB_Capstone_OVERTIME_TYPE table
        /// </summary>
        /// <param name="editedOvertimeType">Information for the updated Overtime Type, including table Key</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void EditOvertimeType(OvertimeTypeDTO editedOvertimeType, int employeeID)
        {
            var errorList = new List<Exception>();
            if (editedOvertimeType.DeactivationDate < editedOvertimeType.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (editedOvertimeType.PayMultiplier <= 0)
            {
                errorList.Add(new Exception("Pay multipliers must be greater than zero."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                TB_Capstone_OVERTIME_TYPE modifiedOvertimeType = context.TB_Capstone_OVERTIME_TYPE.Find(editedOvertimeType.OvertimeTypeID);

                modifiedOvertimeType.Name = editedOvertimeType.Name;
                modifiedOvertimeType.Description = editedOvertimeType.Description;
                modifiedOvertimeType.PayMultiplier = editedOvertimeType.PayMultiplier;
                modifiedOvertimeType.UpdatedBy = employeeID;
                modifiedOvertimeType.UpdatedDate = DateTime.Now;

                context.Entry(modifiedOvertimeType).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// sets the Deactivated date in an entry of TB_Capstone_OVERTIME_TYPE to the current date
        /// </summary>
        /// <param name="overtimeTypeID">TB_Capstone_OVERTIME_TYPE Primary Key value for deactivated row</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void DeactivateOvertimeType(int overtimeTypeID, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                TB_Capstone_OVERTIME_TYPE deactivatedOvertimeType = context.TB_Capstone_OVERTIME_TYPE.Find(overtimeTypeID);

                deactivatedOvertimeType.DeactivationDate = DateTime.Now;
                deactivatedOvertimeType.UpdatedBy = employeeID;
                deactivatedOvertimeType.UpdatedDate = DateTime.Now;

                context.Entry(deactivatedOvertimeType).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
