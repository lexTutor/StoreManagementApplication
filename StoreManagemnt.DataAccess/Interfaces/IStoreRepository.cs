using StoreManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagemnt.DataAccess
{
    public interface IStoreRepository
    {
        Task<bool> Add(Store store);
        Task<bool> Delete(string storeId);
        Task<bool> UpdateProducts(string storeId, int amount);
        Task<ICollection<Store>> GetAll(string customerId);
        Task<Store> GetStore(string storeId);
    }
}