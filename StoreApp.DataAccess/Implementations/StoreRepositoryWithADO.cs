using StoreApp.DataAccess.Interfaces;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StoreManagemnt.DataAccess.Implementations
{
    public class StoreRepositoryWithADO : IStoreRepository
    {
        private readonly string _connectionString;

        public StoreRepositoryWithADO()
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

        public async Task<bool> Add(Store store)
        {
            using (var connection = Connection)
            {
                var query = ("INSERT INTO Stores (Id, [Name], [Image], TotalNumbeOfProducts, [UserName] )" +
                    "VALUES (@Id, @Name, @Image, @TotalNumbeOfProducts, @UserName)");
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("Id", SqlDbType.NVarChar).Value = store.Id;
                command.Parameters.Add("Image", SqlDbType.NVarChar).Value = store.Image;
                command.Parameters.Add("Name", SqlDbType.NVarChar).Value = store.Name;
                command.Parameters.Add("TotalNumbeOfProducts", SqlDbType.Int).Value = store.TotalNumberOfProducts;
                command.Parameters.Add("UserName", SqlDbType.NVarChar).Value = store.UserName;

                var result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return result > 0;
            }

        }

        public async Task<bool> Update(string storeName, int productCount, string Id)
        {
            using (var connection = Connection)
            {
                var query = ("UPDATE Stores SET TotalNumbeOfProducts  = @TotalNumbeOfProducts, [Name] = @Name WHERE Id = @storeId");
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("storeId", SqlDbType.NVarChar).Value = Id;
                command.Parameters.Add("TotalNumbeOfProducts", SqlDbType.NVarChar).Value = productCount;
                command.Parameters.Add("Name", SqlDbType.NVarChar).Value = storeName;

                var result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return result > 0;
            }

        }

        public async Task<bool> Delete(string storeId)
        {
            using (var connection = Connection)
            {
                var query = "DELETE FROM Stores WHERE Id = @storeId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@storeId", SqlDbType.NVarChar).Value = storeId;

                var result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return result > 0;
            }

        }

        public async Task<ICollection<Store>> GetStoresByCustomerId(string userName)
        {
            using (var connection = Connection)
            {
                var query = "SELECT * FROM Stores WHERE UserName = @userName";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;

                var reader = command.ExecuteReader();

                List<Store> stores = new List<Store>();

                while (await reader.ReadAsync())
                {
                    var store = new Store
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Image = reader["Image"].ToString(),
                        TotalNumberOfProducts = Convert.ToInt32(reader["TotalNumbeOfProducts"]),
                        UserName = reader["UserName"].ToString()
                    };

                    stores.Add(store);
                }

                return stores;
            }
        }

        public async Task<ICollection<Store>> GetAll()
        {
            using (var connection = Connection)
            {
                var query = "SELECT * FROM Stores";

                SqlCommand command = new SqlCommand(query, connection);

                var reader = command.ExecuteReader();

                List<Store> stores = new List<Store>();

                while (await reader.ReadAsync())
                {
                    var store = new Store
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Image = reader["Image"].ToString(),
                        TotalNumberOfProducts = Convert.ToInt32(reader["TotalNumbeOfProducts"]),
                        UserName = reader["UserName"].ToString()
                    };

                    stores.Add(store);
                }

                return stores;
            }
        }

        public async Task<Store> GetStore(string storeId)
        {
            using (var connection = Connection)
            {
                var query = "SELECT * FROM Stores WHERE Id = @storeId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@storeId", SqlDbType.NVarChar).Value = storeId;

                var reader = command.ExecuteReader();

                Store store = new Store();

                while (await reader.ReadAsync())
                {
                    store.Id = reader["Id"].ToString();
                    store.Name = reader["Name"].ToString();
                    store.Image = reader["Image"].ToString();
                    store.TotalNumberOfProducts = Convert.ToInt32(reader["TotalNumbeOfProducts"]);
                    store.UserName = reader["UserName"].ToString();
                }

                return store;
            }
        }
    }
}