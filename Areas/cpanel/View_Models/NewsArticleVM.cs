using IndianArmyWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Areas.cpanel.View_Models
{
    public class NewsArticleVM
    {
        [Key]

        public int NewsArticleId { get; set; }

        [Required(ErrorMessage = "Select News Category")]
        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Select News Category")]
        public NewsCategory NewsCategory { get; set; }

        [Required(ErrorMessage = "Please Enter Headline")]
        [Display(Name = "Headline")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Highlight")]
        public bool Highlight { get; set; }

        [Display(Name = "Archive")]
        public bool Archive { get; set; }

        [Required(ErrorMessage = "Please Enter PublishDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Dt")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Please Enter ArchiveDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Dt")]
        public DateTime ArchiveDate { get; set; }
    }
    public class NewsArticleIndxVM : NewsArticleVM
    {
    }
    public class NewsArticleCrtVM : NewsArticleVM
    {
    }
    public class NewsArticleUpVM : NewsArticleVM
    {
    }

    //---------------Client---------------
    public class NewsAllDto
    {
        [Key]
        public int NewsArticleId { get; set; }

        public NewsCategory NewsCategory { get; set; }

        public string Headline { get; set; }
        public  string Description { get; set; }

        public bool Highlight { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
    }
    public class NewsAllList
    {
        public NewsCategory NewsCategory { get; set; }
        public NewsAllDto NewsAllDto { get; set; }
    }
}