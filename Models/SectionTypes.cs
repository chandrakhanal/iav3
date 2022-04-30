using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class SectionTypes
    {
        [Key]
        public int SectionTypeID { get; set; }
        public string SectionType { get; set; }
        public string SectionTypeHindi { get; set; }
        public DateTime lastuserupdate { get; set; }

        public virtual ICollection<SectionMaster> iSectionMasters { get; set; }
    }
}