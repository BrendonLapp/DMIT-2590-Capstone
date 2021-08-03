using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs.OvertimeRequestDTOs
{
    /// <summary>
    /// DTO for trasfering data between the Overtime Request controller and Overtime Request ViewController
    /// </summary>
    public class TeamOvertimeRequestDTO
    {

        public int OvertimeID { get; set; }

        public int EmployeeID { get; set; }

        public int ProjectID { get; set; }

        public string EmployeeName { get; set; }
        public int? ProjectDetailID { get; set; }
        public string ProjectName { get; set; }

        public int OvertimeTypeID { get; set; }

        public string OvertimeTypeName { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DateTime? ReviewDate { get; set; }

        public decimal Amount { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string SubmissionNotes { get; set; }

        public string ApprovalNotes { get; set; }

        public string Approved { get; set; }
    }
}
