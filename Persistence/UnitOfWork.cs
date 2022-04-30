using IndianArmyWeb.Core;
using IndianArmyWeb.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace IndianArmyWeb.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IASiteContext _context;

        #region Interface Instance
        private IMenuItemMasterRepository _MenuItemMstrRepo;
        private IMenuUrlMasterRepository _MenuUrlMstrRepo;
        private IPageContentRepository _PageContentRepo;
        private IMenuRoleRepository _MenuRoleRepo;
        private IMediaCategoryMasterRepository _MediaCategoryMstrRepo;
        private INewsArticleRepository _NewsArticleRepo;
        private IMediaGalleryRepository _MediaGalleryRepo;
        private IUserActivityRepository _UserActivityRepo;
        private IContactUsRepository _ContactUsRepo;
        private ICSConfRepository _ICSConfRepo;
        #endregion
        public UnitOfWork(IASiteContext context)
        {
            _context = context;
        }
        #region Interface new Object
        
        public IMenuItemMasterRepository MenuItemMstrRepo => _MenuItemMstrRepo = _MenuItemMstrRepo ?? new MenuItemMasterRepository(_context);
        public IMenuUrlMasterRepository MenuUrlMstrRepo => _MenuUrlMstrRepo = _MenuUrlMstrRepo ?? new MenuUrlMasterRepository(_context);
        public IPageContentRepository PageContentRepo => _PageContentRepo = _PageContentRepo ?? new PageContentRepository(_context);
        public IMenuRoleRepository MenuRoleRepo => _MenuRoleRepo = _MenuRoleRepo ?? new MenuRoleRepository(_context);
        public IMediaCategoryMasterRepository MediaCategoryMstrRepo => _MediaCategoryMstrRepo = _MediaCategoryMstrRepo ?? new MediaCategoryMasterRepository(_context);
        public INewsArticleRepository NewsArticleRepo => _NewsArticleRepo = _NewsArticleRepo ?? new NewsArticleRepository(_context);
        public IMediaGalleryRepository MediaGalleryRepo => _MediaGalleryRepo = _MediaGalleryRepo ?? new MediaGalleryRepository(_context);
        public IUserActivityRepository UserActivityRepo => _UserActivityRepo = _UserActivityRepo ?? new UserActivityRepository(_context);
        public IContactUsRepository ContactUsRepo => _ContactUsRepo = _ContactUsRepo ?? new ContactUsRepository(_context);
        public ICSConfRepository CSConfRepo => _ICSConfRepo = _ICSConfRepo ?? new CSConfRepository(_context);

        #endregion

        public int Complete()
        {
            //OnBeforeSaving();
            return _context.SaveChanges();
        }

        public int Complete(Controller controller)
        {
            OnBeforeSaving();
            //string NotifyType = getOperationType();
            int rowAffct = _context.SaveChanges();
            //if (rowAffct > 0) CSPLNotificationExtensions.SetPromptNotification(controller, NotifyType);
            return rowAffct;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            OnBeforeSaving();
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            OnBeforeSaving();
            await _context.SaveChangesAsync();
        }

        #region Helper Methods
        private void OnBeforeSaving()
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is Models.BaseEntity)
                {
                    var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["CreatedBy"] = HttpContext.Current.User.Identity.GetUserId();
                            entry.Property("LastUpdatedAt").IsModified = false;
                            entry.Property("LastUpdatedBy").IsModified = false;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues["LastUpdatedAt"] = now;
                            entry.CurrentValues["LastUpdatedBy"] = HttpContext.Current.User.Identity.GetUserId();
                            entry.Property("CreatedAt").IsModified = false;
                            entry.Property("CreatedBy").IsModified = false;
                            break;
                    }
                }
            }
        }
        #endregion
    }
}