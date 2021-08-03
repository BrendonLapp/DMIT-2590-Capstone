using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Class used to track unsaved team assignments but keeping both a EmployeeID and a RoleID
    /// </summary>
    public class UnsavedAssignmentDataClass
    {
        public int EmployeeID { get; set; }

        public int RoleID { get; set; }

        public UnsavedAssignmentDataClass()
        {

        }

        public UnsavedAssignmentDataClass(int employeeID, int roleID)
        {
            EmployeeID = employeeID;
            RoleID = roleID;
        }
    }
}