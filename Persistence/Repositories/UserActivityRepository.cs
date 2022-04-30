using IndianArmyWeb.Persistence.Repositories;
using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class UserActivityRepository: Repository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(DbContext context) : base(context)
        {
        }
    }
}