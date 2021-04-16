using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using USMALL.Models;

namespace USMALL.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private string ConnStr
        {
            get  { return WebConfigurationManager.ConnectionStrings["USMALLConnStr"].ConnectionString; }
        }

        public IList<Employee> GetAll()
        {
            IList<Employee> employee = new List<Employee>();
          using  (var conn = new SqlConnection(ConnStr))
            {
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT e.[EmployeeId]
                                              ,e.[LastName]
                                              ,e.[FirstName]
                                              ,e.[JobTitle]
                                              ,e.[Department]
                                              ,e.[ReportsTo]
                                              ,e.[HireDate]
                                              ,e.[Address]
                                              ,e.[City]
                                              ,e.[Phone]
                                              ,e.[Email]
                                            ,m.FirstName as SFirstName
                                            ,m.LastName as SLastName
                                          FROM [dbo].[Employee] e LEFT Join Employee m on e.ReportsTo = m.EmployeeId
                                        ";
                    conn.Open();
                    using(var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var emp = new Employee();
                            emp.EmployeeId = rdr.GetInt32(rdr.GetOrdinal("EmployeeId"));
                            emp.LastName = rdr.GetString(rdr.GetOrdinal("LastName"));
                            emp.FirstName = rdr.GetString(rdr.GetOrdinal("FirstName"));
                            emp.JobTitle = rdr.GetString(rdr.GetOrdinal("JobTitle"));
                            emp.Department = rdr.GetString(rdr.GetOrdinal("Department"));

                            if (!rdr.IsDBNull(rdr.GetOrdinal("ReportsTo")))
                            {
                                emp.ReportsTo = rdr.GetInt32(rdr.GetOrdinal("ReportsTo"));
                            }

                            if (!rdr.IsDBNull(rdr.GetOrdinal("HireDate")))
                            {
                                emp.HireDate = rdr.GetDateTime(rdr.GetOrdinal("HireDate"));
                            }       
                            emp.Address = rdr.GetString(rdr.GetOrdinal("Address"));
                            emp.City = rdr.GetString(rdr.GetOrdinal("City"));
                            emp.Phone = rdr.GetString(rdr.GetOrdinal("Phone"));
                            emp.Email = rdr.GetString(rdr.GetOrdinal("Email"));

                            emp.ReportstoEmployee = new Employee();
                            if (!rdr.IsDBNull(rdr.GetOrdinal("SFirstName")))
                                emp.ReportstoEmployee.FirstName = rdr.GetString(rdr.GetOrdinal("SFirstName"));
                            if (!rdr.IsDBNull(rdr.GetOrdinal("SLastName")))
                                emp.ReportstoEmployee.LastName = rdr.GetString(rdr.GetOrdinal("SLastName"));

                            employee.Add(emp);

                        }
                    }
                }
            }
            return employee;
        }

        public Employee GetById(int id)
        {
            Employee employee = null;
            using (var conn = new SqlConnection(ConnStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [EmployeeId]
                                           ,[LastName]
                                           ,[FirstName]
                                           ,[JobTitle]
                                           ,[Department]
                                           ,[ReportsTo]
                                           ,[HireDate]
                                           ,[Address]
                                           ,[City]
                                           ,[Phone]
                                           ,[Email]
                                           ,[Photo]
                                       FROM [dbo].[Employee]
                                       WHERE EmployeeId = @EmployeeId
                                      ";
                    cmd.Parameters.AddWithValue("@EmployeeId", id);

                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            employee = new Employee()
                            {
                                EmployeeId = id,
                                FirstName=rdr.GetString(rdr.GetOrdinal("FirstName")),
                                LastName = rdr.GetString(rdr.GetOrdinal("LastName")),
                                JobTitle = rdr.GetString(rdr.GetOrdinal("JobTitle")),
                                Department = rdr.GetString(rdr.GetOrdinal("Department")),
                                ReportsTo = rdr.IsDBNull(rdr.GetOrdinal("ReportsTo")) ? (int?)null  :rdr.GetInt32(rdr.GetOrdinal("ReportsTo")),
                                HireDate = (DateTime)(rdr.IsDBNull(rdr.GetOrdinal("HireDate"))
                                ? (DateTime?)null
                                : rdr.GetDateTime(rdr.GetOrdinal("HireDate"))),
                                Address = rdr.GetString(rdr.GetOrdinal("Address")),
                                City = rdr.GetString(rdr.GetOrdinal("City")),
                                Phone = rdr.GetString(rdr.GetOrdinal("Phone")),
                                Email = rdr.GetString(rdr.GetOrdinal("Email")),
                                Photo = rdr.IsDBNull(rdr.GetOrdinal("Photo"))
                                    ? null
                                    : (byte[])rdr["Photo"]
                            };
                        }
                    }
                }
            }
            return employee;
        }

        public void Insert(Employee emp)
        {
            using(var conn = new SqlConnection(ConnStr))
            {
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Employee]
                                                        ([LastName]
                                                        ,[FirstName]
                                                        ,[JobTitle]
                                                        ,[Department]
                                                        ,[ReportsTo]
                                                        ,[HireDate]
                                                        ,[Address]
                                                        ,[City]
                                                        ,[Phone]
                                                        ,[Email]
                                                        ,[Photo])
                                                  VALUES
                                                        (
                                                        @LastName
                                                        ,@FirstName 
                                                        ,@JobTitle
                                                        ,@Department
                                                        ,@ReportsTo
                                                        ,@HireDate
                                                        ,@Address
                                                        ,@City
                                                        ,@Phone
                                                        ,@Email 
                                                        ,@Photo 
	                                               	   )";

                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    if (emp.ReportsTo.HasValue)
                       cmd.Parameters.AddWithValue("@ReportsTo", emp.ReportsTo);
                    else
                       cmd.Parameters.AddWithValue("@ReportsTo", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HireDate", (object)emp.HireDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Photo", (object)emp.Phone ?? SqlBinary.Null);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@City", emp.City);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);

                    conn.Open();
                    cmd.ExecuteNonQuery();
              }
            }
        }

        public void Update(Employee emp)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [dbo].[Employee]
                                                 SET [LastName] 		  = @LastName
                                                    ,[FirstName]		  = @FirstName
                                                    ,[JobTitle]		  = @JobTitle
                                                    ,[Department]		  = @Department
                                                    ,[ReportsTo]		  = @ReportsTo
                                                    ,[HireDate]		  = @HireDate
                                                    ,[Address]		  = @Address
                                                    ,[City] 			  = @City
                                                    ,[Phone] 			  = @Phone
                                                    ,[Email] 			  = @Email
                                               WHERE EmployeeId = @EmployeeId";

                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);       
                    cmd.Parameters.AddWithValue("@ReportsTo", (object)emp.ReportsTo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HireDate", (object)emp.HireDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@City", emp.City);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);

                    cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}