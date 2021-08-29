﻿using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using StoreApp.DataAccess.Implementations;
using StoreApp.DataAccess.Interfaces;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using StoreManagemnt.DataAccess.Implementations;
using StoreApp.DataAccess.Seeder;
using StoreApp.NetFramework.Services;

namespace StoreApp.NetFramework
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var container = this.AddUnity();

            EntitySeeder.Seed().Wait();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IStoreRepository, StoreRepositoryWithADO>();
            container.RegisterType<IImageService, ImageService>();


            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterCustomRoutes(RouteTable.Routes);
        }

        void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("StoreById", "Store/{storeId}", "~/LandingViews/StoreDetails.aspx");
            routes.MapPageRoute("StoresByUserName", "UserStores", "~/UserStores.aspx");
            routes.MapPageRoute("UpdateStoreDetails", "UpdateStore/{storeId}", "~/SubDir/UpdateStore.aspx");
            routes.MapPageRoute("RemoveStore", "RemoveStore/{storeId}", "~/SubDir/RemoveStore.aspx");

        }
    }
}