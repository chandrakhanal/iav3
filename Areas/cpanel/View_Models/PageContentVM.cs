using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IndianArmyWeb.Areas.cpanel.View_Models
{
    public class PageContentVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Page Content Id")]
        public int PageContentId { get; set; }

        [Required(ErrorMessage = "Please Enter Content")]
        [Display(Name ="Contents")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Select Menu")]
        [Display(Name = "Menu")]
        public int MenuId { get; set; }
        public virtual MenuItemMaster MenuItemMasters { get; set; }
    }

    public class PageContentIndxVM : PageContentVM
    {
    }
    public class PageContentCrtVM : PageContentVM
    {
    }
    public class PageContentUpVM : PageContentVM
    {
    }
}