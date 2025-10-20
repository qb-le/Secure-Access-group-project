using DAL.Interfaces;
using Logic.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repository
{
    public class DoorRepository : IDoorRepository
    {
        private readonly string _connectionString;

        public DoorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<DoorGroup> GetAllDoorGroups()
        {
            var groups = new List<DoorGroup>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string groupQuery = "SELECT groupId, Name FROM DoorGroup";
                using (SqlCommand cmd = new SqlCommand(groupQuery, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        groups.Add(new DoorGroup(id, name, new List<Door>()));
                    }
                }
            }
            return groups;
        }

        public List<Door> GetDoorsByGroupId(int groupId)
        {
            var doors = new List<Door>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT door_id, Name, doorgroupId FROM Door WHERE doorgroupId = @groupId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@groupId", groupId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            int gId = reader.GetInt32(2);
                            doors.Add(new Door(id, name, gId));
                        }
                    }
                }
            }

            return doors;
        }

        public Door? GetDoorById(int id)
        {
            Door? door = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT door_id, Name, doorgroupId FROM Door WHERE door_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int doorId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            int groupId = reader.GetInt32(2);
                            door = new Door(doorId, name, groupId);
                        }
                    }
                }
            }

            return door;
        }
    }
}