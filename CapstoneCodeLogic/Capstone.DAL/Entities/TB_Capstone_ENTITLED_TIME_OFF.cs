
using Capstone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

public partial class TB_Capstone_ENTITLED_TIME_OFF
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int OffDayID { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EmployeeID { get; set; }

    public decimal HoursAccumulated { get; set; }

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

    public virtual TB_Capstone_OFFDAY_TYPE TB_Capstone_OFFDAY_TYPE { get; set; }
}
