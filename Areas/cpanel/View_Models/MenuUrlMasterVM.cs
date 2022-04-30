using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IndianArmyWeb.Infrastructure.Constants;

namespace IndianArmyWeb.Areas.cpanel.View_Models
{
    public class MenuUrlMasterVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MenuUrl Id")]
        [Display(Name = "MenuUrl Id")]
        public int MenuUrlId { get; set; }
        
        [Required(ErrorMessage = "Please Enter UrlPrefix")]
        [Display(Name = "UrlPrefix")]
        public string UrlPrefix { get; set; }

        [Required(ErrorMessage = "Please Enter Controller")]
        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Required(ErrorMessage = "Please Enter Action")]
        [Display(Name = "Action")]
        public string Action { get; set; }

        [Required(ErrorMessage = "Please Select PageType")]
        [Display(Name = "PageType")]
        [Range(1, int.MaxValue, ErrorMessage = "Select PageType")]
        public PageType PageType { get; set; }
    }
    public class MenuUrlMasterIndxVM : MenuUrlMasterVM
    {
    }
    public class MenuUrlMasterCrtVM : MenuUrlMasterVM
    {
    }
    public class MenuUrlMasterUpVM : MenuUrlMasterVM
    {
    }
}