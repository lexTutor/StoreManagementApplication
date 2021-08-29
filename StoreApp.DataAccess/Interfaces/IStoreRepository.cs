using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Interfaces
{
    public interface IStoreRepository
    {
        Task<bool> Add(Store store);
        Task<bool> Delete(string storeId);
        Task<bool> Update(string storeId, int productCount, string storeName);
        Task<ICollection<Store>> GetStoresByCustomerId(string customerId);
        Task<ICollection<Store>> GetAll();
        Task<Store> GetStore(string storeId);
    }
}
