using IndianArmyWeb.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IndianArmyWeb.Models;
using System.Data.Entity;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class CSConfRepository : Repository<CSConf>, ICSConfRepository
    {
        public CSConfRepository(DbContext context) : base(context)
        {
        }
    }
}