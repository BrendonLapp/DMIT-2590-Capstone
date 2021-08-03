namespace Capstone.BLL.DTOs.OvertimeRequestDTOs
{
    /// <summary>
    /// DTO for transferinga  list of employees on a team between the OvertimeRequest Controller and OvertimeRequest ViewController
    /// </summary>
    public class EmployeeList
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
    }
}
