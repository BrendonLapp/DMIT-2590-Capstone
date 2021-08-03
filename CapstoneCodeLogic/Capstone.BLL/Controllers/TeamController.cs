using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Capstone.BLL.DTOs;
using Capstone.DAL.Context;
using Capstone.DAL.Entities;

namespace Capstone.BLL.Controllers
{
    /// <summary>
    /// Team CRUD Controller
    /// </summary>
    public class TeamController
    {
        UnitController unitController = new UnitController();

        /// <summary>
        /// Get all active Teams
        /// </summary>
        /// <returns>Listing of all active Teams</returns>
        public List<TeamDTO> LookupTeam()
        {
            using (var db = new CapstoneContext())
            {
                List<TeamDTO> allTeamDTOs = (from team in db.TB_Capstone_TEAM
                                             where team.DeactivationDate == null || team.DeactivationDate > DateTime.UtcNow
                                             select new TeamDTO
                                             {
                                                 TeamID = team.TeamID,
                                                 UnitID = team.UnitID,
                                                 TeamName = team.TeamName,
                                                 ActivationDate = team.ActivationDate,
                                                 DeactivationDate = team.DeactivationDate
                                             }).ToList();
                foreach (var team in allTeamDTOs)
                {
                    team.Unit = unitController.LookupUnit(team.UnitID);
                }
                return allTeamDTOs;
            }
        }
        /// <summary>
        /// Get a single Team
        /// </summary>
        /// <param name="id">TB_Capstone_TEAM Primary Key Value</param>
        /// <returns>Team DTO object that matches the entered primary key</returns>
        public TeamDTO LookupTeam(int id)
        {
            using (var db = new CapstoneContext())
            {
                TB_Capstone_TEAM teamEntity = db.TB_Capstone_TEAM.Find(id);
                var teamDTO = new TeamDTO
                {
                    TeamID = teamEntity.TeamID,
                    TeamName = teamEntity.TeamName,
                    UnitID = teamEntity.UnitID,
                    Unit = unitController.LookupUnit(teamEntity.UnitID),
                    ActivationDate = teamEntity.ActivationDate,
                    DeactivationDate = teamEntity.DeactivationDate,
                    UnitName = teamEntity.TB_Capstone_UNIT.UnitName
                };

                return teamDTO;
            }
        }


        /// <summary>
        /// Gets a list of all active teams from the database, while filtering out a single team
        /// </summary>
        /// <param name="teamID">Key Value for the TB_Capstone_TEAM table</param>
        /// <returns>List of active teams</returns>
        public List<KeyValueDTO> LookupTeamDropdownWithoutCurrentTeam(int teamID)
        {
            using (var context = new CapstoneContext())
            {
                var teamValues =
                    (from team in context.TB_Capstone_TEAM
                     where (team.DeactivationDate == null || team.DeactivationDate > DateTime.UtcNow)
                            && team.TeamID != teamID
                     select new KeyValueDTO
                     {
                         Key = team.TeamID,
                         Value = team.TeamName
                     }).ToList();

                return teamValues;
            }
        }

        /// <summary>
        /// Create a new entry in the TB_Capstone_TEAM table
        /// </summary>
        /// <param name="teamDTO"> new Team information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void CreateTeam(TeamDTO teamDTO, int employeeID)
        {
            var errorList = new List<Exception>();
            if (teamDTO.DeactivationDate < teamDTO.ActivationDate)
            {
                errorList.Add(new Exception("Activation date must come before the deactivation date."));
            }

            if (errorList.Count > 0)
            {
                throw new AggregateException("", errorList);
            }

            using (var scope = new TransactionScope())
            {
                using (var db = new CapstoneContext())
                {
                    var teamEntity = new TB_Capstone_TEAM
                    {
                        TeamName = teamDTO.TeamName,
                        UnitID = teamDTO.UnitID,
                        ActivationDate = teamDTO.ActivationDate,
                        DeactivationDate = teamDTO.DeactivationDate,
                        CreatedBy = employeeID,
                        CreationDate = DateTime.Now
                    };
                    db.TB_Capstone_TEAM.Add(teamEntity);
                    db.SaveChanges();
                    AddTeamMembers(teamDTO.NewTeamMembers, teamEntity.TeamID, employeeID);
                    db.SaveChanges();
                }
                scope.Complete();
            }

        }

        /// <summary>
        /// Edit an existing entry in the TB_Capstone_TEAM table
        /// </summary>
        /// <param name="teamDTO">Edited Team information</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void EditTeam(TeamDTO teamDTO, int employeeID)
        {
            using (var scope = new TransactionScope())
            {
                using (var db = new CapstoneContext())
                {
                    var errorList = new List<Exception>();
                    if (teamDTO.DeactivationDate < teamDTO.ActivationDate)
                    {
                        errorList.Add(new Exception("Activation date must come before the deactivation date."));
                    }

                    if (errorList.Count > 0)
                    {
                        throw new AggregateException("", errorList);
                    }
                    var teamEntity = db.TB_Capstone_TEAM.Find(teamDTO.TeamID);
                    teamEntity.TeamName = teamDTO.TeamName;
                    teamEntity.UnitID = teamDTO.UnitID;
                    teamEntity.ActivationDate = teamDTO.ActivationDate;
                    teamEntity.DeactivationDate = teamDTO.DeactivationDate;
                    teamEntity.UpdatedBy = employeeID;
                    teamEntity.UpdatedDate = DateTime.Now;

                    AddTeamMembers(teamDTO.NewTeamMembers, teamDTO.TeamID, employeeID);

                    db.Entry(teamEntity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                scope.Complete();
            }

        }
        /// <summary>
        /// Logical delete of a Team, sets deactivation date to current date
        /// </summary>
        /// <param name="teamID">TB_Capstone_TEAM Primary Key Value for deactivated row</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        public void DeactivateTeam(int teamID, int employeeID)
        {
            using (var db = new CapstoneContext())
            {
                var employeeList =
                    (from person in db.TB_Capstone_EMPLOYEE
                     join team in db.TB_Capstone_TEAM_HISTORY on person.EmployeeID equals team.EmployeeID
                     where (person.DeactivationDate == null || person.DeactivationDate > DateTime.Now) &&
                           (team.DeactivationDate == null || team.DeactivationDate > DateTime.Now) &&
                            team.ActivationDate <= DateTime.Now &&
                            team.TeamID == teamID
                     select new EmployeeWithForignKeyNamesDTO()
                     {
                         EmployeeID = person.EmployeeID,
                         PositionID = person.PositionID,
                         PositionTitle = person.TB_Capstone_POSITION.PositionTitle,
                         UserID = person.UserID,
                         FirstName = person.FirstName,
                         LastName = person.LastName,
                         PhoneNumber = person.PhoneNumber,
                         AlternatePhoneNumber = person.AlternatePhoneNumber,
                         StationNumber = person.StationNumber,
                         ComputerNumber = person.ComputerNumber,
                         CompanyPhoneNumber = person.CompanyPhoneNumber,
                         BirthDate = person.BirthDate,
                         //StartDate = person.StartDate,
                         ActivationDate = person.ActivationDate,
                         DeactivationDate = person.DeactivationDate,
                         EmergencyContactName1 = person.EmergencyContactName1,
                         EmergencyContactPhoneNumber1 = person.EmergencyContactPhoneNumber1,
                         EmergencyContactName2 = person.EmergencyContactName2,
                         EmergencyContactPhoneNumber2 = person.EmergencyContactPhoneNumber2,
                         TeamID = team.TeamID,
                         TeamName = team.TB_Capstone_TEAM.TeamName,
                         RoleID = team.RoleID,
                         RoleTitle = team.TB_Capstone_ROLE.RoleTitle
                     }).ToList();
                var errorList = new List<Exception>();
                if (employeeList != null && employeeList.Count > 0)
                {
                    errorList.Add(new Exception("Cannot deactivate team if there are currently any employees"));
                }
                if (errorList.Count > 0)
                {
                    throw new AggregateException("", errorList);
                }

                var teamEntity = db.TB_Capstone_TEAM.Find(teamID);
                teamEntity.DeactivationDate = DateTime.Now;
                teamEntity.UpdatedBy = employeeID;
                teamEntity.UpdatedDate = DateTime.Now;
                teamEntity.ActivationDate = teamEntity.ActivationDate > teamEntity.DeactivationDate ? DateTime.Now : teamEntity.ActivationDate;

                db.Entry(teamEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        /// <summary>
        /// Moves employees from one team to another by updated TB_Capstone_TEAM_HISTORY VALUES, and creating new entrys
        /// </summary>
        /// <param name="newMemebers">List of employeeID's and roles of new team members to add</param>
        /// <param name="teamID">KEY value of TB_Capstone_TEAM, that corrosoponds to the team that will have members added to it</param>
        /// <param name="employeeID">The userID of the currently logged in user</param>
        private void AddTeamMembers(List<UnsavedAssignmentDataClass> newMemebers, int teamID, int employeeID)
        {
            if (newMemebers != null)
            {
                using (var context = new CapstoneContext())
                {
                    foreach (var item in newMemebers)
                    {
                        var currentTeamHistory = new TB_Capstone_TEAM_HISTORY();
                        currentTeamHistory = context.TB_Capstone_TEAM_HISTORY.Where
                                                                    (
                                                                        x => x.EmployeeID == item.EmployeeID &&
                                                                        (x.DeactivationDate == null || x.DeactivationDate > DateTime.Now) &&
                                                                        x.ActivationDate <= DateTime.Now
                                                                    ).FirstOrDefault();
                        currentTeamHistory.DeactivationDate = DateTime.Now;
                        currentTeamHistory.UpdatedBy = employeeID;
                        currentTeamHistory.UpdatedDate = DateTime.Now;

                        var newTeamHistory = new TB_Capstone_TEAM_HISTORY
                        {
                            TeamID = teamID,
                            EmployeeID = item.EmployeeID,
                            RoleID = item.RoleID,
                            CreatedBy = employeeID,
                            CreationDate = DateTime.Now,
                            ActivationDate = DateTime.Now,

                        };

                        context.Entry(currentTeamHistory).State = EntityState.Modified;
                        context.TB_Capstone_TEAM_HISTORY.Add(newTeamHistory);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
