using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Models;
using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndianArmyWeb.Core.IRepositories
{
    public interface INewsArticleRepository : IRepository<NewsArticle>
    {
        Task<IEnumerable<WhatsNewVM>> GetWhatsNewByCategory(NewsCategory Category);
    }
}
