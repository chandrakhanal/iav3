using AutoMapper;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public static class TreeViewHelper
    {
        public static IEnumerable<MenuTreeViewVM> GetMenuTree()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuItmMstr = uow.MenuItemMstrRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuTreeViewVM>()
                    //.ForMember(dest => dest.id, opts => opts.MapFrom(src => src.MenuId == 0 ? "#" : src.MenuId.ToString()))
                    .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.MenuId.ToString()))
                    .ForMember(dest => dest.parent, opts => opts.MapFrom(src => src.ParentId.ToString()))
                    .ForMember(dest => dest.text, opts => opts.MapFrom(src => src.MenuName))
                    .ForMember(dest => dest.icon, opts => opts.MapFrom(src => src.MenuUrlId == null ? "fa fa-folder" : "fa fa-file-code"))
                    .ForMember(dest => dest.disabled, opts => opts.MapFrom(src => src.MenuUrlId == null ? true : false));
                    //.ForMember(dest => dest.icon, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType));
                });
                IMapper mapper = config.CreateMapper();
                var MenuTree = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuTreeViewVM>>(menuItmMstr).ToList();
                MenuTreeViewVM menuRootValue = new MenuTreeViewVM()
                {
                    id = "0",
                    parent = "#",
                    text = "Root",
                    icon = "fa fa-folder",
                    opened=true,
                    selected=true,
                    disabled = true
                };
                MenuTree.Add(menuRootValue);
                uow.Commit();
                return MenuTree;
            }
        }
        
    }
}