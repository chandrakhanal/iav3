using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Models;
using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IndianArmyWeb.DataContexts;
using System.Web;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class NewsArticleRepository : Repository<NewsArticle>, INewsArticleRepository
    {
        public NewsArticleRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<WhatsNewVM>> GetWhatsNewByCategory(NewsCategory Category)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Category", Category)
            };
            return await IASiteContext.Database.SqlQuery<WhatsNewVM>("USPNewsArticlList @Category", sqlParam).ToListAsync();
        }

        public int GetLatestNews(NewsCategory category)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Category", category),
            };
            return IASiteContext.Database.SqlQuery<int>("USPNewsArticlList @Category", sqlParam).Single();
        }
        public IASiteContext IASiteContext
        {
            get { return Context as IASiteContext; }
        }
    }
}