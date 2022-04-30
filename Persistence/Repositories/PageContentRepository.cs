using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class PageContentRepository : Repository<PageContent>, IPageContentRepository
    {
        public PageContentRepository(DbContext context) : base(context)
        {
        }
    }
}