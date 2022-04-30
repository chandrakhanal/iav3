using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class UserActivity
    {
        [Key]
        public int ActivityId { get; set; }
        public string Url { get; set; }
        public string Data { get; set; }
        public  string UserName{ get; set; }
        public string IpAddress { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}