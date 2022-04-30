using IndianArmyWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public class ContactUsVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter ContactId")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Contact No")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Select Subject")]
        [Display(Name = "Subject")]
        public string SubjectCategory { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
    public class ContactUsIndxVM : ContactUsVM
    {
        [Display(Name = "Submit Dt")]
        public DateTime CreatedAt { get; set; }
    }
}