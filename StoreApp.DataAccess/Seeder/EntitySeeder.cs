using Newtonsoft.Json;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Security;

namespace StoreApp.DataAccess.Seeder
{
    public class EntitySeeder
    {
        public static async Task Seed()
        {
            string connect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (var connection = new SqlConnection(connect))
                {
                    await connection.OpenAsync();

                    var countUsersQuery = "SELECT COUNT(*) FROM Users";

                    SqlCommand command = new SqlCommand(countUsersQuery, connection);

                    var result = (int)command.ExecuteScalar();

                    var dir = string.Empty;

                    dir = HostingEnvironment.MapPath("~");

                    var separatedDir = dir.Split(Path.DirectorySeparatorChar).ToList();
                    separatedDir.RemoveAt(separatedDir.Count - 1);
                    separatedDir.RemoveAt(separatedDir.Count - 1);


                    if (result <= 0)
                    {
                        dir = string.Join(@"\", separatedDir) + @"\StoreApp.DataAccess\Seeder\Users.json";
                        var usersData = File.ReadAllText(dir);

                        List<UserSeedDTO> users = JsonConvert.DeserializeObject<List<UserSeedDTO>>(usersData);

                        var insertQuery = ("INSERT INTO Users (Email, UserName, [Password] , ProviderName, LastActivityDate , LastLoginDate, CreationDate )" +
                                    "VALUES(@Email, @UserName, @Password, @ProviderName, @LastActivityDate, @LastLoginDate, @CreationDate)");

                        string ProviderName = "CustomMembershipProvider";
                        command.CommandText = insertQuery;

                        foreach (var user in users)
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                            command.Parameters.Add("@ProviderName", SqlDbType.NVarChar).Value = ProviderName;
                            command.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = user.LastActivityDate;
                            command.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = user.LastLoginDate;
                            command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = user.CreationDate;

                            result = await command.ExecuteNonQueryAsync();
                        }



                        dir = string.Join(@"\", separatedDir) + @"\StoreApp.DataAccess\Seeder\Stores.json";

                        var storesData = File.ReadAllText(dir);

                        List<Store> stores = JsonConvert.DeserializeObject<List<Store>>(storesData);

                            var insertStoresQuery = ("INSERT INTO Stores (Id, [Name], [Image], TotalNumbeOfProducts, UserName )" +
                                        "VALUES (@Id, @Image, @Name, @TotalNumbeOfProducts, @UserName)");

                            command.CommandText = insertStoresQuery;
                        foreach (var store in stores)
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("Id", SqlDbType.NVarChar).Value = store.Id;
                            command.Parameters.Add("Image", SqlDbType.NVarChar).Value = store.Name;
                            command.Parameters.Add("Name", SqlDbType.NVarChar).Value = store.Image;
                            command.Parameters.Add("TotalNumbeOfProducts", SqlDbType.Int).Value = store.TotalNumberOfProducts;
                            command.Parameters.Add("UserName", SqlDbType.NVarChar).Value = store.UserName;

                            result = await command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Log errors
                Console.WriteLine(ex.Message);
            }
        }
    }
}
