using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndianArmyWeb.Models
{
    public class MediaFile
    {
        public MediaFile()
        {
            GuId = Guid.NewGuid();
        }
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }

        [Column(Order = 2)]
        public int MediaGalleryId { get; set; }
        public virtual MediaGallery MediaGalleries { get; set; }

        //public int MediaFileId { get; set; }
    }
}