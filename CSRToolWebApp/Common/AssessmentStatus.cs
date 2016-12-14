using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum AssessmentStatus
    {
        [EnumDisplayName(DisplayName = "Incomplete")]
        Incomplete,
        [EnumDisplayName(DisplayName = "Complete")]
        Complete
    }
}