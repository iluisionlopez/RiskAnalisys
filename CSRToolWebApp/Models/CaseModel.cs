using CSRToolWebApp.Common;
using CSRToolWebApp.Common.ExtensionMethods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseType = CSRToolWebApp.DataContracts.Base.CaseType;

namespace CSRToolWebApp.Models
{
    public class CaseModel
    {
        #region Properties
        public string CaseID { get; set; }
        public string CaseName { get; set; }
        public bool Selected { get; set; }
        public IRestService Service { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CaseModel()
        {
            Service = new RestService("casetypes");
        }
        /// <summary>
        /// Create an Instance of OfferModel from a business Entity OfferType
        /// </summary>
        /// <param name="offer">Business Entity OfferType</param>
        public CaseModel(CaseType type)
        {
            CaseID = type.Id.ToString();
            CaseName = type.Name;
            Selected = false;
        }
        #endregion

        #region Internal Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<SelectListItem> GetCaseTypesList()
        {
            var types = new List<SelectListItem>();
            foreach (var type in GetCaseTypes(new RestService("casetypes"), "Customer"))
            {
                var item = new SelectListItem()
                {
                    Text = type.CaseName,
                    Value = type.CaseID
                };
                types.Add(item);
            }
            return types;
        }

        internal static IEnumerable<SelectListItem> GetSupplierCaseTypesList()
        {
            var types = new List<SelectListItem>();
            foreach (SupplierType type in Enum.GetValues(typeof(SupplierType)))
            {
                var item = new SelectListItem()
                {
                    Text = type.DisplayName(),
                    Value = type.ToString()
                };
                types.Add(item);
            }
            return types;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IEnumerable<CaseModel> GetCaseTypes(IRestService service, string casetype)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<CaseType>>(json);
            var result = new List<CaseModel>();
            foreach (CaseType type in dataContract.Where(x=>x.Name.Contains(casetype)))
            {
                var model = new CaseModel(type);
                result.Add(model);
            }
            return result;
        }
        #endregion
    }
}