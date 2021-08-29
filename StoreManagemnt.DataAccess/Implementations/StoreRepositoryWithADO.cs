using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StoreManagemnt.DataAccess.Implementations
{
    public class StoreRepositoryWithADO : IStoreRepository
    {
        private static SqlConnection GetConnection()
        {
            string connect = @"Data Source= .;Initial Catalog=BankApp;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connect);
            return myConnection;
        }

        public async Task<bool> Add(Store store)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = ("INSERT INTO Stores values (Id = @Id, Name = @name, TotalNumbeOfProducts = @TotalNumbeOfProducts, UserId = @userId)");
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("Id", SqlDbType.NVarChar).Value = store.Id;
                command.Parameters.Add("Name", SqlDbType.NVarChar).Value = store.Name;
                command.Parameters.Add("TotalNumbeOfProducts", SqlDbType.NVarChar).Value = store.TotalNumbeOfProducts;
                command.Parameters.Add("UserId", SqlDbType.NVarChar).Value = store.UserId;

                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }

        }

        public async Task<bool> UpdateProducts(string storeId, int amount)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = ("UPDATE Stores SET Amount = @amount WHERE Id = @storeId");
                SqlCommand command = new SqlCommand(query, connection);

                IDataParameter amountValue, storeIdValue;

                amountValue = new SqlParameter
                {
                    Value = amount,
                    SqlDbType = SqlDbType.NVarChar
                };

                storeIdValue = new SqlParameter
                {
                    Value = storeId,
                    SqlDbType = SqlDbType.NVarChar
                };

                var @params = new IDataParameter[] { amountValue, storeIdValue };
                command.Parameters.AddRange(@params);

                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }

        }

        public async Task<bool> Delete(string storeId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = "DELETE FROM Stores WHERE Id = @storeId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("Id", SqlDbType.NVarChar).Value = storeId;

                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }

        }

        public async Task<ICollection<Store>> GetAll(string customerId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = "SELECT * FROM Stores WHERE UserId = @customerId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("Id", SqlDbType.NVarChar).Value = customerId;

                var reader = command.ExecuteReader();

                List<Store> stores = new List<Store>();

                while (await reader.ReadAsync())
                {
                    var store = new Store
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        TotalNumbeOfProducts = Convert.ToInt32(reader["TotalNumbeOfProducts"]),
                        UserId = reader["UserId"].ToString()
                    };

                    stores.Add(store);
                }

                return stores;
            }
        }

        public async Task<Store> GetStore(string storeId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = "SELECT * FROM Stores WHERE Id = @storeId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("Id", SqlDbType.NVarChar).Value = storeId;

                var reader = command.ExecuteReader();

                Store store = new Store();

                while (await reader.ReadAsync())
                {
                    store.Id = reader["Id"].ToString();
                    store.Name = reader["Name"].ToString();
                    store.TotalNumbeOfProducts = Convert.ToInt32(reader["TotalNumbeOfProducts"]);
                    store.UserId = reader["UserId"].ToString();
                }

                return store;
            }
        }
    }
}