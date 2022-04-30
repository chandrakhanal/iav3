using IndianArmyWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Models
{
    public class ContactUs : BaseEntity
    {
        [Key]
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string SubjectCategory { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }
    }
}