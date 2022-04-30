using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Constants
{
    public class CustomEnum
    {
    }
    public enum PositionType
    {
        Select = 0,
        Top = 1,
        Left = 2,
        Bottom=3
    }
    
    public enum Layout
    {
        Select = 0,
        Lay1 = 1,
        Lay2 = 2
    }
    public enum PageType
    {
        Select = 0,
        Static = 1,
        Dynamic = 2
    }
    public enum NewsCategory
    {
        [Display(Name = "Bulletin")]
        Bulletin = 1,

        [Display(Name = "What's New")]
        WhatsNew = 2,

        [Display(Name = "News")]
        News = 3,

        [Display(Name = "Flash News")]
        Flash = 4
    }
    public enum MediaType
    {
        Image = 1,
        Video = 2,
        Document = 3
    }
    
}