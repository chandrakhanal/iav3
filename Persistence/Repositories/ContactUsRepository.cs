using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IndianArmyWeb.Persistence.Repositories
{
    public class ContactUsRepository: Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(DbContext context) : base(context)
        {
        }
    }
}