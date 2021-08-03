using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.OvertimeRequst
{
    /// <summary>
    /// ViewModel for displaying and transfering information about the TB_Capstone_OVERTIME controller
    /// </summary>
    public class TeamOvertimeViewModel
    {
        [Key]
        public int OvertimeID { get; set; }
        public int EmployeeID { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        public int? ProjectDetailID { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int OvertimeTypeID { get; set; }
        [Display(Name = "Overtime Type")]
        public string OvertimeTypeName { get; set; }
        [Display(Name = "Submission Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? ReviewDate { get; set; }
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Submission Notes")]
        public string SubmissionNotes { get; set; }
        [Display(Name = "Review Notes")]
        public string ApprovalNotes { get; set; }
        [Display(Name = "Status")]
        public string Approved { get; set; }
    }
}