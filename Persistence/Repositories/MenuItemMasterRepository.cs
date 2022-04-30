using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
//using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class MenuItemMasterRepository : Repository<MenuItemMaster>, IMenuItemMasterRepository
    {
        public MenuItemMasterRepository(DbContext context) : base(context)
        {
        }
        public void UpdateMenuSortOrder(MenuItemMaster menuItem)
        {
            IASiteContext.MenuItemMstr.Attach(menuItem);
            IASiteContext.Entry(menuItem).Property(x => x.SortOrder).IsModified = true;
        }
        public IEnumerable<SelectListItem> GetParentMenus()
        {
            List<SelectListItem> parentMenus = IASiteContext.MenuItemMstr
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }
        
        //public IEnumerable<View_Models.MenuControlVM> GetMenuControl()
        //{
        //    List<View_Models.MenuControlVM> menulist = IASiteContext.MenuItemMstr
        //        .OrderBy(n => n.MenuName)
        //        .Select(n =>
        //        new View_Models.MenuControlVM
        //        {
        //            MenuId = n.MenuId,
        //            ParentId = n.ParentId,
        //            MenuName = n.MenuName,
        //            HMenuName = n.HMenuName,
        //            //order
        //            SlugMenu = n.SlugMenu,
        //            //url
        //            MenuUrlId = n.MenuUrlId,
        //            MenuUrlMasters = n.MenuUrlMasters
        //        }).ToList();
        //    ret
        //}
        public IASiteContext IASiteContext
        {
            get { return Context as IASiteContext; }
        }
    }
}