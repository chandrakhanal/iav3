using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class TenderDetail
    {
        [Key]
        public int TenderFileID { get; set; }
        public string FileHeader { get; set; }
        public string AttFileName { get; set; }
        public DateTime lastuserupdate { get; set; }
        public int TenderID { get; set; }
        public virtual TenderMaster TenderMasters { get; set; }
    }
}