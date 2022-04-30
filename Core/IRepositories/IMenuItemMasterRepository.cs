using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IndianArmyWeb.Core.IRepositories
{
    public interface IMenuItemMasterRepository : IRepository<MenuItemMaster>
    {
        IEnumerable<SelectListItem> GetParentMenus();
        void UpdateMenuSortOrder(MenuItemMaster menuItem);
    }
}
