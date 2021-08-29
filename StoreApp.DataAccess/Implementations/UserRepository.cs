using StoreApp.DataAccess.Interfaces;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace StoreApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }
        private SqlConnection _connection;

        public SqlConnection Connection
        {
            get
            {
                _connection = new SqlConnection(_connectionString);
                
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }
        public MembershipUser Add(string username,
                     string password,
                     string email)
        {

            using (var connection = Connection)
            {
                var query = ("INSERT INTO Users (Email, UserName, [Password] , ProviderName, LastActivityDate , LastLoginDate, CreationDate )" +
                    "VALUES(@Email, @UserName, @Password, @ProviderName, @LastActivityDate, @LastLoginDate, @CreationDate)");

                string ProviderName = "CustomMembershipProvider";
                DateTime CreationDate = DateTime.Now;
                DateTime LastLoginDate = DateTime.Now;
                DateTime LastActivityDate = DateTime.Now;

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                command.Parameters.Add("@ProviderName", SqlDbType.NVarChar).Value = ProviderName;
                command.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = CreationDate;
                command.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = LastLoginDate;
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = CreationDate;

                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return new MembershipUser(ProviderName, username, string.Empty, email, string.Empty, string.Empty, true,
                      false, CreationDate, LastLoginDate, LastActivityDate, DateTime.MinValue, DateTime.MinValue);
                }

                throw new ArgumentException("An error occured");
            }

        }

        public bool Login(string userName, string password)
        {
            using (var connection = Connection)
            {
                var query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                var result = (int)command.ExecuteScalar();


                if (result > 0)
                {
                    return true;
                }

                throw new UnauthorizedAccessException("Invalid Credentials");
            }
        }
        public bool DeleteUser(string userName, bool deleteAllRelatedData)
        {
            using (var connection = Connection)
            {
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.Parameters.Add("userName", SqlDbType.NVarChar).Value = userName;

                    var query1 = "DELETE FROM Stores WHERE UserName = @userName";
                    command.CommandText = query1;
                    var result1 = command.ExecuteNonQuery();

                    var query2 = "DELETE FROM Users WHERE UserName = @userName";
                    command.CommandText = query2;
                    var result2 = command.ExecuteNonQuery();

                    transaction.Commit();

                    return result2 > 0 || result1 > 0;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    try
                    {
                        // Attempt to roll back the transaction.
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection
                        // is closed or the transaction has already been rolled
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }

                    return false;
                }
            }
        }


        public MembershipUser GetUser(string userName, bool userIsOnline)
        {

            using (var connection = Connection)
            {

                var query = "SELECT * FROM Users WHERE UserName = @userName";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("userName", SqlDbType.NVarChar).Value = userName;

                var reader = command.ExecuteReader();

                string ProviderName = string.Empty;
                string Email = string.Empty;
                string UserName = string.Empty;
                DateTime CreationDate = DateTime.MinValue;
                DateTime LastLoginDate = DateTime.MinValue;
                DateTime LastActivityDate = DateTime.MinValue;

                MembershipUser user = null;
                if(reader.HasRows)
                {
                    ProviderName = reader["ProviderName"].ToString();
                    Email = reader["Email"].ToString();
                    UserName = reader["UserName"].ToString();
                    CreationDate = Convert.ToDateTime(reader["CreationDate"].ToString());
                    LastLoginDate = Convert.ToDateTime(reader["LastLoginDate"].ToString());
                    LastActivityDate = Convert.ToDateTime(reader["LastActivityDate"].ToString());

                    user = new MembershipUser(ProviderName, UserName, string.Empty, Email, string.Empty, string.Empty, true,
                    false, CreationDate, LastLoginDate, LastActivityDate, DateTime.MinValue, DateTime.MinValue);

                    UpdateUser(user);
                    return user;
                }
                
                throw new ArgumentNullException("Invalid UserName");
            }
        }

        public string GetUserNameByEmail(string email)
        {
            using (var connection = Connection)
            {

                var query = "SELECT UserName FROM Users WHERE Email = @email";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("email", SqlDbType.NVarChar).Value = email;

                var reader = command.ExecuteScalar();
                if (reader is null)
                {
                    return string.Empty;
                }
                return reader.ToString();
            }
        }

        public void UpdateUser(MembershipUser user)
        {
            using (var connection = Connection)
            {

                var query = ("UPDATE Stores SET UserName = @UserName, LastLoginDate = @LastLoginDate, LastActivityDate = @LastActivityDate WHERE UserName = @UserName");

                SqlCommand command = new SqlCommand(query, connection);

                IDataParameter firstNameValue, lastNameValue, userNameValue, lastLoginValue;

                firstNameValue = new SqlParameter
                {
                    Value = user.UserName,
                    SqlDbType = SqlDbType.NVarChar
                };

                lastNameValue = new SqlParameter
                {
                    Value = user.LastActivityDate,
                    SqlDbType = SqlDbType.DateTime
                };

                lastLoginValue = new SqlParameter
                {
                    Value = user.LastLoginDate,
                    SqlDbType = SqlDbType.DateTime
                };

                userNameValue = new SqlParameter
                {
                    Value = user.LastActivityDate,
                    SqlDbType = SqlDbType.DateTime
                };

                var @params = new IDataParameter[] { firstNameValue, lastNameValue, userNameValue, lastLoginValue };
                command.Parameters.AddRange(@params);

                command.ExecuteNonQuery();
            }
        }

        public ICollection<MembershipUser> GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            using (var connection = Connection)
            {
                var startValue = pageIndex * pageSize;

                var query = "SELECT * FROM Users ORDER BY name OFFSET startValue = @startValue ROWS FETCH NEXT endValue = @endValue ROWS ONLY ";

                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.Add("startValue", SqlDbType.Int).Value = startValue;
                command.Parameters.Add("endValue", SqlDbType.Int).Value = pageSize;

                var reader = command.ExecuteReader();

                var countQuery = "SELECT COUNT(*) FROM Users";
                command.CommandText = countQuery;

                totalRecords = command.ExecuteNonQuery();

                string ProviderName = string.Empty;
                string Email = string.Empty;
                string UserName = string.Empty;
                DateTime CreationDate = DateTime.MinValue;
                DateTime LastLoginDate = DateTime.MinValue;
                DateTime LastActivityDate = DateTime.MinValue;

                List<MembershipUser> users = new List<MembershipUser>();

                while (reader.Read())
                {
                    ProviderName = reader["ProviderName"].ToString();
                    Email = reader["Email"].ToString();
                    UserName = reader["UserName"].ToString();
                    CreationDate = Convert.ToDateTime(reader["CreationDate"].ToString());
                    LastLoginDate = Convert.ToDateTime(reader["LastLoginDate"].ToString());
                    LastActivityDate = Convert.ToDateTime(reader["LastActivityDate"].ToString());

                    MembershipUser user = new MembershipUser(ProviderName, UserName, string.Empty, Email, string.Empty, string.Empty, true,
                    false, CreationDate, LastLoginDate, LastActivityDate, DateTime.MinValue, DateTime.MinValue);

                    users.Add(user);
                }

                return users;
            }
        }
    }
}
