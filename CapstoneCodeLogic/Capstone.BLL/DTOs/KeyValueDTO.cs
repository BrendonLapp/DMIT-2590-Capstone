using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object for Key Value pairs across the application
    /// </summary>
    public class KeyValueDTO
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
