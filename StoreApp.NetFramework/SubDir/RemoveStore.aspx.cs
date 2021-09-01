using System;
using System.Web.UI;
using NLog;
using StoreApp.DataAccess.Interfaces;

namespace StoreApp.NetFramework.SubDir
{
    public partial class RemoveStore : Page
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public RemoveStore(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string storeId = Request.QueryString["storeId"];
                if (!string.IsNullOrWhiteSpace(storeId))
                {
                    _storeRepository.Delete(storeId).ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            finally 
            {
                Response.Redirect("~/SubDir/UserStores.aspx");
            }
        }
    }
}