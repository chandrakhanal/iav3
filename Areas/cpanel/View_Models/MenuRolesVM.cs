using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Areas.cpanel.View_Models
{
    public class MenuRoleVM
    {
        //public MenuRoleVM()
        //{
        //    iMenuRole = new List<MenuRole>();
        //}
        //public virtual ICollection<MenuRole> iMenuRole { get; set; }
    }
    
    public class MenuRoleIndxVM : MenuRoleVM
    {
        public MenuRoleIndxVM()
        {
            iMenuRole = new List<MenuRole>();
        }
        public virtual ICollection<MenuRole> iMenuRole { get; set; }
    }
    public class MenuRoleCrtVM : MenuRoleVM
    {
        [Required(ErrorMessage = "Select Role")]
        [Display(Name = "Role")]
        //[Range(0, int.MaxValue, ErrorMessage = "Select MenuType")]
        public int RoleId { get; set; }
        public MenuRoleCrtVM()
        {
            //iMenuRoles = new List<MenuRole>();
            iMenuRoleMenuitem = new List<MenuRoleMenuitem>();
        }
        //public virtual ICollection<MenuRole> iMenuRoles { get; set; }
        public virtual ICollection<MenuRoleMenuitem> iMenuRoleMenuitem { get; set; }
    }
    public class MenuRoleMenuitem
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public string SlugMenu { get; set; }
        public int SortOrder { get; set; }
        public string MenuName { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
    }
    public class MenuRoleUpVM : MenuRoleVM
    {
        public MenuRoleUpVM()
        {
            iMenuRole = new List<MenuRole>();
        }
        public virtual ICollection<MenuRole> iMenuRole { get; set; }
    }
}