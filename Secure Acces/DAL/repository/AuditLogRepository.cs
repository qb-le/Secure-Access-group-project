using Logic.Classes;
using Logic.Dto;
using Logic.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly string _connectionString;

        public AuditLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<DtoAuditLog> GetAllAuditLogs()
        {
            var logs = new List<DtoAuditLog>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM AuditLogs";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            DateTime date = reader.GetDateTime(1);
                            int userId = reader.GetInt32(2);
                            int doorId = reader.GetInt32(3);

                            logs.Add(new DtoAuditLog
                            {
                                Id = id,
                                UserId = userId,
                                Date = date,
                                DoorId = doorId
                            });
                        }
                    }
                }
            }

            return logs;
        }

        public void InsertAuditLog(DtoAuditLog log)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO AuditLogs (UserId, DoorId, LogTime) VALUES (@UserId, @DoorId, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", log.UserId);
                    cmd.Parameters.AddWithValue("@DoorId", log.DoorId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
