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
    /// Area CRUD Controller
    /// </summary>
    /// 
    public class AreaController
    {
        DepartmentController deptController = new DepartmentController();

        /// <summary>
        /// Get all active Areas
        /// </summary>
        /// <returns>Listing of all active Areas</returns>
        public List<AreaDTO> LookupArea()
        {
            using (var db = new CapstoneContext())
            {
                List<AreaDTO> allAreaDTOs = (from area in db.TB_Capstone_AREA
                                             where area.DeactivationDate == null || area.DeactivationDate > DateTime.UtcNow
                                             select new AreaDTO
                                             {
                                                 AreaID = area.AreaID,
                                                 DepartmentID = area.DepartmentID,
                                                 AreaName = area.AreaName,
                                                 Description = area.Description,
                                                 ActivationDate = area.ActivationDate,
                                                 DeactivationDate = area.DeactivationDate
                                             }).ToList();
                foreach (var area in allAreaDTOs)
                {
                    area.Department = deptController.LookupDepartment(area.DepartmentID);
                }
                return allAreaDTOs;
            }
        }
        /// <summary>
        /// Get a single Area
        /// </summary>
        /// <param name="id">TB_Capstone_AREA Primary Key Value</param>
        /// <returns>Area DTO object that matches the entered primary key</returns>
        public AreaDTO LookupArea(int id)
        {
            using (var db = new CapstoneContext())
            {
                TB_Capstone_AREA areaEntity = db.TB_Capstone_AREA.Find(id);
                var areaDTO = new AreaDTO
                {
                    AreaID = areaEntity.AreaID,
                    AreaName = areaEntity.AreaName,
                    DepartmentID = areaEntity.DepartmentID,
                    Department = deptController.LookupDepartment(areaEntity.DepartmentID),
                    DepartmentName = deptController.LookupDepartment(areaEntity.DepartmentID).DepartmentName,
                    Description = areaEntity.Description,
                    ActivationDate = areaEntity.ActivationDate,
                    DeactivationDate = areaEntity.DeactivationDate
                };

                return areaDTO;
            }
        }

        public List<AreaDTO> LookupAreasByDepartment(int departmentID)
        {
            if (departmentID == 0)
                return LookupArea();
            else
            {
                using (var db = new CapstoneContext())
                {
                    List<AreaDTO> allAreaDTOs = (from area in db.TB_Capstone_AREA
                                                 where area.DepartmentID == departmentID && (area.DeactivationDate == null || area.DeactivationDate > DateTime.UtcNow)
                                                 select new AreaDTO
                                                 {
                                                     AreaID = area.AreaID,
                                                     DepartmentID = area.DepartmentID,
                                                     AreaName = area.AreaName,
                                                     Description = area.Description,
                                                     ActivationDate = area.ActivationDate,
                                                     DeactivationDate = area.DeactivationDate
                                                 }).ToList();
                    foreach (var area in allAreaDTOs)
                    {
                        area.Department = deptController.LookupDepartment(area.DepartmentID);
                    }
                    return allAreaDTOs;
                }
            }
        }


        /// <summary>
        /// Create a new entry in the TB_Capstone_AREA table
        /// </summary>
        /// <param name="areaDTO"> new Area information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateArea(AreaDTO areaDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (areaDTO.DeactivationDate < areaDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var db = new CapstoneContext())
            {
                var areaEntity = new TB_Capstone_AREA
                {
                    AreaName = areaDTO.AreaName,
                    AreaID = areaDTO.AreaID,
                    DepartmentID = areaDTO.DepartmentID,
                    Description = areaDTO.Description,
                    ActivationDate = areaDTO.ActivationDate,
                    DeactivationDate = areaDTO.DeactivationDate,
                    CreatedBy = employeeID,
                    CreationDate = DateTime.Now
                };
                db.TB_Capstone_AREA.Add(areaEntity);
                db.SaveChanges();

            }
        }

        /// <summary>
        /// Edit an existing entry in the TB_Capstone_AREA table
        /// </summary>
        /// <param name="areaDTO">Edited Area information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void EditArea(AreaDTO areaDTO, int employeeID)
        {
            var errorList = new List<Exception>();


            if (areaDTO.DeactivationDate < areaDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var db = new CapstoneContext())
            {
                var areaEntity = db.TB_Capstone_AREA.Find(areaDTO.AreaID);
                areaEntity.AreaName = areaDTO.AreaName;
                areaEntity.DepartmentID = areaDTO.DepartmentID;
                areaEntity.Description = areaDTO.Description;
                areaEntity.ActivationDate = areaDTO.ActivationDate;
                areaEntity.DeactivationDate = areaDTO.DeactivationDate;
                areaEntity.UpdatedBy = employeeID;
                areaEntity.UpdatedDate = DateTime.Now;

                db.Entry(areaEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Logical delete of an Area, sets deactivation date to current date
        /// </summary>
        /// <param name="areaID">TB_Capstone_AREA Primary Key Value for deactivated row</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void DeactivateArea(int areaID, int employeeID)
        {
            using (var db = new CapstoneContext())
            {
                var areaEntity = db.TB_Capstone_AREA.Find(areaID);
                areaEntity.DeactivationDate = DateTime.Now;
                areaEntity.UpdatedBy = employeeID;
                areaEntity.UpdatedDate = DateTime.Now;
                areaEntity.ActivationDate = areaEntity.ActivationDate > areaEntity.DeactivationDate ? DateTime.Now : areaEntity.ActivationDate;

                db.Entry(areaEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
