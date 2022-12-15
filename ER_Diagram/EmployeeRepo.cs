using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                    if(reader.HasRows)
                    {
                            while(reader.Read())
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(objConnection.State==ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
    }
}
