using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ER_Diagram
{
    public class EmployeeRepo
    {
        public static string connectionstring = @"Server=DESKTOP-ICFRQNG;Database=EmployeePayroll_Service;User Id=DESKTOP-ICFRQNG/Ramchandra;Password=;TrustServerCertificate=True;Integrated Security=SSPI;";
        SqlConnection objConnection = new SqlConnection(connectionstring);
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                using (this.objConnection)
                {
                    string query = @"Select * from Employee_Payroll;";
                    SqlCommand objCommand = new SqlCommand(query, objConnection);
                    this.objConnection.Open();
                    SqlDataReader reader = objCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32("Id");
                            model.Name = reader.IsDBNull("Name") ? string.Empty : reader.GetString("Name");
                            model.Basic_Pay = reader.IsDBNull("Basic_Pay") ? 0.0 : (Double)reader.GetDecimal("Basic_Pay");
                            model.Gender = Convert.ToChar(reader.IsDBNull("Gender") ? String.Empty : reader.GetString("Gender"));
                            model.Department = reader.IsDBNull("Department") ? string.Empty : reader.GetString("Department");
                            model.PhoneNumber = reader.IsDBNull("PhoneNumber") ? string.Empty : reader.GetString("PhoneNumber");
                            model.Address = reader.IsDBNull("Address") ? string.Empty : reader.GetString("Address");
                            model.Deductions = reader.IsDBNull("Deductions") ? 0.0 : (Double)reader.GetDecimal("Deductions");
                            model.Taxable_Pay = reader.IsDBNull("Taxable_Pay") ? 0.0 : (Double)reader.GetDecimal("Taxable_Pay");
                            model.Tax = reader.IsDBNull("Tax") ? 0.0 : (Double)reader.GetDecimal("Tax");
                            model.Net_Pay = reader.IsDBNull("Net_pay") ? 0.0 : (Double)reader.GetDecimal("Net_pay");
                            model.Start = reader.IsDBNull("Start") ? DateTime.MinValue : reader.GetDateTime("Start");
                            model.City = reader.IsDBNull("City") ? string.Empty : reader.GetString("City");
                            model.Country = reader.IsDBNull("Country") ? string.Empty : reader.GetString("Country");
                            Console.WriteLine(JsonConvert.SerializeObject(model));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.objConnection)
                {
                    SqlCommand Command = new SqlCommand("SpAddEmployeeDetails", this.objConnection);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@ Id", model.Id);
                    Command.Parameters.AddWithValue("@Name", model.Name);
                    Command.Parameters.AddWithValue("@Basic_Pay", model.Basic_Pay);
                    Command.Parameters.AddWithValue("@Gender", model.Gender);
                    Command.Parameters.AddWithValue("@Department", model.Department);
                    Command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    Command.Parameters.AddWithValue("@Address", model.Address);
                    Command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    Command.Parameters.AddWithValue("@Taxable_Pay", model.Taxable_Pay);
                    Command.Parameters.AddWithValue("@Tax", model.Tax);
                    Command.Parameters.AddWithValue("@Start", model.Start);
                    Command.Parameters.AddWithValue("@City", model.City);
                    Command.Parameters.AddWithValue("@Country", model.Country);
                    this.objConnection.Open();
                    var result = Command.ExecuteNonQuery();
                    this.objConnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.objConnection.Close();
            }
        }
        public string UpdateSalary()
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);

            using (objConnection)
            {
                string query = @"Update Employee_Payroll Set Basic_Pay = 3000000 Where Name = 'Mahesh' and Id = 2";
                SqlCommand objCommand = new SqlCommand(query, objConnection);
                objConnection.Open();
                try
                {
                    var objDataReader = objCommand.ExecuteNonQuery();
                    if (objDataReader >= 1)
                    {
                        return "Data Updated";
                    }
                    else
                    {
                        return "Data  Not Updated";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
        }
        public string UpdateSalaryUsingStoredProcedure(EmployeeModel model)
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);

            using (objConnection)
            {
                SqlCommand objCommand = new SqlCommand("UpdateSalary", objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Id", model.Id);
                objCommand.Parameters.AddWithValue("@Name", model.Name);
                objCommand.Parameters.AddWithValue("@Salary", model.Basic_Pay);
                objConnection.Open();
                try
                {
                    var objDataReader = objCommand.ExecuteNonQuery();
                    if (objDataReader >= 1)
                    {
                        return "Data Updated";
                    }
                    else
                    {
                        return "Data  Not Updated";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
        }
        public string GetDataByName(EmployeeModel model)
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);
            EmployeeModel objEmployeeModel = new EmployeeModel();
            try
            {
                using (objConnection)
                {
                    SqlCommand objCommand = new SqlCommand("GetByName", objConnection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@Name", model.Name);
                    objConnection.Open();
                    SqlDataReader reader = objCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32("Id");
                            model.Name = reader.IsDBNull("Name") ? string.Empty : reader.GetString("Name");
                            model.Basic_Pay = reader.IsDBNull("Basic_Pay") ? 0.0 : (Double)reader.GetDecimal("Basic_Pay");
                            model.Gender = Convert.ToChar(reader.IsDBNull("Gender") ? String.Empty : reader.GetString("Gender"));
                            model.Department = reader.IsDBNull("Department") ? string.Empty : reader.GetString("Department");
                            model.PhoneNumber = reader.IsDBNull("PhoneNumber") ? string.Empty : reader.GetString("PhoneNumber");
                            model.Address = reader.IsDBNull("Address") ? string.Empty : reader.GetString("Address");
                            model.Deductions = reader.IsDBNull("Deductions") ? 0.0 : (Double)reader.GetDecimal("Deductions");
                            model.Taxable_Pay = reader.IsDBNull("Taxable_Pay") ? 0.0 : (Double)reader.GetDecimal("Taxable_Pay");
                            model.Tax = reader.IsDBNull("Tax") ? 0.0 : (Double)reader.GetDecimal("Tax");
                            model.Net_Pay = reader.IsDBNull("Net_pay") ? 0.0 : (Double)reader.GetDecimal("Net_pay");
                            model.Start = reader.IsDBNull("Start") ? DateTime.MinValue : reader.GetDateTime("Start");
                            model.City = reader.IsDBNull("City") ? string.Empty : reader.GetString("City");
                            model.Country = reader.IsDBNull("Country") ? string.Empty : reader.GetString("Country");
                            Console.WriteLine(JsonConvert.SerializeObject(model));
                            Console.WriteLine($"Employee ID   : {model.Id},\n" +
                                             $"Employee Name : {model.Name},\n" +
                                             $"PhoneNumber   : {model.PhoneNumber},\n" +
                                             $"Address       : {model.Address},\n" +
                                             $"Department    : {model.Department},\n" +
                                             $"Gender        : {model.Gender},\n" +
                                             $"Basic_Pay     : {model.Basic_Pay},\n" +
                                             $"Deductions    : {model.Deductions},\n" +
                                             $"Taxable_Pay   : {model.Taxable_Pay},\n" +
                                             $"Tax           : {model.Tax},\n" +
                                             $"Net_Pay       : {model.Net_Pay},\n" +
                                             $"StartDate     : {model.Start},\n" +
                                             $"City          : {model.City},\n" +
                                             $"Country       : {model.Country}\n");
                        }
                        return "Data Found";
                    }
                    else
                    {
                        Console.WriteLine("No Records Found in the table");
                        return "Data Not Found";
                    }
                    reader.Close();
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        public string GetDataWithinDateRange(DateTime start, DateTime end)
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);
            EmployeeModel model = new EmployeeModel();
            try
            {
                using (objConnection)
                {
                    SqlCommand objCommand = new SqlCommand("GetByDateWithinRange", objConnection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@Start", start);
                    objCommand.Parameters.AddWithValue("@End", end);
                    objConnection.Open();
                    SqlDataReader reader = objCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32("Id");
                            model.Name = reader.IsDBNull("Name") ? string.Empty : reader.GetString("Name");
                            model.Basic_Pay = reader.IsDBNull("Basic_Pay") ? 0.0 : (Double)reader.GetDecimal("Basic_Pay");
                            model.Gender = Convert.ToChar(reader.IsDBNull("Gender") ? String.Empty : reader.GetString("Gender"));
                            model.Department = reader.IsDBNull("Department") ? string.Empty : reader.GetString("Department");
                            model.PhoneNumber = reader.IsDBNull("PhoneNumber") ? string.Empty : reader.GetString("PhoneNumber");
                            model.Address = reader.IsDBNull("Address") ? string.Empty : reader.GetString("Address");
                            model.Deductions = reader.IsDBNull("Deductions") ? 0.0 : (Double)reader.GetDecimal("Deductions");
                            model.Taxable_Pay = reader.IsDBNull("Taxable_Pay") ? 0.0 : (Double)reader.GetDecimal("Taxable_Pay");
                            model.Tax = reader.IsDBNull("Tax") ? 0.0 : (Double)reader.GetDecimal("Tax");
                            model.Net_Pay = reader.IsDBNull("Net_pay") ? 0.0 : (Double)reader.GetDecimal("Net_pay");
                            model.Start = reader.IsDBNull("Start") ? DateTime.MinValue : reader.GetDateTime("Start");
                            model.City = reader.IsDBNull("City") ? string.Empty : reader.GetString("City");
                            model.Country = reader.IsDBNull("Country") ? string.Empty : reader.GetString("Country");
                            Console.WriteLine(JsonConvert.SerializeObject(model));
                            Console.WriteLine($"Employee ID   : {model.Id},\n" +
                                             $"Employee Name : {model.Name},\n" +
                                             $"PhoneNumber   : {model.PhoneNumber},\n" +
                                             $"Address       : {model.Address},\n" +
                                             $"Department    : {model.Department},\n" +
                                             $"Gender        : {model.Gender},\n" +
                                             $"Basic_Pay     : {model.Basic_Pay},\n" +
                                             $"Deductions    : {model.Deductions},\n" +
                                             $"Taxable_Pay   : {model.Taxable_Pay},\n" +
                                             $"Tax           : {model.Tax},\n" +
                                             $"Net_Pay       : {model.Net_Pay},\n" +
                                             $"StartDate     : {model.Start},\n" +
                                             $"City          : {model.City},\n" +
                                             $"Country       : {model.Country}\n");
                        }
                        reader.Close();
                        return "Data Found";
                    }
                    else
                    {
                        Console.WriteLine("No Records Found in the table");
                        reader.Close();
                        return "Data Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

        }
        public void GetAggregateFunction(string Name)
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);
            try
            {
                using (objConnection)
                {
                    string query = @$"SELECT SUM(Basic_Pay),MAX(Basic_Pay),MIN(Basic_Pay),AVG(Basic_Pay),Gender,COUNT(*) FROM Employee_Payroll WHERE Name= 'Ramchandra'  GROUP BY Gender;";
                    SqlCommand command = new SqlCommand(query, objConnection);
                    objConnection.Open();
                    SqlDataReader result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            Console.WriteLine($"Total Salary = {result[0]}\n Max Salary = {result[1]}\n Min Salary = {result[2]}\n Avg Salary = {result[3]}\n Gender = {result[4]} \n Count = {result[5]}\n");
                        }
                        result.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                objConnection.Close();
            }
        }
        public string InsertEmployee(EmployeeModel employee)
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);
            using (objConnection)
            {
                string query = @$"INSERT Into Employee_Payroll (EmployeeName,PhoneNumber,Address, Basic_Pay, StartDate, Gender, Department, Deductions, Taxable_Pay, Tax, Net_Pay,City,Country) Values ('{employee.Name}','{employee.PhoneNumber}','{employee.Address}','{employee.Basic_Pay}','{employee.Start}','{employee.Gender}', '{employee.Department}','{employee.Deductions}','{employee.Taxable_Pay}','{employee.Tax}','{employee.Net_Pay}','{employee.City}','{employee.Country}')";

                SqlCommand Command = new SqlCommand(query, objConnection);
                objConnection.Open();
                try
                {
                    var reader = Command.ExecuteNonQuery();
                    if (reader >= 1)
                    {
                        return "Data Inserted Successfully";
                    }
                    else
                    {
                        return "Data Not Inserted";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
        }
        public string InsertEmployee_EmployeePayroll_AsWellAs_PayrollDetail(EmployeeModel employee)
        {
            SqlConnection objConnection = new SqlConnection(connectionstring);
            using (objConnection)
            {
                //string query = @$"INSERT Into Employee_Payroll (EmployeeName,PhoneNumber,Address, Basic_Pay, StartDate, Gender, Department, Deductions, Taxable_Pay, Tax, Net_Pay,City,Country) Values ('{employee.Name}','{employee.PhoneNumber}','{employee.Address}','{ employee.Basic_Pay}','{ employee.StartDate}','{employee.Gender}', '{employee.Department}','{ employee.Deductions}','{employee.Taxable_Pay}','{ employee.Tax}','{ employee.Net_Pay}','{employee.City}','{employee.Country}')";
                string query = @$"begin TRANSACTION declare @EmpId int, @Basic_Pay money INSERT Into employee_payroll (EmployeeName,PhoneNumber,Address, Basic_Pay, StartDate, Gender, Department, Deductions, Taxable_Pay, Tax, Net_Pay,City,Country) Values  ('{employee.Name}','{employee.PhoneNumber}','{employee.Address}','{employee.Basic_Pay}','{employee.Start}','{employee.Gender}', '{employee.Department}','{employee.Deductions}','{employee.Taxable_Pay}','{employee.Tax}','{employee.Net_Pay}','{employee.City}','{employee.Country}') set @EmpID = (select EmployeeID from Employee_Payroll where EmployeeName = '{employee.Name}') set @Basic_Pay = (select Basic_Pay from Employee_Payroll where EmployeeName = '{employee.Name}') insert into Payroll_Detail (EmployeeID,Salary) values (@EmpID, @Basic_Pay) Commit;";

                SqlCommand objCommand = new SqlCommand(query, objConnection);
                objConnection.Open();
                try
                {
                    var objDataReader = objCommand.ExecuteNonQuery();
                    if (objDataReader >= 1)
                    {
                        return "Data Inserted Successfully in Both Tables";
                    }
                    else
                    {
                        return "Data Not Inserted in Tables";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
        }
    }
}
            

          
  

            

    
