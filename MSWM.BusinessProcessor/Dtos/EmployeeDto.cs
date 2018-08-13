using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWM.BusinessProcessor.Dtos
{
    public class EmployeeDto
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string phone { get; set; }
    }
}
