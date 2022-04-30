using AutoMapper;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Account;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using IndianArmyWeb.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [Authorize]
    [CSPLHeaders]
    [SessionTimeout]
    public class UserActivityController : Controller
    {
        // GET: Admin/UserActivity
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Audit log accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var userActivity = await uow.UserActivityRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<UserActivity>, List<UserActivityVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<UserActivity>, IEnumerable<UserActivityVM>>(userActivity).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: Admin/UserActivity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/UserActivity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserActivity/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/UserActivity/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/UserActivity/Edit/5
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

        // GET: Admin/UserActivity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/UserActivity/Delete/5
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
