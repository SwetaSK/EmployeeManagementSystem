using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Data
{
    public class EmployeeRepository
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

        public List<EmployeeModel> GetAllEmployees()
        {
            var list = new List<EmployeeModel>();
            string sql = "SELECT Id, FirstName, LastName, Email, Phone, Department, Designation, Salary, DateOfJoining, IsActive FROM Employees ORDER BY Id DESC";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new EmployeeModel
                        {
                            Id = (int)rdr["Id"],
                            FirstName = rdr["FirstName"].ToString(),
                            LastName = rdr["LastName"].ToString(),
                            Email = rdr["Email"] == DBNull.Value ? null : rdr["Email"].ToString(),
                            Phone = rdr["Phone"] == DBNull.Value ? null : rdr["Phone"].ToString(),
                            Department = rdr["Department"] == DBNull.Value ? null : rdr["Department"].ToString(),
                            Designation = rdr["Designation"] == DBNull.Value ? null : rdr["Designation"].ToString(),
                            Salary = rdr["Salary"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Salary"]),
                            DateOfJoining = rdr["DateOfJoining"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["DateOfJoining"]),
                            IsActive = rdr["IsActive"] != DBNull.Value && (bool)rdr["IsActive"]
                        });
                    }
                }
            }
            return list;
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            string sql = "SELECT Id, FirstName, LastName, Email, Phone, Department, Designation, Salary, DateOfJoining, IsActive FROM Employees WHERE Id=@Id";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new EmployeeModel
                        {
                            Id = (int)rdr["Id"],
                            FirstName = rdr["FirstName"].ToString(),
                            LastName = rdr["LastName"].ToString(),
                            Email = rdr["Email"] == DBNull.Value ? null : rdr["Email"].ToString(),
                            Phone = rdr["Phone"] == DBNull.Value ? null : rdr["Phone"].ToString(),
                            Department = rdr["Department"] == DBNull.Value ? null : rdr["Department"].ToString(),
                            Designation = rdr["Designation"] == DBNull.Value ? null : rdr["Designation"].ToString(),
                            Salary = rdr["Salary"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Salary"]),
                            DateOfJoining = rdr["DateOfJoining"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["DateOfJoining"]),
                            IsActive = rdr["IsActive"] != DBNull.Value && (bool)rdr["IsActive"]
                        };
                    }
                }
            }
            return null;
        }

        public void AddEmployee(EmployeeModel model)
        {
            string sql = @"INSERT INTO Employees (FirstName, LastName, Email, Phone, Department, Designation, Salary, DateOfJoining, IsActive)
                           VALUES (@FirstName, @LastName, @Email, @Phone, @Department, @Designation, @Salary, @DateOfJoining, @IsActive)";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)model.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Department", (object)model.Department ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Designation", (object)model.Designation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", (object)model.Salary ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateOfJoining", (object)model.DateOfJoining ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(EmployeeModel model)
        {
            string sql = @"UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, 
                           Department=@Department, Designation=@Designation, Salary=@Salary, DateOfJoining=@DateOfJoining, IsActive=@IsActive
                           WHERE Id=@Id";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)model.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Department", (object)model.Department ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Designation", (object)model.Designation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", (object)model.Salary ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateOfJoining", (object)model.DateOfJoining ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@Id", model.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            string sql = "DELETE FROM Employees WHERE Id=@Id";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    
     }
}