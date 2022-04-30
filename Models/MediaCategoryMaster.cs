using IndianArmyWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class MediaCategoryMaster
    {
        [Key]
        public int MediaCategoryId { get; set; }
        //public MediaType MediaType { get; set; }
        public string MediaCategoryName { get; set; }
    }
}