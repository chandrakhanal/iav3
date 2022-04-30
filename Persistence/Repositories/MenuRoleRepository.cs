using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class MenuRoleRepository : Repository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(DbContext context) : base(context)
        {
        }

        //public void c()
        //{
        //    DGRSiteContext.MenuRoles.
        //}
        //public void AddOrUpdateData(IEnumerable<MenuRole> entities, int roleId)
        //{
        //    var menurolelist = DGRSiteContext.MenuRoles.Where(x => x.RoleId == roleId);
        //    if (menurolelist != null)
        //    {

                
        //    }
            
        //    DGRSiteContext.MenuRoles.AddRange(entities);
        //}
        public IASiteContext IASiteContext
        {
            get { return Context as IASiteContext; }
        }
    }
}