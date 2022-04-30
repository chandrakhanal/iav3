using AutoMapper;
using IndianArmyWeb.View_Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IndianArmyWeb.Infrastructure.Constants;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public class SiteMenuManager
    {
        public List<MenuControlVM> GetMenuItems(PositionType positionType=PositionType.Top)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuItmMstr = uow.MenuItemMstrRepo.GetAll().Where(x => x.IsVisible == true && x.PositionType == positionType);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuControlVM>()
                    .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
                    .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
                    .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
                    .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType));
                });
                IMapper mapper = config.CreateMapper();
                var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstr).ToList();
                uow.Commit();
                return SiteMenu;
            }
        }
        public List<MenuControlVM> GetMenuItems(IEnumerable<MenuItemMaster> menuItmMstrs)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuControlVM>()
                    .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
                    .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
                    .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
                    .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType));
                });
                IMapper mapper = config.CreateMapper();
                var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstrs).ToList();
                uow.Commit();
                return SiteMenu;
            }
        }
        #region Old Code
        //public List<MenuControlVM> GetMenuItems2()
        //{
        //    using (var uow = new UnitOfWork(new DGRSiteContext()))
        //    {
        //        var menuItmMstr = uow.MenuItemMstrRepo.GetAll();
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<MenuItemMaster, MenuControlVM>()
        //            .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
        //            .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
        //            .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
        //            .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType));
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstr).ToList();
        //        uow.Commit();
        //        return SiteMenu;
        //    }
        //}
        #endregion
    }
}