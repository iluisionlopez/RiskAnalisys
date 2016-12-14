using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CSRToolWebApp.Models
{
    public class AssemblyInfo
    {
        public AssemblyInfo() : this(Assembly.GetExecutingAssembly()) { }

        public AssemblyInfo(Assembly assembly)
        {
            object attr = null;

            this.Name = assembly.GetName().Name;

            attr = assembly.GetCustomAttribute(typeof(AssemblyTitleAttribute));
            if (attr != null)
            {
                this.Title = ((AssemblyTitleAttribute)attr).Title;
            }

            attr = assembly.GetCustomAttribute(typeof(AssemblyDescriptionAttribute));
            if (attr != null)
            {
                this.Description = ((AssemblyDescriptionAttribute)attr).Description;
            }

            attr = assembly.GetCustomAttribute(typeof(AssemblyCompanyAttribute));
            if (attr != null)
            {
                this.Company = ((AssemblyCompanyAttribute)attr).Company;
            }

            attr = assembly.GetCustomAttribute(typeof(AssemblyProductAttribute));
            if (attr != null)
            {
                this.Product = ((AssemblyProductAttribute)attr).Product;
            }

            attr = assembly.GetCustomAttribute(typeof(AssemblyCopyrightAttribute));
            if (attr != null)
            {
                this.Copyright = ((AssemblyCopyrightAttribute)attr).Copyright;
            }

            attr = assembly.GetCustomAttribute(typeof(AssemblyTrademarkAttribute));
            if (attr != null)
            {
                this.Trademark = ((AssemblyTrademarkAttribute)attr).Trademark;
            }

            this.Version = assembly.GetName().Version.ToString();
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Product { get; set; }
        public string Copyright { get; set; }
        public string Trademark { get; set; }
        public string Version { get; set; }
    }
}