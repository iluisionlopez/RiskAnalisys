using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CSRToolWebApp.Common.ExtensionMethods
{
    public static class CommonExtensions
    {
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomNumber (this int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderPartialViewAsHtml(this Controller controller, string viewName, object model)
        {
            var viewData = controller.ViewData;
            var tmpData = controller.TempData;
            var context = controller.ControllerContext;
            viewData.Model = model;

            using (System.IO.StringWriter writer = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                if (viewData.ModelState.IsValid)
                {
                    viewData.ModelState.Clear();
                }
                var viewContext = new ViewContext(context, viewResult.View, viewData, tmpData, writer);
                viewResult.View.Render(viewContext, writer);

                return writer.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Retrives the Display name attribute of a Enum
        /// </summary>
        /// <param name="value">Enum Value</param>
        /// <returns>Display name attribute</returns>
        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            EnumDisplayNameAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute))
                        as EnumDisplayNameAttribute;

            return attribute == null ? value.ToString() : attribute.DisplayName;
        }

        /// <summary>Indicates whether the specified array is null or has a length of zero.</summary>
        /// <param name="array">The array to test.</param>
        /// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }
    }
}