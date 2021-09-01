using NLog;
using StoreApp.DataAccess.Interfaces;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreApp.NetFramework.LandingViews
{
    public partial class LandingForm : System.Web.UI.Page
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public LandingForm(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public async Task<ICollection<Store>> GetStores()
        {
            try
            {
                return await _storeRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.Message);
                return new List<Store>();
            }
        }
    }
}