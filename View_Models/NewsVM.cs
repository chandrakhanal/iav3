using IndianArmyWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public class NewsVM
    {
    }
    public class HomeNewsVM
    {
        [Key]

        public int NewsArticleId { get; set; }

        public NewsCategory NewsCategory { get; set; }

        public string Headline { get; set; }

        public string Description { get; set; }

        public bool Highlight { get; set; }

        public bool Archive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArchiveDate { get; set; }
    }
    public class NewsBulletinVM
    {
        public int NewsArticleId { get; set; }

        public NewsCategory NewsCategory { get; set; }

        [Display(Name = "Headline")]
        public string Headline { get; set; }
        public string Description { get; set; }
        public bool Highlight { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
    }
    public class WhatsNewVM : NewsBulletinVM
    {
        [Display(Name = "Highlights")]
        public string Highlights { get; set; }
    }
}