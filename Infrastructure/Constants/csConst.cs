using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Constants
{
    public class csConst
    {
        private static string csalt; // field
        public static string cSalt   // property
        {
            get { return csalt; }
            set { csalt = value; }
        }
    }
}