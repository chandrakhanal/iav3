using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public interface IMenuControlVM
    {
        int MenuId { get; set; }
        int ParentId { get; set; }
        string SlugMenu { get; set; }
        string MenuName { get; set; }
        string HMenuName { get; set; }
        string PageTitle { get; set; }
        string PageHeading { get; set; }
        string HPageHeading { get; set; }
        bool IsVisible { get; set; }
        PositionType MenuType { get; set; }
        int MenuUrlId { get; set; }
        string UrlPrefix { get; set; }
        string Controller { get; set; }
        string Action { get; set; }
    }
    public class MenuControlVM
    {

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
        public PositionType MenuType { get; set; }
        public int? MenuUrlId { get; set; }
        //public virtual MenuUrlMaster MenuUrlMasters { get; set; }
        public string UrlPrefix { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public PageType PageType { get; set; }

        public bool ExternalLink { get; set; }
        public string ExternalUrl { get; set; }
    }

    #region Menu NotUse
    public class ParentMenu
    {
        public ParentMenu()=> iSubMenus = new List<SubMenu>();
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string SlugMenu { get; set; }
        public ICollection<SubMenu> iSubMenus { get; set; }
    }
    public class SubMenu
    {
        public SubMenu() => iSubSubMenu = new List<SubSubMenu>();
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string SlugMenu { get; set; }
        public ICollection<SubSubMenu> iSubSubMenu { get; set; }
    }
    public class SubSubMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string SlugMenu { get; set; }
    }
    #endregion
}