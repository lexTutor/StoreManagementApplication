using StoreApp.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreApp.Models;
using StoreApp.NetFramework.Helpers;
using NLog;

namespace StoreApp.NetFramework.SubDir
{
    public partial class UpdateStore : System.Web.UI.Page
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public UpdateStore(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == HTTPMethods.POST.ToString())
            {
                try
                {
                   var res = _storeRepository.Update(StoreName.Value, 
                       Convert.ToInt32(ProductCount.Value), StoreId.Value)
                        .ConfigureAwait(false).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
                Response.Redirect("~/SubDir/UserStores");
            }
            else
            {
                var id = Request.Url.Segments[2];

                if (string.IsNullOrWhiteSpace(id))
                {
                    Response.Redirect("~/SubDir/UserStores");
                    return;
                }
                try
                {
                    Store store = _storeRepository.GetStore(id).Result;
                    StoreName.Value = store.Name;
                    ProductCount.Value = store.TotalNumberOfProducts.ToString();
                    StoreId.Value = store.Id;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }

               
            }
        }
    }
}