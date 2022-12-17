namespace ER_Diagram
{
    public class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employeeModel = new EmployeeModel();
            //repo.AddEmployee(employeeModel);
            repo.UpdateSalary();
        }
    }
}