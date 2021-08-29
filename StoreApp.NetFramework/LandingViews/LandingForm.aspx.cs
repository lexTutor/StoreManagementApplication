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
    public partial class LandingFormaspx : System.Web.UI.Page
    {
        private readonly IStoreRepository _storeRepository;
        public LandingFormaspx(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public async Task<ICollection<Store>> GetStores()
        {
            return await _storeRepository.GetAll();
            //return new List<Store>
            //{
            //    new Store
            //    {
            //        Name = "Jesus Store",
            //        TotalNumbeOfProducts= 3000,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "Lex's Store",
            //        TotalNumbeOfProducts= 2000,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "Row's Store",
            //        TotalNumbeOfProducts= 1500,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "Chi's Store",
            //        TotalNumbeOfProducts= 1000,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "G's Store",
            //        TotalNumbeOfProducts= 500,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "R's Store",
            //        TotalNumbeOfProducts= 1000,
            //        Image = "https://source.unsplash.com/random"
            //    } ,
            //    new Store
            //    {
            //        Name = "Jesus Store",
            //        TotalNumbeOfProducts= 3000,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "Lex's Store",
            //        TotalNumbeOfProducts= 2000,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "Row's Store",
            //        TotalNumbeOfProducts= 1500,
            //        Image = "https://source.unsplash.com/random"
            //    },
            //    new Store
            //    {
            //        Name = "Chi's Store",
            //        TotalNumbeOfProducts= 1000,
            //        Image = "https://source.unsplash.com/random"
            //    }
            }
        //}

    }
}