using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IndianArmyWeb.Infrastructure.Constants;

namespace IndianArmyWeb.Models
{
    public class MenuUrlMaster
    {
        [Key]
        public int MenuUrlId { get; set; }
        public string UrlPrefix { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public PageType PageType { get; set; }
    }
}