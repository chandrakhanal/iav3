using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class MediaGalleryRepository : Repository<MediaGallery>, IMediaGalleryRepository
    {
        public MediaGalleryRepository(DbContext context) : base(context)
        {
        }
        public void UpdateRecord(MediaGallery objmediaGallery)
        {
            IASiteContext.MediaGalleries.Attach(objmediaGallery);
            IASiteContext.Entry(objmediaGallery).State = EntityState.Modified;
            if (objmediaGallery.iMediaFiles.Count >= 1)
            {
                var removeOldItem = IASiteContext.MediaFiles.Where(x => x.MediaGalleryId == objmediaGallery.MediaGalleryId).ToList();
                if (removeOldItem != null)
                {
                    IASiteContext.MediaFiles.RemoveRange(removeOldItem);
                    //IASiteContext.SaveChanges();
                }
                foreach (var up in objmediaGallery.iMediaFiles)
                {
                    IASiteContext.MediaFiles.Attach(up);
                    IASiteContext.Entry(up).State = EntityState.Added;
                    //IASiteContext.Entry(up).Property(x => x.IsAttend).IsModified = true;
                }
            }
        }

        public IASiteContext IASiteContext
        {
            get { return Context as IASiteContext; }
        }

    }
}