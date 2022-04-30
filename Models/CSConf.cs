using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class CSConf: BaseEntity
    {
        [Key]
        public int ConfId { get; set; }
        public string ConfIP { get; set; }
    }
}