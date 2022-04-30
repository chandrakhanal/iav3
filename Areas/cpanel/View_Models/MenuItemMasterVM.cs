using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Infrastructure.Helpers.Routing;

namespace IndianArmyWeb.Areas.cpanel.View_Models
{
    public class MenuItemMasterVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Menu Id")]
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Select Parent Menu")]
        [Display(Name = "Parent Menu")]
        [Range(0, int.MaxValue, ErrorMessage = "Select MenuType")]
        public int ParentId { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "Please Enter Menu Name")]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "Please Enter मेनू का नाम")]
        [Display(Name = "मेनू का नाम")]
        public string HMenuName { get; set; }

        [Required(ErrorMessage = "Please Enter Page Title")]
        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }

        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Page Heading")]
        public string PageHeading { get; set; }

        [Required(ErrorMessage = "Please Enter पृष्ठ शीर्षक")]
        [Display(Name = "पृष्ठ शीर्षक")]
        public string HPageHeading { get; set; }

        [Required(ErrorMessage = "Visible")]
        [Display(Name = "Visible")]
        public bool IsVisible { get; set; }

        [Required(ErrorMessage = "Please Select Position")]
        [Display(Name = "Position")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Position")]
        public PositionType PositionType { get; set; }

        [Required(ErrorMessage = "Please Select Layout")]
        [Display(Name = "Layout")]
        [Range(0, int.MaxValue, ErrorMessage = "Select Layout")]
        public Layout Layout { get; set; }

        [Required(ErrorMessage = "Please Select External Link")]
        [Display(Name = "External Link")]
        public bool ExternalLink { get; set; }

        [Required(ErrorMessage = "Please Select External Url")]
        [Display(Name = "External Url")]
        public string ExternalUrl { get; set; }

        //[Required(ErrorMessage = "Select Url Prefix")]
        [Display(Name = "Url Prefix")]
        public int? MenuUrlId { get; set; }
        public virtual MenuUrlMaster MenuUrlMasters { get; set; }
    }
    public class MenuItemMasterIndxVM : MenuItemMasterVM
    {
        [Display(Name = "SlugMenu")]
        public string SlugMenu { get; set; }
    }
    public class MenuItemMasterCrtVM : MenuItemMasterVM
    {
        [Display(Name = "SlugMenu")]
        public string SlugMenu { get { return MenuName.ToSlug(); } set { SlugMenu = MenuName.ToSlug(); } }
    }
    
    public class MenuItemMasterUpVM : MenuItemMasterVM
    {
        private string _slugMenu="";
        [Display(Name = "SlugMenu")]
        public string SlugMenu
        {
            get { if (MenuName != null) { return MenuName.ToSlug(); } return ""; }
            set { this._slugMenu = value; }
        }
    }
    public class MenuSortOrderVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Menu Id")]
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Select Parent Menu")]
        [Display(Name = "Parent Menu")]
        [Range(0, int.MaxValue, ErrorMessage = "Select MenuType")]
        public int ParentId { get; set; }

        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }

        [Display(Name = "Slug")]
        public string SlugMenu { get; set; }
        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }
    }

}