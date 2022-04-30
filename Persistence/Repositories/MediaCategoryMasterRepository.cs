using IndianArmyWeb.Core.IRepositories;
using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class MediaCategoryMasterRepository : Repository<MediaCategoryMaster>, IMediaCategoryMasterRepository
    {
        public MediaCategoryMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetMediaCategories()
        {
            List<SelectListItem> mediaCategories = IASiteContext.MediaCategoryMstr
                .OrderBy(n => n.MediaCategoryName)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.MediaCategoryId.ToString(),
                    Text = n.MediaCategoryName
                }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = null,
                Text = "-- Select --"
            };
            mediaCategories.Insert(0, ddltip);
            return new SelectList(mediaCategories, "Value", "Text");
        }
        public IASiteContext IASiteContext
        {
            get { return Context as IASiteContext; }
        }
    }
}