namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_PROJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_Capstone_PROJECT()
        {
            TB_Capstone_ALLOCATION = new HashSet<TB_Capstone_ALLOCATION>();
            TB_Capstone_PROJECT_DETAIL = new HashSet<TB_Capstone_PROJECT_DETAIL>();
            TB_Capstone_OVERTIME = new HashSet<TB_Capstone_OVERTIME>();
        }

        [Key]
        public int ProjectID { get; set; }

        public int ProjectCategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ProjectedEndDate { get; set; }

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
        public virtual ICollection<TB_Capstone_ALLOCATION> TB_Capstone_ALLOCATION { get; set; }

        public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE1 { get; set; }

        public virtual TB_Capstone_PROJECT_CATEGORY TB_Capstone_PROJECT_CATEGORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_OVERTIME> TB_Capstone_OVERTIME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_PROJECT_DETAIL> TB_Capstone_PROJECT_DETAIL { get; set; }
    }
}
