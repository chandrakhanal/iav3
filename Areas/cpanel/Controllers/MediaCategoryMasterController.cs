using AutoMapper;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Account;
using IndianArmyWeb.Infrastructure.Extensions;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [Authorize]
    [CSPLHeaders]
    [SessionTimeout]
    public class MediaCategoryMasterController : Controller
    {
        // GET: MediaCategoryMaster
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Media Category Pages Accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var mediaCtgry = await uow.MediaCategoryMstrRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MediaCategoryMaster>, List<MediaCategoryMaster>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MediaCategoryMaster>, IEnumerable<MediaCategoryMasterVM>>(mediaCtgry).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: MediaCategoryMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaCategoryMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(MediaCategoryMasterVM objmediaCtgryVm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMasterVM, MediaCategoryMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MediaCategoryMaster CreateDto = mapper.Map<MediaCategoryMasterVM, MediaCategoryMaster>(objmediaCtgryVm);
                uow.MediaCategoryMstrRepo.Add(CreateDto);
                await uow.CommitAsync();
                UserActivityHelper.SaveUserActivity("Media Category Added", Request.Url.ToString());
                this.AddNotification("Media Category created successfully", NotificationType.SUCCESS);
                return RedirectToAction("Create");
            }
        }

        // GET: MediaCategoryMaster/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var mediaCtgry = await uow.MediaCategoryMstrRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMaster, MediaCategoryMasterVM>();
                });
                IMapper mapper = config.CreateMapper();
                MediaCategoryMasterVM UpdateDto = mapper.Map<MediaCategoryMaster, MediaCategoryMasterVM>(mediaCtgry);
                uow.Commit();
                this.AddNotification("Media Category updated successfully", NotificationType.SUCCESS);
                return View(UpdateDto);
            }
        }

        // POST: MediaCategoryMaster/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MediaCategoryMasterVM objmediaCtgryVm)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaCategoryMasterVM, MediaCategoryMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MediaCategoryMaster UpdateDto = mapper.Map<MediaCategoryMasterVM, MediaCategoryMaster>(objmediaCtgryVm);
                uow.MediaCategoryMstrRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Media Category updated successfully", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var DeleteItem = await uow.MediaCategoryMstrRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.MediaCategoryMstrRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    UserActivityHelper.SaveUserActivity("Media Category Deleted", Request.Url.ToString());
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
