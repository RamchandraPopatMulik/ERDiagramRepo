namespace ER_Diagram
{
    public class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employeeModel = new EmployeeModel();
            //repo.AddEmployee(employeeModel);
            //repo.UpdateSalary();
            //employeeModel.Id = 1;
            //employeeModel.Name = "Ramchandra";
            //employeeModel.Basic_Pay = 5000000;
            //repo.UpdateSalaryUsingStoredProcedure(employeeModel);
            //repo.GetDataByName(employeeModel);
            //repo.GetAggregateFunction("Ramchandra");
            //repo.InsertEmployee(employeeModel);
            repo.InsertEmployee_EmployeePayroll_AsWellAs_PayrollDetail(employeeModel);
        }
    }
}