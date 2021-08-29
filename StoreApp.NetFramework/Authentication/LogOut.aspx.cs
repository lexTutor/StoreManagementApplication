using StoreApp.NetFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreApp.NetFramework.Authentication
{
    public partial class LogOut : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == HTTPMethods.GET.ToString())
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/");
                return;
            }

        }
    }
}