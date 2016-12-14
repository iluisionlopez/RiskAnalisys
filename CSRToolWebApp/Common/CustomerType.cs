using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum CustomerType
    {
        [EnumDisplayName(DisplayName = "New Customer")]
        New,
        [EnumDisplayName(DisplayName = "Existing Customer")]
        Existing
    }
}