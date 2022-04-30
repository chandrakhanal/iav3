using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndianArmyWeb.Models
{
    public class PageContent
    {
        [Key]
        public int PageContentId { get; set; }
        
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int MenuId { get; set; }
        public virtual MenuItemMaster MenuItemMasters { get; set; }
    }
}