using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Diagram
{
    public class EmployeeModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public char Gender { get; set; }
        public double Basic_Pay { get; set; }
        public double Deductions { get; set; }
        public double Taxable_Pay { get; set; }
        public double Tax { get; set; }
        public double Net_Pay { get; set; }
        public DateTime Start { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
