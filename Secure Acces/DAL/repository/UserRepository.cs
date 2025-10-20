using Microsoft;
using System.Data;
using Microsoft.Data.SqlClient;
using Logic.Dto;
using Logic.Interface;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<DtoUser> GetAllUsers()
        {
            var users = new List<DtoUser>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT Id, Name FROM user";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new DtoUser
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            });
                        }
                    }
                }
            }

            return users;
        }
    }
}
