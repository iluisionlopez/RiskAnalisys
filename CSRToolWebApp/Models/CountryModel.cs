using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSRToolWebApp.Models
{
    public class CountryModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string TerritoryType { get; set; }

        public Guid TerritoryTypeId { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> TerritoryList { get; set; }
    }
}