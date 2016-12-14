using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Models;
using GridMvc;
using GridMvc.Filtering;
using GridMvc.Sorting;
using Newtonsoft.Json;

using Territory = CSRToolWebApp.DataContracts.Base.Territory;

namespace CSRToolWebApp.Controllers
{
    public class TerritoryController : Controller
    {
        #region Views

        public ActionResult Default()
        {

            var territoryList = GetTerritoryList();
            ViewBag.Territories = territoryList;

            var territories = FetchTerritoriesFromService();


            var model = CreateGridFromTerritoriesList(territories);

            return View(model);
        }

        private static List<SelectListItem> GetTerritoryList()
        {
            var restService = new RestService("territories");
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var dataContractTerritory = JsonConvert.DeserializeObject<List<Territory>>(json);
            var selectList =
                dataContractTerritory.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            //selectList.Insert(0, new SelectListItem() { Text = string.Empty, Value = Guid.Empty.ToString() });

            return selectList;
        }


        #endregion Views

        #region Methods

        private static Grid<SimpleTerritory> CreateGridFromTerritoriesList(IEnumerable<SimpleTerritory> territories)
        {
            var grid = new Grid<SimpleTerritory>(territories.OrderBy(x => x.Name));
            grid.Columns.Add(x => x.Name);


            return grid;
        }

        private static IEnumerable<SimpleTerritory> FetchTerritoriesFromService()
        {
            var restService = new RestService("territories");
            var json = restService.Get();

            return JsonConvert.DeserializeObject<List<SimpleTerritory>>(json);
        }


        #endregion Methods
    }
}