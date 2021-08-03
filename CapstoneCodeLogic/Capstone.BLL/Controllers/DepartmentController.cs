using System;
using System.Collections.Generic;
using System.Linq;
using Capstone.DAL.Entities;
using Capstone.BLL.DTOs;
using System.Data.Entity;


namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// Department CRUD Controller
    /// </summary>
    /// <remarks>
    /// 
    public class DepartmentController
    {
        /// <summary>
        /// Get all active departments
        /// </summary>
        /// <returns>Listing of all active departments</returns>
        public List<DepartmentDTO> LookupDepartment()
        {
            using (var db = new DAL.Context.CapstoneContext())
            {
                List<TB_Capstone_DEPARTMENT> dptEntityList = db.TB_Capstone_DEPARTMENT.Where(d => d.DeactivationDate == null || d.DeactivationDate > DateTime.Now).ToList();
                var dptDTOList = new List<DepartmentDTO>();
                foreach (var entity in dptEntityList)
                {
                    dptDTOList.Add(new DepartmentDTO
                    {
                        DepartmentID = entity.DepartmentID,
                        DepartmentName = entity.DepartmentName,
                        Description = entity.Description,
                        ActivationDate = entity.ActivationDate,
                        DeactivationDate = entity.DeactivationDate
                    });
                }
                return dptDTOList;
            }
        }
        /// <summary>
        /// Get a single department
        /// </summary>
        /// <param name="id">TB_Capstone_DEPARTMENT Primary Key Value</param>
        /// <returns>Department DTO object that matches the entered primary key</returns>
        public DepartmentDTO LookupDepartment(int id)
        {
            using (var db = new DAL.Context.CapstoneContext())
            {
                TB_Capstone_DEPARTMENT dptEntity = db.TB_Capstone_DEPARTMENT.Find(id);
                var departmentDTO = new DepartmentDTO
                {
                    DepartmentID = dptEntity.DepartmentID,
                    DepartmentName = dptEntity.DepartmentName,
                    Description = dptEntity.Description,
                    ActivationDate = dptEntity.ActivationDate,
                    DeactivationDate = dptEntity.DeactivationDate
                };

                return departmentDTO;
            }
        }
        /// <summary>
        /// Create a new entry in the TB_Capstone_DEPARTMENT table
        /// </summary>
        /// <param name="departmentDTO"> new department information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateDepartment(DepartmentDTO departmentDTO, int employeeID)
        {
            var errorList = new List<Exception>();

            if (departmentDTO.DeactivationDate < departmentDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }


            using (var db = new DAL.Context.CapstoneContext())
            {
                if (departmentDTO.DeactivationDate < departmentDTO.ActivationDate)
                {
                    throw new Exception("The activation date must come before the deactivation date.");
                }
                var departmentEntity = new TB_Capstone_DEPARTMENT
                {
                    DepartmentName = departmentDTO.DepartmentName,
                    Description = departmentDTO.Description,
                    ActivationDate = departmentDTO.ActivationDate,
                    DeactivationDate = departmentDTO.DeactivationDate,
                    CreatedBy = employeeID,
                    CreationDate = DateTime.Now
                };

                db.TB_Capstone_DEPARTMENT.Add(departmentEntity);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Edit an existing entry in the TB_Capstone_DEPARTMENT table
        /// </summary>
        /// <param name="departmentDTO">Edited department information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void EditDepartment(DepartmentDTO departmentDTO, int employeeID)
        {
            var errorList = new List<Exception>();

            if (departmentDTO.DeactivationDate < departmentDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var db = new DAL.Context.CapstoneContext())
            {
                var deptEntity = db.TB_Capstone_DEPARTMENT.Find(departmentDTO.DepartmentID);
                deptEntity.DepartmentName = departmentDTO.DepartmentName;
                deptEntity.Description = departmentDTO.Description;
                deptEntity.ActivationDate = departmentDTO.ActivationDate;
                deptEntity.DeactivationDate = departmentDTO.DeactivationDate;
                deptEntity.UpdatedBy = employeeID;
                deptEntity.UpdatedDate = DateTime.Now;

                db.Entry(deptEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Logical delete of a department, sets deactivation date to current date
        /// </summary>
        /// <param name="departmentID">TB_Capstone_DEPARTMENT Primary Key Value for deactivated row</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void DeactivateDepartment(int departmentID, int employeeID)
        {
            using (var db = new DAL.Context.CapstoneContext())
            {
                var deptEntity = db.TB_Capstone_DEPARTMENT.Find(departmentID);
                deptEntity.DeactivationDate = DateTime.Now;
                deptEntity.UpdatedBy = employeeID;
                deptEntity.UpdatedDate = DateTime.Now;
                deptEntity.ActivationDate = deptEntity.ActivationDate > deptEntity.DeactivationDate ? DateTime.Now : deptEntity.ActivationDate;

                db.Entry(deptEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
