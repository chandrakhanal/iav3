using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IndianArmyWeb.Models
{
    public class BaseEntity
    {
        [ScaffoldColumn(false)]
        public DateTime? CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LastUpdatedAt { get; set; }

        [ScaffoldColumn(false)]
        public string LastUpdatedBy { get; set; }
    }
}