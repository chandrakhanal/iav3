using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.DataContexts
{
    public class DGRSiteDbInitSitelizer : DropCreateDatabaseIfModelChanges<IASiteContext>
    {
        protected override void Seed(IASiteContext context)
        {
            //var MenuUrlMaster = new List<MenuUrlMaster>()
            //{
            //    new MenuUrlMaster{MenuUrlId=1,UrlPrefix="HM",Controller="Home",Action="Index"},
            //    new MenuUrlMaster{MenuUrlId=2,UrlPrefix="HM",Controller="Home",Action="ContactUs"},
            //    new MenuUrlMaster{MenuUrlId=3,UrlPrefix="DGRL1",Controller="Dynamic",Action="DynamicL1"},
            //    new MenuUrlMaster{MenuUrlId=4,UrlPrefix="DGRL2",Controller="Dynamic",Action="DynamicL2"}
            //};
            //context.MenuUrlMstr.AddRange(MenuUrlMaster);
            //context.SaveChanges();
            //MenuUrlMaster.ForEach(s => context.MenuUrlMstr.Add(s));



            //var MenuItems = new List<MenuItemMaster>()
            //{

            //};
            //MenuItems.ForEach(s => context.MenuItemMstr.Add(s));
            //context.SaveChanges();


            base.Seed(context);
        }
    }
}