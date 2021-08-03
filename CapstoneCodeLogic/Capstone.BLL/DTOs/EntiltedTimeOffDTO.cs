using System;


namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for information in TB_Capstone_ENTITLED_TIME_OFF
    /// </summary>
    public class EntiltedTimeOffDTO
    {
        public int OffDayID { get; set; }
        public int EmployeeID { get; set; }
        public string OffDayTypeDescription { get; set; }
        public decimal HoursAccumulated { get; set; }
        public DateTime DeactivationDate { get; set; }
    }
}
