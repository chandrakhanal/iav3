using System;
using System.Collections.Generic;
using IndianArmyWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IndianArmyWeb.Infrastructure.Constants;

namespace IndianArmyWeb.Areas.cpanel.View_Models
{
    public class GrievanceVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter GrievanceId")]
        [Display(Name = "Grievance Id")]
        public int GrievanceId { get; set; }

        [Required(ErrorMessage = "Please Enter RegnNo")]
        [Display(Name = "DGR Regn No")]
        public string RegnNo { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Contact No")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Select Scheme")]
        [Display(Name = "Name of Scheme")]
        public int SchemeId { get; set; }

        [Display(Name = "Remarks by Dept")]
        public string DeptRemarks { get; set; }

        [Display(Name = "Remarks by (Person)")]
        public string DeptRemarksGivenBy { get; set; }

        [Display(Name = "Status")]
        public string GrievanceStatus { get; set; }

        [Display(Name = "Remarks Given Date")]
        public DateTime DeptRemarksGivenDate { get; set; }

        [Display(Name = "Archive")]
        public bool Archive { get; set; }

        [Display(Name = "Archive Date")]
        public DateTime ArchiveDate { get; set; }
        //public virtual DGRScheme DGRSchemes { get; set; }
        //public virtual GrievanceFile GrievanceFiles { get; set; }
    }
}