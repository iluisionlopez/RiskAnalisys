using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum OfferType
    {
        [EnumDisplayName(DisplayName = "Product")]
        Product,
        [EnumDisplayName(DisplayName = "Service inluding Scania people")]
        Service,
        [EnumDisplayName(DisplayName = "Financing")]
        Financing
    }
}