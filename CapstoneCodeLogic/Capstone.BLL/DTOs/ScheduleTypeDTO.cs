using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information designed for TB_Capstone_SCHEDULE_TYPE tables
    /// </summary>
    public class ScheduleTypeDTO
    {
        [Key]
        public int ScheduleTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal HoursPerDay { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
    }
}
