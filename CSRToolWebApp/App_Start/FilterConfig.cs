using CSRToolWebApp.Common;
using System.Web;
using System.Web.Mvc;

namespace CSRToolWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //  Will intercept all actions and set language for entire thread (Request --> Response)
            filters.Add(new SetLanguageAttribute());

            if (Helper.Application.LoginEnabled)
            {
                //  Will intercept all actions and check for logged in user, but only if login enabled.
                filters.Add(new CheckLoginAttribute());
            }
        }
    }
}
