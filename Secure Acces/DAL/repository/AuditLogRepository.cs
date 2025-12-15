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

                string query = "SELECT AuditLogId, LogTime, UserId, DoorId, AuditType, ExtraData FROM AuditLogs ORDER BY AuditLogId DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(ReadDtoFromReader(reader));
                        }
                    }
                }
            }

            return logs;
        }

        public List<DtoAuditLog> GetAuditLogsByUserId(int userId)
        {
            var logs = new List<DtoAuditLog>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, LogTime, UserId, DoorId, AuditType, ExtraData " +
                               "FROM AuditLogs WHERE UserId = @UserId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(ReadDtoFromReader(reader));
                        }
                    }
                }
            }

            return logs;
        }


        public List<DtoAuditLog> GetAuditLogsByDoorId(int doorId)
        {
            var logs = new List<DtoAuditLog>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, LogTime, UserId, DoorId, AuditType, ExtraData " +
                               "FROM AuditLogs WHERE DoorId = @DoorId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DoorId", doorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(ReadDtoFromReader(reader));
                        }
                    }
                }
            }

            return logs;
        }

        public void InsertAuditLog(DtoAuditLog log)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO AuditLogs 
                                (UserId, DoorId, AuditType, ExtraData, LogTime)
                                VALUES (@UserId, @DoorId, @AuditType, @ExtraData, GETDATE())";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", log.UserId);
                    cmd.Parameters.AddWithValue("@DoorId", log.DoorId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AuditType", (int)log.AuditType);
                    cmd.Parameters.AddWithValue("@ExtraData", log.ExtraData ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private DtoAuditLog ReadDtoFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            DateTime date = reader.GetDateTime(1);
            int userId = reader.GetInt32(2);

            int? doorId = reader.IsDBNull(3) ? null : reader.GetInt32(3);
            AuditType auditType = (AuditType)reader.GetInt32(4);
            string? extraData = reader.IsDBNull(5) ? null : reader.GetString(5);

            return new DtoAuditLog
            {
                Id = id,
                UserId = userId,
                DoorId = doorId,
                Date = date,
                AuditType = auditType,
                ExtraData = extraData
            };
        }
    }
}
