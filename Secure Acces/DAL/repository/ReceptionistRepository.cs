using Azure.Core;
using Logic.Classes;
using Logic.Interface;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using Request = Logic.Classes.Request;

namespace DAL.repository
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly string _connectionString;

        public ReceptionistRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Request> GetAllRequests()
        {
            var requests = new List<Request>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string request = @"SELECT rq.Id, rq.Name, rq.Email, rq.DoorId, d.Name AS doorName, rq.RequestTime, rq.Status  
                                FROM Request rq
                                INNER JOIN Door d ON rq.DoorId = d.door_id";
                using (SqlCommand cmd = new SqlCommand(request, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        string email = reader.GetString(reader.GetOrdinal("Email"));
                        int doorId = reader.GetInt32(reader.GetOrdinal("DoorId"));
                        string doorname = reader.GetString(reader.GetOrdinal("doorName"));
                        DateTime requestTime = reader.GetDateTime(reader.GetOrdinal("RequestTime"));
                        int status = reader.GetInt32(reader.GetOrdinal("Status"));

                        requests.Add(new Request
                        (
                            id, name, email, doorId, doorname, requestTime, status
                        ));
                    }
                }
            }
            return requests;
        }

        public void AddRequest(Request request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            INSERT INTO Request (Name, Email, DoorId, RequestTime, [Status]) 
            VALUES (@Name, @Email, @DoorId, GETDATE(), @Status)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", request.Name);
                    command.Parameters.AddWithValue("@Email", request.Email);
                    command.Parameters.AddWithValue("@DoorId", request.DoorId);
                    command.Parameters.AddWithValue("@Status", request.Status);
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateRequestStatus(int requestId, int status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                        UPDATE Request
                        SET Status = @Status
                        WHERE Id = @RequestId;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@RequestId", requestId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Request GetRequestById(int id)
        {
            Request request;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT rq.Name, rq.Email, rq.DoorId, d.Name AS doorName, rq.RequestTime, rq.Status  
                                FROM Request rq
                                INNER JOIN Door d ON rq.DoorId = d.door_id
                                WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            int doorId = reader.GetInt32(reader.GetOrdinal("DoorId"));
                            string doorname = reader.GetString(reader.GetOrdinal("doorName"));
                            DateTime requestTime = reader.GetDateTime(reader.GetOrdinal("RequestTime"));
                            int status = reader.GetInt32(reader.GetOrdinal("Status"));

                            request = new Request
                            (
                                name, email, doorId, doorname, requestTime, status
                            );
                            return request;
                        }
                    }
                    return null;
                }
            }
        }

        public Request GetLatestRequest()
        {
            Request request;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT *  
                                FROM Request rq
                                INNER JOIN Door d ON rq.DoorId = d.door_id
                                ORDER BY Id DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            int doorId = reader.GetInt32(reader.GetOrdinal("DoorId"));
                            DateTime requestTime = reader.GetDateTime(reader.GetOrdinal("RequestTime"));
                            int status = reader.GetInt32(reader.GetOrdinal("Status"));

                            request = new Request
                            (
                                name, email, doorId, requestTime, status
                            );
                            return request;
                        }
                    }
                    return null;
                }
            }
        }

    }

}
