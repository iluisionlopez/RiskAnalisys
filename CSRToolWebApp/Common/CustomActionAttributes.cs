using CSRToolWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CSRToolWebApp.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SetLanguageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //  It's important to check whether session object is ready
            if (SessionHandler.CurrentSession != null)
            {
                string action = Helper.Request.GetActionName();
                string controller = Helper.Request.GetControllerName();

                bool routeToLogIn = controller == "Home" && action == "LogIn";
                bool routeToChangeLanguage = controller == "Home" && action == "ChangeLanguage";

                if (!routeToLogIn && !routeToChangeLanguage)
                {
                    if (SessionHandler.SelectedLanguage == null)
                    {
                        SessionHandler.SelectedLanguage = Helper.Request.GetDefaultLanguage();
                    }

                    Thread.CurrentThread.CurrentUICulture = SessionHandler.SelectedLanguage;
                    Thread.CurrentThread.CurrentCulture = SessionHandler.SelectedLanguage;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionHandler.LoggedInUser == null)
            {
                string action = Helper.Request.GetActionName();
                string controller = Helper.Request.GetControllerName();

                bool routeToLogIn = controller == "Home" && action == "LogIn";
                bool routeToLogOut = controller == "Home" && action == "LogOut";
                bool routeToChangeLanguage = controller == "Home" && action == "ChangeLanguage";

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //  Return error message
                    var partialViewResult = new PartialViewResult();
                    partialViewResult.ViewName = "_UserMessage";
                    partialViewResult.ViewData.Model = new UserMessage(UserMessageType.Error, "*** You have to log in first. Change this message in Common.CustomActionAttributes.cs file ***");
                    filterContext.Result = partialViewResult;
                }
                else if (!routeToLogIn && !routeToChangeLanguage && !routeToLogOut)
                {
                    string returnUrl = HttpContext.Current.Request.RawUrl;

                    //  "Redirect" to /LogOn
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                                                                         {
                                                                             { "Controller", "Home" },                         
                                                                             { "Action", "LogIn" },
                                                                             { "returnUrl", returnUrl }
                                                                         }
                                                                     );
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}