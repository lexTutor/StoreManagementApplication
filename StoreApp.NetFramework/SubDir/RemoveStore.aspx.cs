using System;
using System.Web.UI;
using StoreApp.DataAccess.Interfaces;

namespace StoreApp.NetFramework.SubDir
{
    public partial class RemoveStore : Page
    {
        private readonly IStoreRepository _storeRepository;

        public RemoveStore(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string storeId = Request.QueryString["storeId"];
            if (!string.IsNullOrWhiteSpace(storeId))
            {
            _storeRepository.Delete(storeId).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            Response.Redirect("~/SubDir/UserStores.aspx");
        }
    }
}