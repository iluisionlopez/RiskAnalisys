using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TerritoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TerritoryType { get; set; }
        public bool IsActive { get; set; }
        public Guid ChangedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal static IEnumerable<SelectListItem> GetTerritories(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContractTerritory = JsonConvert.DeserializeObject<List<Territory>>(json).OrderBy(x=>x.Name);
            var territories = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Selected = true,
                    Text = Resources.SalesController_GetTerritories_Select,
                    Value = "-1"
                }
            };

            territories.AddRange(dataContractTerritory.Select(territory => new SelectListItem()
            {
                Selected = false,
                Text = territory.Name,
                Value = territory.Id.ToString()
            }));

            return territories;
        }
    }
}