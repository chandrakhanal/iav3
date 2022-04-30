using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public class UserActivityVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter Act ID")]
        [Display(Name = "Activity Id")]
        public int ActivityId { get; set; }

        [Display(Name = "Url")] 
        public string Url { get; set; }
        [Display(Name = "Description")]
        public string Data { get; set; }
        [Display(Name = "Username")] 
        public string UserName { get; set; }
        [Display(Name = "IP Address")] 
        public string IpAddress { get; set; }
        [Display(Name = "Activity Date")] 
        public DateTime ActivityDate { get; set; }
    }
}