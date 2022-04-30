using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Constants
{
    public static class CustomDropDowns
    {
        public static IEnumerable<SelectListItem> GetDepartments()
        {
            var departments = new List<SelectListItem>
            {
                new SelectListItem{ Text="DG Sectt", Value = "DG Sectt" },
                new SelectListItem{ Text="Adm & Coord", Value = "Adm & Coord" },
                new SelectListItem{ Text="Directorate of Self Employment", Value = "Directorate of Self Employment" },
                new SelectListItem{ Text="ESM Registration", Value = "ESM Registration" },
                new SelectListItem{ Text="ESM JCOs / OR Employment Assistance", Value = "ESM JCOs / OR Employment Assistance" },
                new SelectListItem{ Text="Directorate of Training", Value = "Directorate of Training" },
                new SelectListItem{ Text="Directorate of Legal", Value = "Directorate of Legal" },
                new SelectListItem{ Text="Directorate of IT", Value = "Directorate of IT" },
                new SelectListItem{ Text="Directorate of Publicity", Value = "Directorate of Publicity" },
                new SelectListItem{ Text="Reservation Monitoring Cell", Value = "Reservation Monitoring Cell" },
                new SelectListItem{ Text="Directorate of S & R", Value = "Directorate of S & R" },
                new SelectListItem{ Text="CPGRAM & RTI Nodal Officer", Value = "CPGRAM & RTI Nodal Officer" },
                new SelectListItem{ Text="DG Sectt and Adm & Coord", Value = "DG Sectt and Adm & Coord" },
                new SelectListItem{ Text="Directorate of S & R, CPGRAM & RTI Nodal Officer", Value = "Directorate of S & R, CPGRAM & RTI Nodal Officer" },
            };
            return departments;
        }
        public static IEnumerable<SelectListItem> GetLocations()
        {
            var locations = new List<SelectListItem>
            {
                new SelectListItem{ Text="HQ DGR", Value = "HQ DGR" },
                new SelectListItem{ Text="DRZ(N)", Value = "DRZ(N)" },
                new SelectListItem{ Text="DRZ(S)", Value = "DRZ(S)" },
                new SelectListItem{ Text="DRZ(E)", Value = "DRZ(E)" },
                new SelectListItem{ Text="DRZ(W)", Value = "DRZ(W)" }, 
                new SelectListItem{ Text="DRZ(C)", Value = "DRZ(C)" },
            };
            return locations;
        }

    }
}