using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IndianArmyWeb.Core.IRepositories
{
    public interface IMenuUrlMasterRepository : IRepository<MenuUrlMaster>
    {
        IEnumerable<SelectListItem> GetUrlPrefixs();
    }
}
