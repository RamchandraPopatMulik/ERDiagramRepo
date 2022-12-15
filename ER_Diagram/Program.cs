namespace ER_Diagram
{
    public class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.Id = 101;
            employeeModel.Name = "Ramchandra";
            employeeModel.PhoneNumber = "9604340676";
            employeeModel.Address = "Revangoan";
            employeeModel.Department = "IT";
            employeeModel.Gender = 'M';
            employeeModel.Basic_Pay = 45000;
            employeeModel.Deductions = 1000;
            employeeModel.Taxable_Pay = 1500;
            employeeModel.Tax = 2000;
            employeeModel.Net_Pay = 45000 - (1000 + 1500 + 2000);
            //repo.AddEmployee(employeeModel);
            repo.GetAllEmployee();
            
        }
    }
}