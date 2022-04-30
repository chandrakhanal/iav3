using AutoMapper;
using IndianArmyWeb.Areas.cpanel.View_Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Account;
using IndianArmyWeb.Infrastructure.Extensions;
using IndianArmyWeb.Infrastructure.Helpers.FileDirectory;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [Authorize]
    [CSPLHeaders]
    [SessionTimeout]
    public class MediaGalleryController : Controller
    {
        // GET: MediaGallery

        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Media Gallery Accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var mediaGalry = await uow.MediaGalleryRepo.FindAsync(x => x.ArchiveDate >= DateTime.Now);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MediaGallery>, List<MediaGalleryIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<MediaGalleryIndxVM>>(mediaGalry).ToList();
                return View(indexDto);
            }
        }
        public async Task<ActionResult> Archives()
        {
            UserActivityHelper.SaveUserActivity("Media Gallery Accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var mediaGalry = await uow.MediaGalleryRepo.FindAsync(x => x.ArchiveDate <= DateTime.Now);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MediaGallery>, List<MediaGalleryIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<MediaGalleryIndxVM>>(mediaGalry).ToList();
                return View(indexDto);
            }
        }
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var mediaGalry = await uow.MediaGalleryRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaGallery, MediaGalleryIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<MediaGallery, MediaGalleryIndxVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_ShowMediaFiles", showMediaDto);
            }
        }
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ViewBag.MediaCategories = uow.MediaCategoryMstrRepo.GetMediaCategories();
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: MediaGallery/Create
        [HttpPost]
        public async Task<ActionResult> Create(MediaGalleryCrtVM objMediaGalryCvm, HttpPostedFileBase[] Files)
        {
            //Ensure model state is valid
            string path = ServerRootConsts.MEDIA_ROOT;
            objMediaGalryCvm.iMediaFiles = new List<MediaFile>();
            Boolean cond = false;
            foreach (var file in Files)
            {
                cond = false;

                if (CustomConst.GetUploadDirectory(file.FileName) != "InvalidFile")
                {
                    path = CustomConst.GetUploadDirectory(file.FileName);
                    cond = true;

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        Guid guid = Guid.NewGuid();
                        file.SaveAs(Server.MapPath(path + guid + Path.GetExtension(fileName)));
                        if (CheckFile(Server.MapPath(path + guid + Path.GetExtension(fileName))))  //CHECK MAGIC NUMBERS
                        {
                            MediaFile objMediaFile = new MediaFile()
                            {
                                GuId = guid,
                                FileName = fileName,
                                Extension = Path.GetExtension(fileName),
                                FilePath = path + guid + Path.GetExtension(fileName)
                            };

                            //file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                            objMediaGalryCvm.iMediaFiles.Add(objMediaFile);
                            this.AddNotification(objMediaFile.FilePath, NotificationType.INFO);
                        }
                        else
                        {
                            this.AddNotification("Invalid file type", NotificationType.WARNING);
                            cond = false;
                        }
                    }                  

                }
                else
                {
                    this.AddNotification("Invalid file type", NotificationType.WARNING);
                }

                if (cond == true)
                {
                    //if (file != null && file.ContentLength > 0)
                    //{
                    //    var fileName = Path.GetFileName(file.FileName);
                    //    Guid guid = Guid.NewGuid();
                    //    MediaFile objMediaFile = new MediaFile()
                    //    {
                    //        GuId = guid,
                    //        FileName = fileName,
                    //        Extension = Path.GetExtension(fileName),
                    //        FilePath = path + guid + Path.GetExtension(fileName)
                    //    };

                    //    file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                    //    objMediaGalryCvm.iMediaFiles.Add(objMediaFile);
                    //}
                }
            }
            if (cond == true)
            {
                using (var uow = new UnitOfWork(new IASiteContext()))
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MediaGalleryCrtVM, MediaGallery>();
                    });
                    IMapper mapper = config.CreateMapper();
                    MediaGallery CreateDto = mapper.Map<MediaGalleryCrtVM, MediaGallery>(objMediaGalryCvm);
                    uow.MediaGalleryRepo.Add(CreateDto);
                    await uow.CommitAsync();
                    UserActivityHelper.SaveUserActivity("Media Gallery created", Request.Url.ToString());
                    this.AddNotification("File(s) added to the Media Gallery", NotificationType.SUCCESS);
                    return RedirectToAction("Create");
                }
            }
            else
            {
                this.AddNotification("File(s) could not be added to the Media Gallery", NotificationType.ERROR);
                return RedirectToAction("Create");
            }   

        }

        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ViewBag.MediaCategories = uow.MediaCategoryMstrRepo.GetMediaCategories();

                var mediaGalry = await uow.MediaGalleryRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaGallery, MediaGalleryUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                MediaGalleryUpVM UpdateDto = mapper.Map<MediaGallery, MediaGalleryUpVM>(mediaGalry);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: MediaGallery/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MediaGalleryUpVM objMediaGalryUvm, HttpPostedFileBase[] Files)
        {
            string path = ServerRootConsts.MEDIA_ROOT;
            
            //objMediaGalryUvm.iMediaFiles = new List<MediaFile>();
            Boolean cond = false;
            foreach (var file in Files)
            {
                cond = false;
                if (CustomConst.GetUploadDirectory(file.FileName) != "InvalidFile")
                {
                    path = CustomConst.GetUploadDirectory(file.FileName);
                    cond = true;

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        Guid guid = Guid.NewGuid();
                        file.SaveAs(Server.MapPath(path + guid + Path.GetExtension(fileName)));
                        if (CheckFile(Server.MapPath(path + guid + Path.GetExtension(fileName))))  //CHECK MAGIC NUMBERS
                        {
                            MediaFile objMediaFile = new MediaFile()
                            {
                                GuId = guid,
                                FileName = fileName,
                                Extension = Path.GetExtension(fileName),
                                FilePath = path + guid + Path.GetExtension(fileName)
                            };

                            //file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                            objMediaGalryUvm.iMediaFiles.Add(objMediaFile);
                            this.AddNotification(objMediaFile.FilePath, NotificationType.INFO);
                        }
                        else
                        {
                            this.AddNotification("Invalid file type", NotificationType.WARNING);
                            cond = false;
                        }
                    }

                }
                else
                {
                    this.AddNotification("Invalid file type", NotificationType.WARNING);
                }
                //if (cond == true)
                //{
                //    if (file != null && file.ContentLength > 0)
                //    {
                //        var fileName = Path.GetFileName(file.FileName);
                //        Guid guid = Guid.NewGuid();
                //        MediaFile objMediaFile = new MediaFile()
                //        {
                //            GuId = guid,
                //            FileName = fileName,
                //            Extension = Path.GetExtension(fileName),
                //            FilePath = path + guid + Path.GetExtension(fileName),
                //            MediaGalleryId = objMediaGalryUvm.MediaGalleryId
                //        };

                //        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                //        objMediaGalryUvm.iMediaFiles.Add(objMediaFile);
                //    }
                //}
            }

            if (cond == true)
            {
                using (var uow = new UnitOfWork(new IASiteContext()))
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MediaGalleryUpVM, MediaGallery>();
                    });
                    IMapper mapper = config.CreateMapper();
                    MediaGallery UpdateDto = mapper.Map<MediaGalleryUpVM, MediaGallery>(objMediaGalryUvm);
                    uow.MediaGalleryRepo.UpdateRecord(UpdateDto);
                    await uow.CommitAsync();
                    this.AddNotification("Gallery updated", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                this.AddNotification("File(s) could not be added to the Media Gallery", NotificationType.ERROR);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var DeleteItem = await uow.MediaGalleryRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var item in DeleteItem.iMediaFiles)
                    {
                        //String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
                        String path = Server.MapPath(item.FilePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    uow.MediaGalleryRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    UserActivityHelper.SaveUserActivity("Media Gallery Deleted", Request.Url.ToString());
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        public static bool CheckFile(string path)
        {
            string ext = null;
            string msg = "";
            bool retMsg = false;
            string[] file_hexa_signature = { "25-50-44-46-2D-31-2E", "50-4B-03-04-14-00-06", "D0-CF-11-E0-A1-B1-1A", "47-49-46-38-39-61-20", "FF-D8-FF-E0-00-10-4A", "89-50-4E-47-0D-0A-1A" };
            
            if (path != null && path !="")
            {
                //string str = Path.GetFileName(file.FileName);
                //string path = Path.Combine(Server.MapPath("~/Docs"), Path.GetFileName(file.FileName));
                //file.SaveAs(path);
                BinaryReader reader = new BinaryReader(new FileStream(Convert.ToString(path), FileMode.Open, FileAccess.Read, FileShare.None));
                reader.BaseStream.Position = 0x0;     // The offset you are reading the data from
                byte[] data = reader.ReadBytes(0x10); // Read 16 bytes into an array         
                string data_as_hex = BitConverter.ToString(data);
                reader.Close();

                // substring to select first 20 characters from hexadecimal array
                string fUpload = data_as_hex.Substring(0, 20);
                string output = null;
                bool isGeniun = false;
                switch (fUpload)
                {
                    case "25-50-44-46-2D-31-2E":
                        output = "pdf";
                        isGeniun = true;
                        break;
                    case "50-4B-03-04-14-00-06":
                        output = "word-excel-ppt";
                        isGeniun = true;
                        break;
                    case "D0-CF-11-E0-A1-B1-1A":
                        output = "doc-xls-ppt";
                        isGeniun = true;
                        break;
                    case "47-49-46-38-39-61-20":
                        output = "gif";
                        isGeniun = true;
                        break;
                    case "FF-D8-FF-E0-00-10-4A":
                        output = "jpeg-jpg";
                        isGeniun = true;
                        break;
                    case "89-50-4E-47-0D-0A-1A":
                        output = "png";
                        isGeniun = true;
                        break;
                    case null:
                        output = "notmatched";
                        isGeniun = false;
                        break;
                }
                msg = output;

                if (!isGeniun)
                {
                    System.IO.File.Delete(path);
                }
                else
                    retMsg = isGeniun;
            }
            return retMsg;
        }

    }
}
