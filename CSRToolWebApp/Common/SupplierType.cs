using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum SupplierType
    {
        [EnumDisplayName(DisplayName = "New Supplier")]
        New,
        [EnumDisplayName(DisplayName = "Existing Supplier")]
        Existing
    }
}