using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSRToolWebApp.Common.ExtensionMethods;

namespace CSRToolWebApp.Models
{
    public class RiskCategoryModel
    {
        #region Properties
        public Guid RiskCategoryID { get; set; }
        public string RiskCategoryName { get; set; }
        public double RiskCategoryValue { get; set; }
        public string ProgressAttribute { get; set; }
        public double TotalRisk { get; set; }
        public IRestService Service { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public RiskCategoryModel()
        {
            Service = new RestService("riskcategories");
        }

        /// <summary>
        /// Constructor that convert a RiskCategory Business Model to a GUI Model
        /// </summary>
        /// <param name="risk">Business Model</param>
        public RiskCategoryModel(RiskCategory risk)
        {
            RiskCategoryID = risk.Id;
            RiskCategoryName = risk.Name;
            RiskCategoryValue = 0;
            ProgressAttribute = "";
            TotalRisk = 0;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal static IEnumerable<RiskCategoryModel> GetRiskCategories(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContractRiskCotegories = JsonConvert.DeserializeObject<List<RiskCategory>>(json);
            var result = new List<RiskCategoryModel>();
            foreach (RiskCategory risk in dataContractRiskCotegories)
            {
                var riskcategory = new RiskCategoryModel(risk);
                result.Add(riskcategory);
            }

            return result;
        }
    }
}