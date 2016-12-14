using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SectorModel
    {
        public Guid SectorID { get; set; }
        public string SectorName { get; set; }
        public Guid SelectedSectorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal static IEnumerable<SelectListItem> GetSectors(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContractSector = JsonConvert.DeserializeObject<List<Sector>>(json);
            var sectors = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Selected = true,
                    Text = Resources.SalesController_GetSectors_Select,
                    Value = string.Empty
                }
            };

            sectors.AddRange(dataContractSector.Select(sector => new SelectListItem()
            {
                Selected = false,
                Text = sector.Name,
                Value = sector.Id.ToString()
            }));

            return sectors;
        }
    }
}