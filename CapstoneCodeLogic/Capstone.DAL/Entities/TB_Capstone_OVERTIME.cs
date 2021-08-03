namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_OVERTIME
    {
        [Key]

        public int OvertimeID { get; set; }

        public int EmployeeID { get; set; }

        public int? ProjectID { get; set; }

        public int OvertimeTypeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime SubmissionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReviewDate { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(100)]
        public string SubmissionNotes { get; set; }

        [StringLength(100)]
        public string ApprovalNotes { get; set; }

        [Required]
        [StringLength(1)]
        public string Approved { get; set; }

        [Column(TypeName = "date")]
        public DateTime ActivationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeactivationDate { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE1 { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE2 { get; set; }

        public virtual TB_Capstone_OVERTIME_TYPE TB_Capstone_OVERTIME_TYPE { get; set; }

        public virtual TB_Capstone_PROJECT TB_Capstone_PROJECT { get; set; }
    }
}
