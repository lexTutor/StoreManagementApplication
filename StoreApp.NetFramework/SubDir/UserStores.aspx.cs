using NLog;
using StoreApp.DataAccess.Interfaces;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace StoreApp.NetFramework.SubDir
{
    public partial class UserStores : System.Web.UI.Page
    {
        private readonly IStoreRepository _storeRepository; 
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public UserStores(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public async Task<ICollection<Store>> GetUserStores()
        {
            try
            {
                var userName = HttpContext.Current.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(userName))
                {
                    Response.Redirect("~/LandingViews/LandingForm");
                }
                return await _storeRepository.GetStoresByCustomerId(userName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new List<Store>();
            }


        }
    }
}