using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using IndianArmyWeb.Persistence;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.Models;
using AutoMapper;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Menu;
using IndianArmyWeb.Infrastructure.Helpers.Account;
using IndianArmyWeb.Infrastructure.Extensions;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [UserMenuAttribute]
    [Authorize]
    [CSPLHeaders]
    [SessionTimeout]
    public class MenuItemMasterController : Controller
    {
        // GET: MenuItemMaster
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Menu Master Page Accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuItmMstr = await uow.MenuItemMstrRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MenuItemMaster>, List<MenuItemMasterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuItemMasterIndxVM>>(menuItmMstr).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: MenuItemMaster/Create
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ViewBag.ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.UrlPrefixs = uow.MenuUrlMstrRepo.GetUrlPrefixs();
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: MenuItemMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(MenuItemMasterCrtVM objMenuItmMstrCvm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMasterCrtVM, MenuItemMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MenuItemMaster CreateDto = mapper.Map<MenuItemMasterCrtVM, MenuItemMaster>(objMenuItmMstrCvm);
                uow.MenuItemMstrRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Data saved", NotificationType.SUCCESS);
                return RedirectToAction("Create");
            }
        }

        // GET: MenuItemMaster/Edit/5
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ViewBag.ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.UrlPrefixs = uow.MenuUrlMstrRepo.GetUrlPrefixs();

                var menuItemMstr = uow.MenuItemMstrRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuItemMasterUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                MenuItemMasterUpVM UpdateDto = mapper.Map<MenuItemMaster, MenuItemMasterUpVM>(menuItemMstr);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: MenuItemMaster/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MenuItemMasterUpVM objMenuItemUvm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMasterUpVM, MenuItemMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MenuItemMaster UpdateDto = mapper.Map<MenuItemMasterUpVM, MenuItemMaster>(objMenuItemUvm);
                uow.MenuItemMstrRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Data updated", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public async Task<ActionResult> MenuSortOrder()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ViewBag.menus = uow.MenuItemMstrRepo.GetParentMenus();
                await uow.CommitAsync();
                return View();
            }
        }
        [HttpPost]
        public ActionResult MenuSortOrder(int[] menuIds)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                int sortorder = 1;
                foreach (int id in menuIds)
                {
                    MenuItemMaster menuitem = uow.MenuItemMstrRepo.GetById(id);
                    menuitem.SortOrder = sortorder;

                    uow.MenuItemMstrRepo.UpdateMenuSortOrder(menuitem);
                    uow.Commit();
                    sortorder += 1;
                }
                //return View(uow.MenuItemMstrRepo.GetAll().OrderBy(p => p.SortOrder).ToList());
                return RedirectToAction("MenuSortOrder");
            }
        }
        [HttpPost]
        public JsonResult GetMenuSortData(int menuId)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuSortData = uow.MenuItemMstrRepo.Find(x => x.ParentId == menuId).OrderBy(x=>x.SortOrder);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MenuItemMaster>, List<MenuSortOrderVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var data = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuSortOrderVM>>(menuSortData).ToList();
                uow.Commit();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var dataModal = await uow.MenuItemMstrRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.MenuItemMstrRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    UserActivityHelper.SaveUserActivity("Menu Deleted", Request.Url.ToString());
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        //[HttpGet]
        //public ActionResult JsTreeDemo()
        //{
        //    return View();
        //}
        public ActionResult Nodes()
        {
            var nodes = TreeViewHelper.GetMenuTree().ToList();
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        
    }
}
