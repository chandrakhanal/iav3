﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public class SiteMenuItem : ISiteLink
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public bool OpenInNewWindow { get; set; }
        public int SortOrder { get; set; }
    }
}