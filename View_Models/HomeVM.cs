using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public class HomeVM
    {
    }
    public class PageContentSearchVM
    {
        public int PageContentId { get; set; }
        public List<string> Content { get; set; }
        public int MenuId { get; set; }
    }
}