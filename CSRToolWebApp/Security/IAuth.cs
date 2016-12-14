using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSRTool.Core;

namespace CSRToolWebApp.Security
{
    public interface IAuth
    {
        User User { get; }
        User AdminUser { get; }

        bool IsValidUser();
    }
}