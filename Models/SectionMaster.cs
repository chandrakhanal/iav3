﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class SectionMaster
    {
        [Key]
        public int SectionID { get; set; }
        public string SectionName { get; set; }
        public string SectionNameHindi { get; set; }
        public DateTime lastuserupdate { get; set; }
        public int SectionTypeID { get; set; }
        public virtual SectionTypes SectionTypes { get; set; }
    }
}