using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum ScanCommentTypes
    {
        [EnumDisplayName(DisplayName = "Negative information found, credible source")]
        NegativeCredibleSource = 0,
        [EnumDisplayName(DisplayName = "Negative information found, non credible source")]
        NegativeNonCredibleSource = 3,
        [EnumDisplayName(DisplayName = "No negative information found")]
        NoNegative = 10
    }
}