using NLog;
using StoreApp.DataAccess.Interfaces;
using StoreApp.Models;
using System;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace StoreApp.NetFramework.LandingViews
{
    public partial class StoreDetails : System.Web.UI.Page
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public StoreDetails(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public async Task<Store> GetStore([RouteData] string storeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(storeId))
                {
                    Response.Redirect("~/LandingViews/LandingForm");
                }
                return await _storeRepository.GetStore(storeId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new Store();
            }
        }
    }
}