using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.View_Models
{
    public class PagingVM
    {
        //public List<gallary> Datas { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}