using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CSRToolWebApp.Common.ExtensionMethods;
using CSRToolWebApp.Models;

namespace CSRToolWebApp
{
    public class IndexModel
    {
        #region Properties
        public Guid IndexId { get; set; }
        public Guid IndexTypeID { get; set; }
        public string IndexTypeName { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public IEnumerable<RiskCategoryModel> Riskcategories { get; set; }
        #endregion

        #region Constructors
        public IndexModel()
        {
            Riskcategories = new List<RiskCategoryModel>();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<IndexModel> GetIndexTypes()
        {
            var response = new List<IndexModel>();
            var restService = new RestService("indextypes");
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var dataContractIndex = JsonConvert.DeserializeObject<List<IndexType>>(json);
            var serviceOpereartion = "riskcategoriesbyindex/?indextype={0}";
            foreach (var type in dataContractIndex)
            {
                var result = new IndexModel();
                result.IndexId = type.Id;
                result.IndexTypeName = type.Name;
                result.Value = string.Empty;
                result.Riskcategories = RiskCategoryModel.GetRiskCategories(new RestService(string.Format(serviceOpereartion,result.IndexId.ToString())));
                result.Order = type.SortOrder;
                response.Add(result);
            }
            return response.OrderBy(x => x.Order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="territoryId"></param>
        /// <param name="versionId"></param>
        /// <returns></returns>
        internal static IEnumerable<IndexModel> GetIndexesByCuontry(string territoryId, string versionId)
        {
            var response = new List<IndexModel>();
            var str = string.Format("indexesbycountry/?territory={0}&version={1}", territoryId, versionId);
            var service = new RestService(str);
            var result = service.Get(new List<KeyValuePair<string, string>>());
            var indexvalues = JsonConvert.DeserializeObject<List<Index>>(result);

            str = "indextypes";
            service = new RestService(str);
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var indextypes = JsonConvert.DeserializeObject<List<IndexType>>(json);
            var serviceOpereartion = "riskcategoriesbyindex/?indextype={0}";

            foreach (var type in indexvalues)
            {
                var index = new IndexModel();
                index.IndexId = type.Id;
                index.IndexTypeName = type.IndexTypeName;
                index.IndexTypeID = Guid.Parse(type.IndexTypeId);
                index.Value = Math.Ceiling(type.Value).ToString();
                index.Order = indextypes.FirstOrDefault(x => x.Id == Guid.Parse(type.IndexTypeId)).SortOrder;
                index.Riskcategories = RiskCategoryModel.GetRiskCategories(new RestService(string.Format(serviceOpereartion, index.IndexTypeID.ToString())));
                response.Add(index);
            }

            return response.OrderBy(x => x.Order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="riskCategoryId"></param>
        /// <returns></returns>
        internal static IEnumerable<IndexModel> IndexesByRiskCategory(string riskCategoryId)
        {
            var response = new List<IndexModel>();
            var str = string.Format("indexesbyriskcategory/?riskcategory={0}", riskCategoryId);
            var restService = new RestService("str");
            var json = restService.Get(new List<KeyValuePair<string, string>>());
            var dataContractIndex = JsonConvert.DeserializeObject<List<IndexType>>(json);

            var serviceOpereartion = "riskcategoriesbyindex/?indextype={0}";
            foreach (var type in dataContractIndex)
            {
                var result = new IndexModel();
                result.IndexId = type.Id;
                result.IndexTypeName = type.Name;
                result.Value = "";
                result.Order = type.SortOrder;
                result.Riskcategories = RiskCategoryModel.GetRiskCategories(new RestService(string.Format(serviceOpereartion, result.IndexId.ToString())));
                response.Add(result);
            }
            return response.OrderBy(x => x.Order);
        }
    }
}