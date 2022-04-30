using AutoMapper;
using IndianArmyWeb.Areas.cpanel.Models;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Infrastructure.Filters;
using IndianArmyWeb.Infrastructure.Helpers.Account;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using IndianArmyWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    [CSPLHeaders]
    [SessionTimeout]
    [Authorize]
    public class ContactUsController : Controller
    {
        // GET: Admin/ContactUs
        [Authorize(Roles = CustomRoles.Admin)]
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var contactUs = await uow.ContactUsRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ContactUs, ContactUsIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<ContactUs>, IEnumerable<ContactUsIndxVM>>(contactUs).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }
        [Authorize(Roles = CustomRoles.Admin)]
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var DeleteItem = await uow.ContactUsRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                   
                    uow.ContactUsRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    UserActivityHelper.SaveUserActivity("Feedback/Grievances deleted", Request.Url.ToString());
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
  

}