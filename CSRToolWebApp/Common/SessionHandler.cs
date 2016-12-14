using CSRToolWebApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CSRToolWebApp.Common
{
    public static class SessionHandler
    {
        public static HttpSessionState CurrentSession
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static void ClearSession()
        {
            CurrentSession.RemoveAll();
        }

        public static CultureInfo SelectedLanguage
        {
            get
            {
                try
                {
                    return (CultureInfo)CurrentSession["ChosenLanguage"];
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                CurrentSession["ChosenLanguage"] = value;
            }
        }

        public static UserModel LoggedInUser
        {
            get
            {
                try
                {
                    return (UserModel)CurrentSession["LoggedInUser"];
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                CurrentSession["LoggedInUser"] = value;
            }
        }
    }
}