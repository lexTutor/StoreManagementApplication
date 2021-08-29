using StoreApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Security;

namespace StoreApp.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        MembershipUser Add(string username,string password,string email);
        bool Login(string email, string password);
        bool DeleteUser(string username, bool deleteAllRelatedData);
        ICollection<MembershipUser> GetAllUsers(int pageIndex, int pageSize, out int totalRecords);
        MembershipUser GetUser(string username, bool userIsOnline);
        string GetUserNameByEmail(string email);
        void UpdateUser(MembershipUser user);

    }
}
