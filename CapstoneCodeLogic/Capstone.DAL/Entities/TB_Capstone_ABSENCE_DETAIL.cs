namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_ABSENCE_DETAIL
    {
        [Key]
        public int AbsenceID { get; set; }

        public int OffDayID { get; set; }

        public int EmployeeID { get; set; }

        [StringLength(2)]
        public string HalfDay { get; set; }

        [Column(TypeName = "date")]
        public DateTime AbsenceDate { get; set; }

        public decimal? Hours { get; set; }

        [StringLength(100)]
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

        public virtual TB_Capstone_OFFDAY_TYPE TB_Capstone_OFFDAY_TYPE { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE2 { get; set; }
    }
}
