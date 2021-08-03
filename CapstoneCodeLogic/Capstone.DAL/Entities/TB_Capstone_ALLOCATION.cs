namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_ALLOCATION
    {
        [Key]
        public int AllocationID { get; set; }

        public int ProjectID { get; set; }

        public int EmployeeID { get; set; }

        public decimal AllocatedDays { get; set; }

        public int Year { get; set; }

        public decimal January { get; set; }

        public decimal February { get; set; }

        public decimal March { get; set; }

        public decimal April { get; set; }

        public decimal May { get; set; }

        public decimal June { get; set; }

        public decimal July { get; set; }

        public decimal August { get; set; }

        public decimal September { get; set; }

        public decimal October { get; set; }

        public decimal November { get; set; }

        public decimal December { get; set; }

        [StringLength(50)]
        public string Notes { get; set; }

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

        public virtual TB_Capstone_PROJECT TB_Capstone_PROJECT { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE2 { get; set; }
    }
}
