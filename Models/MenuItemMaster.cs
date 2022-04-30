using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IndianArmyWeb.Infrastructure.Constants;

namespace IndianArmyWeb.Models
{
    public class MenuItemMaster
    {
        [Key]
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public string SlugMenu { get; set; }
        public int SortOrder { get; set; }
        public string MenuName { get; set; }
        public string HMenuName { get; set; }
        public string PageTitle { get; set; }
        public string PageHeading { get; set; }
        public string HPageHeading { get; set; }
        public bool IsVisible { get; set; }
        public PositionType PositionType { get; set; }
        public Layout Layout { get; set; }
        public bool ExternalLink { get; set; }
        public string ExternalUrl { get; set; }
        public int? MenuUrlId { get; set; }
        public virtual MenuUrlMaster MenuUrlMasters { get;set;}
        

        //public string HPageTitle { get; set; }
        //public string MenuPrefix { get; set; }
        //public int TempleteId { get; set; }
        //public bool Active { get; set; }
        //public int Menutype { get; set; }
    }
}