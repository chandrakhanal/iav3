using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndianArmyWeb.Models
{
    [Table("MenuRoles")]

    public class MenuRole
    {
        [Key, Column(Order = 0)]
        public int RoleId { get; set; }

        [Key, Column(Order = 1)]
        public int MenuId { get; set; }
        public virtual MenuItemMaster MenuItemMasters { get; set; }

        public bool Read { get; set; }
        public bool Write { get; set; }
    }

}