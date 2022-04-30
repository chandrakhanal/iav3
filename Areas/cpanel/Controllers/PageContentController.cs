using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.IO;
using IndianArmyWeb.Models;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.Infrastructure.Helpers.Routing;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Infrastructure.Helpers.FileDirectory;
using IndianArmyWeb.Infrastructure.Helpers.Editor;
using IndianArmyWeb.Infrastructure.Helpers.Account;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Extensions;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
  
    [SessionTimeout]
    [Authorize]
    [CSPLHeaders]
    public class PageContentController : Controller
    {
        // GET: PageContent
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Page List Accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                int roleid = UserAccountHelper.getCurrentUserRoleId();
                var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Read == true).Select(x => x.MenuId);
                var pageContent = uow.PageContentRepo.Find(x => MenuRole.Contains(x.MenuId));
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<PageContent>, List<PageContentIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<PageContent>, IEnumerable<PageContentIndxVM>>(pageContent).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: PageContent/Create
        public async Task<ActionResult> Create()
        {
            UserActivityHelper.SaveUserActivity("Page Create Option visited", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                int roleid = UserAccountHelper.getCurrentUserRoleId();
                var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Write == true).Select(x => (x.MenuId).ToString());
                var ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.ParentMenus = ParentMenus.Where(x => MenuRole.Contains(x.Value));
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: PageContent/Create
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Create(PageContentCrtVM objPageContCvm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PageContentCrtVM, PageContent>();
                });
                IMapper mapper = config.CreateMapper();
                PageContent CreateDto = mapper.Map<PageContentCrtVM, PageContent>(objPageContCvm);
                uow.PageContentRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Page saved", NotificationType.SUCCESS);
                return RedirectToAction("Create");
            }
        }

        // GET: PageContent/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                int roleid = UserAccountHelper.getCurrentUserRoleId();
                var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Write == true).Select(x => (x.MenuId).ToString());
                var ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.ParentMenus = ParentMenus.Where(x => MenuRole.Contains(x.Value));
                var pgContntMstr = uow.PageContentRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PageContent, PageContentUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                PageContentUpVM UpdateDto = mapper.Map<PageContent, PageContentUpVM>(await pgContntMstr);
                await uow.CommitAsync();
                return View(UpdateDto);
            }
        }

        // POST: PageContent/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Edit(PageContentUpVM objPageContUvm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PageContentUpVM, PageContent>();
                });
                IMapper mapper = config.CreateMapper();
                PageContent UpdateDTO = mapper.Map<PageContentUpVM, PageContent>(objPageContUvm);
                uow.PageContentRepo.Update(UpdateDTO);
                await uow.CommitAsync();
                this.AddNotification("Page updated", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var dataModal = await uow.PageContentRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.PageContentRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        public async Task<ActionResult> PagePreview(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var pageContent = await uow.PageContentRepo.GetByIdAsync(id);
                ViewBag.Content = pageContent.Content;
                await uow.CommitAsync();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ImageUpload(HttpPostedFileBase file)
        {
            string currentYear = DateTime.Now.Year.ToString();
            string currentMonth = DateTime.Now.ToString("MMM");
            string path = Path.Combine(ServerRootConsts.NEWS_CONTENT_ROOT, "images", currentYear, currentMonth);
            var location = TinyEditorHelper.SaveImageFile(Server.MapPath(path), file, path);
            return Json(new { location }, JsonRequestBehavior.AllowGet);
        }
    }
}
