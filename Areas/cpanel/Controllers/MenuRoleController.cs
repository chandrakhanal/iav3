using AutoMapper;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Extensions;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IndianArmyWeb.Infrastructure.Filters;
namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    [SessionTimeout]
    public class MenuRoleController : Controller
    {
        private ApplicationRoleManager _roleManager;
        public MenuRoleController()
        {
        }
        public MenuRoleController(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        
        // GET: Admin/MenuRoles/Create
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ViewBag.Roles = RoleManager.Roles.Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
                //ViewBag.UrlPrefixs = uow.MenuUrlMstrRepo.GetUrlPrefixs();
                await uow.CommitAsync();
                return View();
            }
        }
        
        // POST: Admin/MenuRoles/Create
        [HttpPost]
        public ActionResult Create(MenuRoleCrtVM objMenuRoleCvm, int[] selChkbxMenuIds, int[] selChkRead, int[] selChkWrite)
        {
            try
            {
                //bool a=Convert.ToBoolean(form["selChkbxSelected2"].ToString());
                using (var uow = new UnitOfWork(new IASiteContext()))
                {
                    if (selChkbxMenuIds != null)
                    {
                        int cn = selChkbxMenuIds.Count();
                        List<MenuRole> objmenuRoleList = new List<MenuRole>();
                        for (int i = 0; i < cn; i++)
                        {
                            MenuRole objMenu = new MenuRole();
                            objMenu.RoleId = objMenuRoleCvm.RoleId;
                            objMenu.MenuId = selChkbxMenuIds[i];
                            if (selChkRead != null)
                            {
                                if (selChkRead.Contains(selChkbxMenuIds[i])) objMenu.Read = true;
                                else objMenu.Read = false;
                            }
                            if (selChkWrite != null)
                            {
                                if (selChkWrite.Contains(selChkbxMenuIds[i])) objMenu.Write = true;
                                else objMenu.Write = false;
                            }
                            objmenuRoleList.Add(objMenu);
                        }
                        
                        //var menurolelist=uow.MenuRoleRepo.Find(x=>selChkbxMenuIds.Contains(x.MenuId)).ToList().ForEach(f => f.MenuId = 1);
                        //IEnumerable<MenuRole> list1 = uow.MenuRoleRepo.Find(x => x.RoleId == 1);
                        //var list2 = uow.MenuRoleRepo.Find(c => c.MenuId == 1).ToList().ForEach(cc => new MenuRole{ RoleId=cc.RoleId, MenuId=cc.MenuId, Read = true, Write = true});
                        //var list2 = uow.MenuRoleRepo.Find(c => c.MenuId == 1).ToList().ForEach(cc => cc.Read = true);
                        var menurolelist = uow.MenuRoleRepo.Find(x => x.RoleId == objMenuRoleCvm.RoleId);
                        if(menurolelist!=null)
                        {
                            uow.MenuRoleRepo.RemoveRange(menurolelist);
                            uow.Commit();
                        }
                        uow.MenuRoleRepo.AddRange(objmenuRoleList);
                        uow.Commit();
                    }
                    return RedirectToAction("Create");
                }
            }
            finally { }
        }
        
        [HttpPost]
        public JsonResult GetMenuViewList(int roleId)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuViewData = uow.MenuItemMstrRepo.GetAll().OrderBy(x => x.MenuName).ToList();
                var menurole = uow.MenuRoleRepo.Find(x => x.RoleId == roleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuRoleMenuitem>();
                });
                IMapper mapper = config.CreateMapper();
                var data = mapper.Map<List<MenuItemMaster>, List<MenuRoleMenuitem>>(menuViewData);

                if (menurole != null)
                {
                    foreach (var mr in menurole)
                    {
                        var itemToChange = data.First(d => d.MenuId == mr.MenuId);
                        itemToChange.Read = mr.Read;
                        itemToChange.Write = mr.Write;
                    }
                }
                
                uow.Commit();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}
