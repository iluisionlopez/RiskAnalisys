using CSRToolWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CSRToolWebApp.Common
{
    public static class Helper
    {
        public static class Request
        {
            public static RouteData GetRoute()
            {
                return RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            }

            public static string GetControllerName()
            {
                return GetRoute().Values["controller"].ToString();
            }

            public static string GetActionName()
            {
                return GetRoute().Values["action"].ToString();
            }

            public static CultureInfo GetDefaultLanguage()
            {
                CultureInfo ci = new CultureInfo("en-GB");

                //  Try to get values from Accept lang HTTP header
                if (HttpContext.Current.Request.UserLanguages != null)
                {
                    foreach (string lang in HttpContext.Current.Request.UserLanguages)
                    {
                        // the string can look like "sv-SE;q=0.5", so split on semicolon to get the first part
                        string langCode = lang.Split(';').First();

                        // check against our list of supported languages
                        if (Helper.Application.SupportedLangCodes.Exists(l => l.Equals(langCode, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            // if we have it, set the culture and exit  
                            ci = new CultureInfo(langCode);
                            break;
                        }
                    }
                }

                return ci;
            }
        }

        public static class Application
        {
            public static List<string> SupportedLangCodes
            {
                get
                {
                    return new List<string>() { "sv-SE"
                                               ,"en-GB"
                                               ,"fa-IR"
                                               ,"fi-FI" };
                }
            }

            public static bool LoginEnabled
            {
                get
                {
                    try
                    {
                        return bool.Parse(ConfigurationManager.AppSettings["LoginEnabled"]);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            public static string LoginDomain
            {
                get
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["LoginDomain"];
                    }
                    catch (Exception)
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public static class GlobalUserMessages
        {
            public static void Append(UserMessage newMessage)
            {
                List<UserMessage> messages = GetAll();
                messages.Add(newMessage);
            }

            public static List<UserMessage> GetAll()
            {
                //  HttpContext.Current.Items, unlike the Session object, exist only within one request context
                //  This way, one can use "global" variables/objects to share between views/controllers but only within one request
                //  without weighing down the server with session objects.

                if (HttpContext.Current.Items["GlobalUserMessages"] == null)
                {
                    HttpContext.Current.Items["GlobalUserMessages"] = new List<UserMessage>();
                }

                return (List<UserMessage>)HttpContext.Current.Items["GlobalUserMessages"];
            }
        }
    }
}