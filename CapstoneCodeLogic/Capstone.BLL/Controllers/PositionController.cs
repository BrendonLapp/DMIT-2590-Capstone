using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    ///<summary>
    ///Controller class for positions
    ///</summary>
    public class PositionController
    {
        ///<summary>
        ///A lookup to find all positions
        /// </summary>
        public List<PositionDTO> LookUpPosition()
        {
            using (var db = new DAL.Context.CapstoneContext())
            {
                List<PositionDTO> positionList = (from position in db.TB_Capstone_POSITION
                                                  where position.DeactivationDate == null || position.DeactivationDate > DateTime.Now
                                                  select new PositionDTO
                                                  {
                                                      PositionID = position.PositionID,
                                                      PositionTitle = position.PositionTitle,
                                                      Description = position.Description,
                                                      ActivationDate = position.ActivationDate,
                                                      DeactivationDate = position.DeactivationDate
                                                  }).ToList();
                return positionList;
            }
        }

        ///<summary>
        ///Find a specific postition
        /// </summary>
        /// <param name="positionID"></param> The position id passed from the view
        public PositionEditDTO LookUpPosition(int positionID)
        {
            using (var context = new DAL.Context.CapstoneContext())
            {
                var currentPostion = context.TB_Capstone_POSITION.Find(positionID);
                var currentPostionDTO = new PositionEditDTO
                {
                    PositionID = currentPostion.PositionID,
                    PositionTitle = currentPostion.PositionTitle,
                    Description = currentPostion.Description,
                    ActivationDate = currentPostion.ActivationDate,
                    DeactivationDate = currentPostion.DeactivationDate,
                    CreationDate = currentPostion.CreationDate,
                    UpdateDate = currentPostion.UpdatedDate

                };

                return currentPostionDTO;


            }
        }

        ///<summary>
        ///Create a new position
        /// </summary>
        /// <param name="positionDTO"></param> an instance of a basic position dto
        /// <param name="employeeID"></param> the username of the currently logged in employee
        public void CreatePosition(PositionDTO positionDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (positionDTO.DeactivationDate < positionDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }
            using (var context = new DAL.Context.CapstoneContext())
            {
                var newPostion = new TB_Capstone_POSITION
                {
                    PositionTitle = positionDTO.PositionTitle,
                    Description = positionDTO.Description,
                    ActivationDate = positionDTO.ActivationDate,
                    DeactivationDate = positionDTO.DeactivationDate,
                    CreatedBy = employeeID,
                    CreationDate = DateTime.Now
                };
                context.TB_Capstone_POSITION.Add(newPostion);
                context.SaveChanges();
            }
        }

        ///<summary>
        ///Edit a position
        /// </summary>
        /// <param name="positionEditDTO"></param> an instance of the more complex position dto
        /// <param name="employeeID"></param> the username of the currently logged in employee
        public void EditPosition(PositionEditDTO positionEditDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (positionEditDTO.DeactivationDate < positionEditDTO.ActivationDate)
            {
                errorList.Add(new Exception("The activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var context = new CapstoneContext())
            {
                var editPosition = context.TB_Capstone_POSITION.Find(positionEditDTO.PositionID);
                editPosition.PositionTitle = positionEditDTO.PositionTitle;
                editPosition.Description = positionEditDTO.Description;
                editPosition.ActivationDate = positionEditDTO.ActivationDate;
                editPosition.DeactivationDate = positionEditDTO.DeactivationDate;
                editPosition.UpdatedBy = employeeID;
                editPosition.UpdatedDate = DateTime.Now;

                context.Entry(editPosition).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        ///<summary>
        ///Deactivates, instead of deleting, a given position record
        /// </summary>
        /// <param name="positionID"></param> the position id of the record we are going to deactivate
        /// <param name="employeeID"></param> the user id of the employee deactivating the position
        public void DeactivatePosition(int positionID, int employeeID)
        {
            using (var context = new CapstoneContext())
            {
                var deactivatedPosition = context.TB_Capstone_POSITION.Find(positionID);
                deactivatedPosition.DeactivationDate = DateTime.Now;
                deactivatedPosition.UpdatedBy = employeeID;
                deactivatedPosition.UpdatedDate = DateTime.Now;

                context.Entry(deactivatedPosition).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
