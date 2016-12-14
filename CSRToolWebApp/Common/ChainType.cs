using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum ChainType
    {
        [EnumDisplayName(DisplayName = "None")]
        None = 0,
        [EnumDisplayName(DisplayName = "No visits, pictures from site and written info received")]
        Novisits = 3,
        [EnumDisplayName(DisplayName = "Visited site, lacking info about labour laws & working conditions")]
        Visitedlacking = 6,
        [EnumDisplayName(DisplayName = "Visited site, labour laws are followed & working conditions acceptable")]
        VisitedAccepted = 10
    }
}