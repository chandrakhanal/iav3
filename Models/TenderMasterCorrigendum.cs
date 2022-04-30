using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class TenderMasterCorrigendum
    {
        [Key]
        public int TenderID { get; set; }
        public string TenderName { get; set; }
        public Boolean Publish { get; set; }
        public Boolean Archive { get; set; }
        public DateTime duedate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime lastuserupdate { get; set; }
        public int tblId { get; set; }
        public int SectionID { get; set; }
        public virtual SectionMaster SectionMasters { get; set; }


    }
}