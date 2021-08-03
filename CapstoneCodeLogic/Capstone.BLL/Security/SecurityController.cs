using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Capstone.BLL.Exceptions;
using Capstone.DAL.Context;

/// <summary>
/// Controller for use with Windows Authentication based Authorization
/// </summary>
namespace Capstone.BLL.Security
{
    public class SecurityController
    {
        /// <summary>
        /// Gets LoggedInUser information
        /// </summary>
        /// <returns>Single User security information</returns>
        public SecurityDTO LookupSecurityInformation()
        {
            string userID = Thread.CurrentPrincipal?.Identity?.Name;
            userID = userID.Substring(userID.IndexOf('\\') + 1);
            using (var context = new CapstoneContext())
            {
                //var securityInformation = new SecurityDTO();

                var securityInformation =
                    (from person in context.TB_Capstone_EMPLOYEE
                     let team =
                         ((from history in person.TB_Capstone_TEAM_HISTORY
                           where (history.DeactivationDate == null || history.DeactivationDate > DateTime.Now) &&
                                 history.ActivationDate <= DateTime.Now
                           select new
                           {
                               history.TeamID,
                               history.TB_Capstone_TEAM.TeamName,
                               history.RoleID,
                               history.TB_Capstone_ROLE.RoleTitle
                           }).FirstOrDefault())
                     where person.UserID == userID
                     select new SecurityDTO
                     {
                         EmployeeID = person.EmployeeID,
                         EmployeeName = person.FirstName + " " + person.LastName,
                         TeamID = team.TeamID,
                         TeamName = team.TeamName,
                         RoleID = team.RoleID,
                         RoleName = team.RoleTitle,
                         UserID = person.UserID
                     }).SingleOrDefault();
                if (securityInformation == null || securityInformation.RoleID == null || securityInformation.TeamID == null)
                {
                    throw new UserNotFoundException();
                }
                return securityInformation;
            }
        }
    }
}
