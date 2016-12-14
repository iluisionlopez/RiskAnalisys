using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    public class ApplicationHeaderOption
    {
        public bool ShowLogo { get; set; }
        public bool ShowApplicationTitle { get; set; }
        public bool ShowFAQLink { get; set; }
        public bool ShowHelpLink { get; set; }
        public bool ShowAboutLink { get; set; }
        public bool ShowLanguageDropDown { get; set; }
        public bool ShowLogOutLink { get; set; }
        public bool ShowLoggedInUserInfo { get; set; }
    }
}