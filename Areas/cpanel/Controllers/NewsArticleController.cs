using AutoMapper;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Infrastructure.Extensions;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Editor;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [CSPLHeaders]
    [SessionTimeout]
    [Authorize]
    public class NewsArticleController : Controller
    {
        // GET: NewsArticle
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var newsArticle = await uow.NewsArticleRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsArticleIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsArticleIndxVM>>(newsArticle).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: NewsArticle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsArticle/Create
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Create(NewsArticleCrtVM objNewsArtclCvm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NewsArticleCrtVM, NewsArticle>();
                });
                IMapper mapper = config.CreateMapper();
                NewsArticle CreateDto = mapper.Map<NewsArticleCrtVM, NewsArticle>(objNewsArtclCvm);
                uow.NewsArticleRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Data saved", NotificationType.SUCCESS);
                return RedirectToAction("Create");
            }
        }

        // GET: NewsArticle/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var newsArticle = await uow.NewsArticleRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NewsArticle, NewsArticleUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                NewsArticleUpVM UpdateDto = mapper.Map<NewsArticle, NewsArticleUpVM>(newsArticle);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: NewsArticle/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Edit(NewsArticleUpVM objNewsArtclUvm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NewsArticleUpVM, NewsArticle>();
                });
                IMapper mapper = config.CreateMapper();
                NewsArticle UpdateDto = mapper.Map<NewsArticleUpVM, NewsArticle>(objNewsArtclUvm);
                uow.NewsArticleRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Data updated", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var DeleteItem = await uow.NewsArticleRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.NewsArticleRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
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
