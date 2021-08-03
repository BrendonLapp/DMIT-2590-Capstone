using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Web.Models.Employee
{
    /// <summary>
    /// View Model for Employee CRUD pages
    /// </summary>
    public class EmployeeViewModel
    {
        [Key]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Display(Name = "Team")]
        public int TeamID { get; set; }

        [Display(Name = "Role")]
        public int RoleID { get; set; }

        [Display(Name = "Schedule Type")]
        public int ScheduleTypeID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(16)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(16)]
        [Display(Name = "Alternate Phone Number")]
        public string AlternatePhoneNumber { get; set; }

        [Display(Name = "Station Number")]
        public int? StationNumber { get; set; }

        [Display(Name = "Computer Number")]
        public int? ComputerNumber { get; set; }

        [Required]
        [StringLength(16)]
        [Display(Name = "Company Phone Number")]
        public string CompanyPhoneNumber { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        //[Required]
        //[Column(TypeName = "date")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime ActivationDate { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? DeactivationDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Emergency Contact 1")]
        public string EmergencyContactName1 { get; set; }

        [Required]
        [StringLength(16)]
        [Display(Name = "Emergency Contact Phone 1")]
        public string EmergencyContactPhoneNumber1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Emergency Contact 2")]
        public string EmergencyContactName2 { get; set; }

        [StringLength(16)]
        [Display(Name = "Emergency Contact Phone 2")]
        public string EmergencyContactPhoneNumber2 { get; set; }

        [Display(Name = "Employee Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}