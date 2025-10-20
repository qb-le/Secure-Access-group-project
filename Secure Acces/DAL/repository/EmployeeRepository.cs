using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class EmployeeDal
    {
        private readonly string _connectionString;

        public EmployeeDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetEmployeeNameById(int employeeId)
        {
            string employeeName = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT Name FROM Employees WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", employeeId);
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                        employeeName = result.ToString();
                }
            }

            return employeeName;
        }
    }
}
