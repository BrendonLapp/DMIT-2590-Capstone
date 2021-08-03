using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for entries in the TB_Capstone_EMPLOYEE table, along with related tables
    /// </summary>
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }

        public int TeamID { get; set; }

        public int RoleID { get; set; }

        public int PositionID { get; set; }

        public string UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public int? StationNumber { get; set; }

        public int? ComputerNumber { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int ScheduleTypeID { get; set; }

        //public DateTime StartDate { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }

        public string EmergencyContactName1 { get; set; }

        public string EmergencyContactPhoneNumber1 { get; set; }

        public string EmergencyContactName2 { get; set; }

        public string EmergencyContactPhoneNumber2 { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public decimal ScheduleHours { get; set; }
    }

}
