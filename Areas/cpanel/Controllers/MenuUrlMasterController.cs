using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Persistence;
using IndianArmyWeb.Areas.cpanel.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IndianArmyWeb.Models;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Account;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = "Admin")]
    [OutputCache]
    [SessionTimeout]
    public class MenuUrlMasterController : Controller
    {
        // GET: MenuUrlMaster
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Menu URL Option visited", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var menuUrlMstr = await uow.MenuUrlMstrRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MenuUrlMaster>, List<MenuUrlMasterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MenuUrlMaster>, IEnumerable<MenuUrlMasterIndxVM>>(menuUrlMstr).ToList();
                //await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: MenuUrlMaster/Create
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                //ViewBag.
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: MenuUrlMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(MenuUrlMasterCrtVM objMenuUrlMstrCvm)
        {
            using(var uow=new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuUrlMasterCrtVM, MenuUrlMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MenuUrlMaster CreateDto = mapper.Map<MenuUrlMasterCrtVM, MenuUrlMaster>(objMenuUrlMstrCvm);
                uow.MenuUrlMstrRepo.Add(CreateDto);
                await uow.CommitAsync();
                UserActivityHelper.SaveUserActivity("New URL Saved", Request.Url.ToString());
                return RedirectToAction("Create");
            }
        }

        // GET: MenuUrlMaster/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuUrlMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuUrlMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuUrlMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
