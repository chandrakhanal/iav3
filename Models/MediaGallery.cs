using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class MediaGallery : BaseEntity
    {
        public MediaGallery()
        {
            iMediaFiles = new List<MediaFile>();
        }
        [Key]
        public int MediaGalleryId { get; set; }
        public MediaType MediaType { get; set; }
        public string Caption { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
        public bool Archive { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ArchiveDate { get; set; }

        public int MediaCategoryId { get; set; }
        public virtual MediaCategoryMaster MediaCategoryMasters { get; set; }

        [Display(Name = "Supported Files .png | .jpg | .pdf | .doc | .docx")]
        [AllowExtensions(Extensions = "png,jpg,doc,docx,pdf", ErrorMessage = "Please select only Supported Files .png | .jpg | .pdf | .doc | .docx")]
        public virtual ICollection<MediaFile> iMediaFiles { get; set; }
    }
}