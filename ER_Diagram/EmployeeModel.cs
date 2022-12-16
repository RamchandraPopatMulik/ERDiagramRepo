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

        //public EmployeeModel(int Id ,string Name,string PhoneNumber,string Address,string Department,char Gender,double Basic_Pay,double Deductions,
        //    double Taxable_Pay,double Tax,double Net_Pay,DateTime Start,string City,string Country)
        //{
        //    this.Id = Id;
        //    this.Name = Name;
        //    this.PhoneNumber = PhoneNumber;
        //    this.Address = Address;
        //    this.Department = Department;
        //    this.Gender = Gender;
        //    this.Basic_Pay = Basic_Pay;
        //    this.Deductions = Deductions;
        //    this.Taxable_Pay = Taxable_Pay;
        //    this.Tax = Tax;
        //    this.Net_Pay = Net_Pay;
        //    this.Start = Start;
        //    this.City = City;
        //    this.Country = Country;
        //}
    }
}
