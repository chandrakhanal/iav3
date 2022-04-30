using AutoMapper;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Persistence;
using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public static class HtmlHelperSiteMenu
    {
        #region Top Menu
        public static MvcHtmlString SiteMenuAsUnorderedList(this HtmlHelper helper, List<MenuControlVM> siteMenus)
        {
            if (siteMenus == null || siteMenus.Count == 0)
                return MvcHtmlString.Empty;
            var topLevelParentId = SiteLinkListHelper.GetTopLevelParentId(siteMenus);
            return MvcHtmlString.Create(buildMenuItems(siteMenus, topLevelParentId));
        }
        private static string buildMenuItems(List<MenuControlVM> siteLinks, int parentId)
        {
            var parentTag = new TagBuilder("ul");
            if (parentId == 0) {
                parentTag.Attributes.Add("class", "clearfix sf-menu");
                parentTag.Attributes.Add("id", "example");
            }
            
            var childSiteLinks = SiteLinkListHelper.GetChildSiteLinks(siteLinks, parentId);
            foreach (var siteLink in childSiteLinks)
            {
                var itemTag = new TagBuilder("li");                
                var anchorTag = new TagBuilder("a");
                var iTag = new TagBuilder("i");
                string urlpattern = HtmlHelperSiteMenu.GetUrlPattern(siteLinks, parentId, siteLink);
                anchorTag.MergeAttribute("href", urlpattern);

                if (siteLink.MenuId == 1)
                {
                    iTag.AddCssClass("fa fa-home");
                    anchorTag.InnerHtml = iTag.ToString(); //+ siteLink.MenuName;
                }
                else 
                    anchorTag.SetInnerText(siteLink.MenuName);

                itemTag.InnerHtml = anchorTag.ToString();
                if (SiteLinkListHelper.SiteLinkHasChildren(siteLinks, siteLink.MenuId))
                {
                    itemTag.Attributes.Add("class", "has-sub");
                    //anchorTag.MergeAttribute("href", "#");
                    itemTag.InnerHtml += buildMenuItems(siteLinks, siteLink.MenuId);
                }
                parentTag.InnerHtml += itemTag;
            }
            return parentTag.ToString();
        }
        public static string GetUrlPattern(List<MenuControlVM> siteLinks, int parentId, MenuControlVM subsiteLink)
        {
            string urlpattern="#";
            string DomainHost = "/";

            if (subsiteLink.ExternalLink)
            {
                urlpattern = subsiteLink.ExternalUrl;
                //return urlpattern;
            }
            else if (subsiteLink.MenuUrlId != null)
            {
                if (parentId == 0)
                {
                    if (subsiteLink.PageType == PageType.Static)
                    {
                        urlpattern = DomainHost + subsiteLink.Controller + "/" + subsiteLink.Action;
                    }
                    else if (subsiteLink.PageType == PageType.Dynamic)
                    {
                        urlpattern = DomainHost + subsiteLink.UrlPrefix + "/" + subsiteLink.SlugMenu;
                    }
                }
                if (parentId > 0)
                {
                    if (subsiteLink.PageType == PageType.Static)
                    {
                        urlpattern = DomainHost + subsiteLink.Controller + "/" + subsiteLink.Action;
                    }
                    else if (subsiteLink.PageType == PageType.Dynamic)
                    {
                        var parentsitelnk = siteLinks.FirstOrDefault(x => x.MenuId == parentId);
                        if (parentsitelnk != null)
                        {
                            urlpattern = DomainHost + subsiteLink.UrlPrefix + "/" + parentsitelnk.SlugMenu + "/" + subsiteLink.SlugMenu;
                        }
                    }
                }
            }
            return urlpattern;
        }
        #endregion

        #region Bottom Menu
        public static MvcHtmlString SiteMenuAsUnorderedListFooter(this HtmlHelper helper, List<MenuControlVM> siteMenus)
        {
            if (siteMenus == null || siteMenus.Count == 0)
                return MvcHtmlString.Empty;
            //var topLevelParentId = SiteLinkListHelper.GetTopLevelParentId(siteMenus);
            return MvcHtmlString.Create(buildMenuItemsFooter(siteMenus));
        }
        private static string buildMenuItemsFooter(List<MenuControlVM> siteLinks)
        {
            var allSiteLinks = GetAllMenuItems();
            var MainTag = new TagBuilder("ul");
            
            foreach (var siteLink in siteLinks)
            {
                var itemTag = new TagBuilder("li");

                var anchorTag = new TagBuilder("a");
                ////StringBuilder urlpattern = new StringBuilder();
                ////urlpattern = "<%= Url.Action('DynamicL1','Dynamic', new { parentSlug='d1-test2', childSlug='d-2-test-4'}) %>";
                //string urlpattern = "<%= Url.Action('DynamicL1','Dynamic', new { parentSlug='d1-test2', childSlug='d-2-test-4'}) %>";
                string urlpattern = HtmlHelperSiteMenu.GetUrlPatternBottom(allSiteLinks, siteLink);
                anchorTag.MergeAttribute("href", urlpattern);

                anchorTag.SetInnerText(siteLink.MenuName);
                //if (siteLink.OpenInNewWindow)
                //{
                //    anchorTag.MergeAttribute("target", "_blank");
                //}
                itemTag.InnerHtml = anchorTag.ToString();
                MainTag.InnerHtml += itemTag;
            }
            return MainTag.ToString();
        }
        public static string GetUrlPatternBottom(List<MenuControlVM> siteLinks, MenuControlVM subsiteLink)
        {
            string urlpattern = "#";
            string DomainHost = "http://localhost:58876/";
            //string DomainHost = "http://localhost:8028/";
            int parentId = subsiteLink.ParentId;

            if (subsiteLink.MenuUrlId != null)
            {
                if (parentId == 0)
                {
                    urlpattern = DomainHost + subsiteLink.UrlPrefix + "/" + subsiteLink.SlugMenu;
                }
                if (parentId > 0)
                {
                    var parentsitelnk = siteLinks.FirstOrDefault(x => x.MenuId == parentId);
                    if (parentsitelnk != null)
                    {
                        urlpattern = DomainHost + subsiteLink.UrlPrefix + "/" + parentsitelnk.SlugMenu + "/" + subsiteLink.SlugMenu;
                    }
                }
            }
            return urlpattern;
        }
        #endregion

        #region Utility
        private static List<MenuControlVM> GetAllMenuItems()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuItmMstr = uow.MenuItemMstrRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Models.MenuItemMaster, MenuControlVM>()
                    .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
                    .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
                    .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
                    .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType));
                });
                IMapper mapper = config.CreateMapper();
                var SiteMenu = mapper.Map<IEnumerable<Models.MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstr).ToList();
                uow.Commit();
                return SiteMenu;
            }
        }
        #endregion
    }
}