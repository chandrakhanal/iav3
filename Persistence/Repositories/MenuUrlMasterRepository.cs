using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class MenuUrlMasterRepository : Repository<MenuUrlMaster>, IMenuUrlMasterRepository
    {
        public MenuUrlMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetUrlPrefixs()
        {
            List<SelectListItem> menuUrls = IASiteContext.MenuUrlMstr
                .OrderBy(n => n.MenuUrlId)
                .Where(n=>n.PageType == PageType.Dynamic)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.MenuUrlId.ToString(),
                    Text = n.UrlPrefix
                }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = null,
                Text = "No Content"
            };
            menuUrls.Insert(0, ddltip);
            return new SelectList(menuUrls, "Value", "Text");
        }
        public IASiteContext IASiteContext
        {
            get { return Context as IASiteContext; }
        }
    }
}