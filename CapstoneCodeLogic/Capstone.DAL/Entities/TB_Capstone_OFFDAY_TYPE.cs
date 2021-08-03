namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_OFFDAY_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_Capstone_OFFDAY_TYPE()
        {
            TB_Capstone_ABSENCE_DETAIL = new HashSet<TB_Capstone_ABSENCE_DETAIL>();
            TB_Capstone_ENTITLED_TIME_OFF = new HashSet<TB_Capstone_ENTITLED_TIME_OFF>();
        }

        [Key]
        public int OffDayID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string AbbreviatedName { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public bool PTO { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_ABSENCE_DETAIL> TB_Capstone_ABSENCE_DETAIL { get; set; }

        public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE { get; set; }

        /*        public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE1 { get; set; } */

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_ENTITLED_TIME_OFF> TB_Capstone_ENTITLED_TIME_OFF { get; set; }
    }
}
