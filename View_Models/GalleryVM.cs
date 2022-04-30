using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public class GalleryVM
    {
    }
    
    public class HomeGalleryVM
    {
        public HomeGalleryVM()
        {
            iMediaFiles = new List<HomeMediaFiles>();
        }
        public int MediaGalleryId { get; set; }

        public MediaType MediaType { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public bool Archive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArchiveDate { get; set; }

        public int MediaCategoryId { get; set; }
        public MediaCategoryMaster MediaCategoryMasters { get; set; }

        public ICollection<HomeMediaFiles> iMediaFiles { get; set; }
    }
    public class HomeBannerSliderVM
    {
        public HomeBannerSliderVM()
        {
            iMediaFiles = new List<HomeMediaFiles>();
        }
        public int MediaGalleryId { get; set; }

        public MediaType MediaType { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public bool Archive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArchiveDate { get; set; }

        public int MediaCategoryId { get; set; }
        public MediaCategoryMaster MediaCategoryMasters { get; set; }

        public ICollection<HomeMediaFiles> iMediaFiles { get; set; }
    }

    public class HomeMediaFiles
    {
        public HomeMediaFiles()
        {
            GuId = Guid.NewGuid();
        }
        [Key]
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }

        public int MediaGalleryId { get; set; }
    }
    public class MemberMediaGalleryVM
    {
        public int MediaGalleryId { get; set; }
        //public MediaFile MediaFiles { get; set; }
        public string FilePath { get; set; }
        public int MediaCategoryId { get; set; }
        public string Caption { get; set; }
    }
}