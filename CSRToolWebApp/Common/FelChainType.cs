using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum TransactionType
    {
        [EnumDisplayName(DisplayName = "Direct")]
        Direct,
        [EnumDisplayName(DisplayName = "Public or Goverrnmnet")]
        Public,
        [EnumDisplayName(DisplayName = "Agent")]
        Agent,
        [EnumDisplayName(DisplayName = "Defense")]
        Defense
    }
}