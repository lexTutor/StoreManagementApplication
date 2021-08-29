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
        public StoreDetails(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public async Task<Store> GetStore([RouteData] string storeId)
        {
            if (string.IsNullOrWhiteSpace(storeId))
            {
                Response.Redirect("~/LandingViews/LandingForm");
            }
            return await _storeRepository.GetStore(storeId);
        }
    }
}