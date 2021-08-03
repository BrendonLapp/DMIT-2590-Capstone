namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_TEAM_HISTORY
    {
        [Key]
        public int TeamHistoryID { get; set; }

        public int TeamID { get; set; }

        public int EmployeeID { get; set; }

        public int RoleID { get; set; }

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

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE3 { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE4 { get; set; }

        public virtual TB_Capstone_ROLE TB_Capstone_ROLE { get; set; }

        public virtual TB_Capstone_TEAM TB_Capstone_TEAM { get; set; }
    }
}
