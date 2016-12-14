using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumDisplayNameAttribute : Attribute
    {
        private string _displayName;
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
    }
}