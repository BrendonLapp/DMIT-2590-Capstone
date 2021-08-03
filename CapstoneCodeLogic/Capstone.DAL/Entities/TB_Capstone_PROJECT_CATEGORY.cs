namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_PROJECT_CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_Capstone_PROJECT_CATEGORY()
        {
            TB_Capstone_PROJECT = new HashSet<TB_Capstone_PROJECT>();
        }

        [Key]
        public int ProjectCategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime ActivationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeactivationDate { get; set; }

        public bool Global { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_PROJECT> TB_Capstone_PROJECT { get; set; }
    }
}
