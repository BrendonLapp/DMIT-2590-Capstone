namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_PROJECT_DETAIL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public TB_Capstone_PROJECT_DETAIL()
        //{
        //    TB_Capstone_OVERTIME = new HashSet<TB_Capstone_OVERTIME>();
        //}

        [Key]
        public int ProjectDetailID { get; set; }

        public int ProjectID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

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

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE2 { get; set; }



        public virtual TB_Capstone_PROJECT TB_Capstone_PROJECT { get; set; }
    }
}
