using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// Unit CRUD Controller
    /// </summary>
    /// 
    public class UnitController
    {
        AreaController areaController = new AreaController();

        /// <summary>
        /// Get all active Units
        /// </summary>
        /// <returns>Listing of all active Units</returns>
        public List<UnitDTO> LookupUnit()
        {
            using (var db = new CapstoneContext())
            {
                List<UnitDTO> allUnitDTOs = (from unit in db.TB_Capstone_UNIT
                                             where unit.DeactivationDate == null || unit.DeactivationDate > DateTime.UtcNow
                                             select new UnitDTO
                                             {
                                                 UnitID = unit.UnitID,
                                                 AreaID = unit.AreaID,
                                                 UnitName = unit.UnitName,
                                                 Description = unit.Description,
                                                 ActivationDate = unit.ActivationDate,
                                                 DeactivationDate = unit.DeactivationDate
                                             }).ToList();
                foreach (var unit in allUnitDTOs)
                {
                    unit.Area = areaController.LookupArea(unit.AreaID);
                }
                return allUnitDTOs;
            }
        }
        /// <summary>
        /// Get a single Unit
        /// </summary>
        /// <param name="id">TB_Capstone_UNIT Primary Key Value</param>
        /// <returns>Unit DTO object that matches the entered primary key</returns>
        public UnitDTO LookupUnit(int id)
        {
            using (var db = new CapstoneContext())
            {
                TB_Capstone_UNIT unitEntity = db.TB_Capstone_UNIT.Find(id);
                var unitDTO = new UnitDTO
                {
                    UnitID = unitEntity.UnitID,
                    UnitName = unitEntity.UnitName,
                    AreaID = unitEntity.AreaID,
                    Area = areaController.LookupArea(unitEntity.AreaID),
                    Description = unitEntity.Description,
                    ActivationDate = unitEntity.ActivationDate,
                    DeactivationDate = unitEntity.DeactivationDate
                };

                return unitDTO;
            }
        }

        /// <summary>
        /// Create a new entry in the TB_Capstone_UNIT table
        /// </summary>
        /// <param name="unitDTO"> new Unit information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateUnit(UnitDTO unitDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (unitDTO.DeactivationDate < unitDTO.ActivationDate)
            {
                errorList.Add(new Exception("Activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var db = new CapstoneContext())
            {
                var unitEntity = new TB_Capstone_UNIT
                {
                    UnitName = unitDTO.UnitName,
                    UnitID = unitDTO.UnitID,
                    AreaID = unitDTO.AreaID,
                    Description = unitDTO.Description,
                    ActivationDate = unitDTO.ActivationDate,
                    DeactivationDate = unitDTO.DeactivationDate,
                    CreatedBy = employeeID,
                    CreationDate = DateTime.Now
                };
                db.TB_Capstone_UNIT.Add(unitEntity);
                db.SaveChanges();

            }

        }

        /// <summary>
        /// Edit an existing entry in the TB_Capstone_UNIT table
        /// </summary>
        /// <param name="unitDTO">Edited Unit information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void EditUnit(UnitDTO unitDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (unitDTO.DeactivationDate < unitDTO.ActivationDate)
            {
                errorList.Add(new Exception("Activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var db = new CapstoneContext())
            {
                var unitEntity = db.TB_Capstone_UNIT.Find(unitDTO.UnitID);
                unitEntity.UnitName = unitDTO.UnitName;
                unitEntity.AreaID = unitDTO.AreaID;
                unitEntity.Description = unitDTO.Description;
                unitEntity.ActivationDate = unitDTO.ActivationDate;
                unitEntity.DeactivationDate = unitDTO.DeactivationDate;
                unitEntity.UpdatedBy = employeeID;
                unitEntity.UpdatedDate = DateTime.Now;

                db.Entry(unitEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Logical delete of a Unit, sets deactivation date to current date
        /// </summary>
        /// <param name="unitID">TB_Capstone_UNIT Primary Key Value for deactivated row</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void DeactivateUnit(int unitID, int employeeID)
        {
            using (var db = new CapstoneContext())
            {
                var unitEntity = db.TB_Capstone_UNIT.Find(unitID);
                unitEntity.DeactivationDate = DateTime.Now;
                unitEntity.UpdatedBy = employeeID;
                unitEntity.UpdatedDate = DateTime.Now;
                unitEntity.ActivationDate = unitEntity.ActivationDate > unitEntity.DeactivationDate ? DateTime.Now : unitEntity.ActivationDate;

                db.Entry(unitEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
