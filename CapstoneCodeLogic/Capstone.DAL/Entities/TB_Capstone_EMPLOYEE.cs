namespace Capstone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_Capstone_EMPLOYEE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_Capstone_EMPLOYEE()
        {
            TB_Capstone_ABSENCE_DETAIL = new HashSet<TB_Capstone_ABSENCE_DETAIL>();
            //TB_Capstone_ABSENCE_DETAIL1 = new HashSet<TB_Capstone_ABSENCE_DETAIL>();
            //TB_Capstone_ABSENCE_DETAIL2 = new HashSet<TB_Capstone_ABSENCE_DETAIL>();
            TB_Capstone_ALLOCATION = new HashSet<TB_Capstone_ALLOCATION>();
            //TB_Capstone_ALLOCATION1 = new HashSet<TB_Capstone_ALLOCATION>();
            //TB_Capstone_ALLOCATION2 = new HashSet<TB_Capstone_ALLOCATION>();
            TB_Capstone_AREA = new HashSet<TB_Capstone_AREA>();
            //TB_Capstone_AREA1 = new HashSet<TB_Capstone_AREA>();
            TB_Capstone_DEPARTMENT = new HashSet<TB_Capstone_DEPARTMENT>();
            //TB_Capstone_DEPARTMENT1 = new HashSet<TB_Capstone_DEPARTMENT>();
            TB_Capstone_ENTITLED_TIME_OFF = new HashSet<TB_Capstone_ENTITLED_TIME_OFF>();
            //TB_Capstone_ENTITLED_TIME_OFF1 = new HashSet<TB_Capstone_ENTITLED_TIME_OFF>();
            //TB_Capstone_ENTITLED_TIME_OFF2 = new HashSet<TB_Capstone_ENTITLED_TIME_OFF>();
            TB_Capstone_OVERTIME_TYPE = new HashSet<TB_Capstone_OVERTIME_TYPE>();
            TB_Capstone_OVERTIME = new HashSet<TB_Capstone_OVERTIME>();
            TB_Capstone_OFFDAY_TYPE = new HashSet<TB_Capstone_OFFDAY_TYPE>();
            //TB_Capstone_OVERTIME1 = new HashSet<TB_Capstone_OVERTIME>();
            //TB_Capstone_OVERTIME_TYPE1 = new HashSet<TB_Capstone_OVERTIME_TYPE>();
            //TB_Capstone_OVERTIME2 = new HashSet<TB_Capstone_OVERTIME>();
            //TB_Capstone_OFFDAY_TYPE1 = new HashSet<TB_Capstone_OFFDAY_TYPE>();
            TB_Capstone_PROJECT_CATEGORY = new HashSet<TB_Capstone_PROJECT_CATEGORY>();
            TB_Capstone_PROJECT = new HashSet<TB_Capstone_PROJECT>();
            TB_Capstone_PROJECT_DETAIL = new HashSet<TB_Capstone_PROJECT_DETAIL>();
            TB_Capstone_PAID_HOLIDAY = new HashSet<TB_Capstone_PAID_HOLIDAY>();
            //TB_Capstone_PROJECT_DETAIL1 = new HashSet<TB_Capstone_PROJECT_DETAIL>();
            //TB_Capstone_PROJECT_CATEGORY1 = new HashSet<TB_Capstone_PROJECT_CATEGORY>();
            //TB_Capstone_PROJECT1 = new HashSet<TB_Capstone_PROJECT>();
            //TB_Capstone_PROJECT_DETAIL2 = new HashSet<TB_Capstone_PROJECT_DETAIL>();
            //TB_Capstone_PAID_HOLIDAY1 = new HashSet<TB_Capstone_PAID_HOLIDAY>();
            TB_Capstone_SCHEDULE_TYPE_DETAILS = new HashSet<TB_Capstone_SCHEDULE_TYPE_DETAILS>();
            TB_Capstone_SCHEDULE_TYPE = new HashSet<TB_Capstone_SCHEDULE_TYPE>();
            //TB_Capstone_SCHEDULE_TYPE_DETAILS1 = new HashSet<TB_Capstone_SCHEDULE_TYPE_DETAILS>();
            //TB_Capstone_SCHEDULE_TYPE_DETAILS2 = new HashSet<TB_Capstone_SCHEDULE_TYPE_DETAILS>();
            //TB_Capstone_SCHEDULE_TYPE1 = new HashSet<TB_Capstone_SCHEDULE_TYPE>();
            TB_Capstone_TEAM_HISTORY = new HashSet<TB_Capstone_TEAM_HISTORY>();
            //TB_Capstone_TEAM_HISTORY1 = new HashSet<TB_Capstone_TEAM_HISTORY>();
            //TB_Capstone_TEAM_HISTORY2 = new HashSet<TB_Capstone_TEAM_HISTORY>();
            //TB_Capstone_POSITION1 = new HashSet<TB_Capstone_POSITION>();
            //TB_Capstone_ROLE1 = new HashSet<TB_Capstone_ROLE>();
            TB_Capstone_TEAM = new HashSet<TB_Capstone_TEAM>();
            //TB_Capstone_TEAM_HISTORY3 = new HashSet<TB_Capstone_TEAM_HISTORY>();
            TB_Capstone_UNIT = new HashSet<TB_Capstone_UNIT>();
            //TB_Capstone_EMPLOYEE1 = new HashSet<TB_Capstone_EMPLOYEE>();
            //TB_Capstone_POSITION2 = new HashSet<TB_Capstone_POSITION>();
            //TB_Capstone_ROLE2 = new HashSet<TB_Capstone_ROLE>();
            //TB_Capstone_TEAM1 = new HashSet<TB_Capstone_TEAM>();
            //TB_Capstone_TEAM_HISTORY4 = new HashSet<TB_Capstone_TEAM_HISTORY>();
            //TB_Capstone_UNIT1 = new HashSet<TB_Capstone_UNIT>();
        }

        [Key]
        public int EmployeeID { get; set; }

        public int PositionID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(16)]
        public string PhoneNumber { get; set; }

        [StringLength(16)]
        public string AlternatePhoneNumber { get; set; }

        public int? StationNumber { get; set; }

        public int? ComputerNumber { get; set; }

        [Required]
        [StringLength(16)]
        public string CompanyPhoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ActivationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeactivationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EmergencyContactName1 { get; set; }

        [Required]
        [StringLength(16)]
        public string EmergencyContactPhoneNumber1 { get; set; }

        [StringLength(50)]
        public string EmergencyContactName2 { get; set; }

        [StringLength(16)]
        public string EmergencyContactPhoneNumber2 { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_ABSENCE_DETAIL> TB_Capstone_ABSENCE_DETAIL { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ABSENCE_DETAIL> TB_Capstone_ABSENCE_DETAIL1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ABSENCE_DETAIL> TB_Capstone_ABSENCE_DETAIL2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_ALLOCATION> TB_Capstone_ALLOCATION { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ALLOCATION> TB_Capstone_ALLOCATION1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ALLOCATION> TB_Capstone_ALLOCATION2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_AREA> TB_Capstone_AREA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_AREA> TB_Capstone_AREA1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_DEPARTMENT> TB_Capstone_DEPARTMENT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_DEPARTMENT> TB_Capstone_DEPARTMENT1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_ENTITLED_TIME_OFF> TB_Capstone_ENTITLED_TIME_OFF { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ENTITLED_TIME_OFF> TB_Capstone_ENTITLED_TIME_OFF1 { get; set; }

        public virtual TB_Capstone_POSITION TB_Capstone_POSITION { get; set; }

        //public virtual TB_Capstone_ROLE TB_Capstone_ROLE { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ENTITLED_TIME_OFF> TB_Capstone_ENTITLED_TIME_OFF2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_OVERTIME_TYPE> TB_Capstone_OVERTIME_TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_OVERTIME> TB_Capstone_OVERTIME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_OFFDAY_TYPE> TB_Capstone_OFFDAY_TYPE { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_OVERTIME> TB_Capstone_OVERTIME1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_OVERTIME_TYPE> TB_Capstone_OVERTIME_TYPE1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_OVERTIME> TB_Capstone_OVERTIME2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_OFFDAY_TYPE> TB_Capstone_OFFDAY_TYPE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_PROJECT_CATEGORY> TB_Capstone_PROJECT_CATEGORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_PROJECT> TB_Capstone_PROJECT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_PROJECT_DETAIL> TB_Capstone_PROJECT_DETAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_PAID_HOLIDAY> TB_Capstone_PAID_HOLIDAY { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_PROJECT_DETAIL> TB_Capstone_PROJECT_DETAIL1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_PROJECT_CATEGORY> TB_Capstone_PROJECT_CATEGORY1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_PROJECT> TB_Capstone_PROJECT1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_PROJECT_DETAIL> TB_Capstone_PROJECT_DETAIL2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_PAID_HOLIDAY> TB_Capstone_PAID_HOLIDAY1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_SCHEDULE_TYPE_DETAILS> TB_Capstone_SCHEDULE_TYPE_DETAILS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_SCHEDULE_TYPE> TB_Capstone_SCHEDULE_TYPE { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_SCHEDULE_TYPE_DETAILS> TB_Capstone_SCHEDULE_TYPE_DETAILS1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_SCHEDULE_TYPE_DETAILS> TB_Capstone_SCHEDULE_TYPE_DETAILS2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_SCHEDULE_TYPE> TB_Capstone_SCHEDULE_TYPE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_TEAM_HISTORY> TB_Capstone_TEAM_HISTORY { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_TEAM_HISTORY> TB_Capstone_TEAM_HISTORY1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_TEAM_HISTORY> TB_Capstone_TEAM_HISTORY2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_POSITION> TB_Capstone_POSITION1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ROLE> TB_Capstone_ROLE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_TEAM> TB_Capstone_TEAM { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_TEAM_HISTORY> TB_Capstone_TEAM_HISTORY3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_Capstone_UNIT> TB_Capstone_UNIT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_EMPLOYEE> TB_Capstone_EMPLOYEE1 { get; set; }

        //public virtual TB_Capstone_EMPLOYEE TB_Capstone_EMPLOYEE2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_POSITION> TB_Capstone_POSITION2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_ROLE> TB_Capstone_ROLE2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_TEAM> TB_Capstone_TEAM1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_TEAM_HISTORY> TB_Capstone_TEAM_HISTORY4 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TB_Capstone_UNIT> TB_Capstone_UNIT1 { get; set; }
    }
}
