using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public static class SiteLinkListHelper
    {
        public static int GetTopLevelParentId(IEnumerable<MenuControlVM> siteLinks)
        {

            return siteLinks.OrderBy(i => i.ParentId).Select(i => i.ParentId).FirstOrDefault();
        }

        public static bool SiteLinkHasChildren(IEnumerable<MenuControlVM> siteLinks, int id)
        {
            return siteLinks.Any(i => i.ParentId == id);
        }

        public static IEnumerable<MenuControlVM> GetChildSiteLinks(IEnumerable<MenuControlVM> siteLinks, int parentIdForChildren)
        {
            return siteLinks.Where(i => i.ParentId == parentIdForChildren)
                .OrderBy(i => i.SortOrder).ThenBy(i => i.MenuName);
        }
        #region Demo
        
        #endregion
    }
}