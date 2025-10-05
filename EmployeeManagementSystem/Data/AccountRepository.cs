using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Data
{
    public class AccountRepository
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

        // Validate admin (must exist in DB with Role='Admin')
        public User ValidateAdmin(string username, string password)
        {
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT UserId, Username, Password, Role FROM Users WHERE Username=@username AND Password=@password AND Role='Admin'", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new User
                        {
                            UserId = (int)rdr["UserId"],
                            Username = rdr["Username"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Role = rdr["Role"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // Optionally check if username exists as DB user (role 'User') — returns null if not found
        public User GetUserByUsername(string username)
        {
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT UserId, Username, Password, Role FROM Users WHERE Username=@username", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new User
                        {
                            UserId = (int)rdr["UserId"],
                            Username = rdr["Username"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Role = rdr["Role"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}