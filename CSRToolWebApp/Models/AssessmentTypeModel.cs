using CSRToolWebApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessmentType = CSRToolWebApp.DataContracts.Base.AssessmentType;

namespace CSRToolWebApp.Models
{
    public class AssessmentTypeModel
    {
        #region Properties
        public string TypeID { get; set; }
        public string TypeName { get; set; }
        public IRestService Service { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public AssessmentTypeModel()
        {
            Service = new RestService("casetypes");
        }
        /// <summary>
        /// Create an Instance of OfferModel from a business Entity OfferType
        /// </summary>
        /// <param name="offer">Business Entity OfferType</param>
        public AssessmentTypeModel(AssessmentType type)
        {
            TypeID = type.Id.ToString();
            TypeName = type.Name;
        }
        #endregion

        #region Internal Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<SelectListItem> GetAssessmentTypesList()
        {
            var types = new List<SelectListItem>();
            foreach (var type in GetCaseTypes(new RestService("assessmenttypes")))
            {
                var item = new SelectListItem()
                {
                    Text = type.TypeName,
                    Value = type.TypeID
                };
                types.Add(item);
            }
            return types;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal static IEnumerable<AssessmentTypeModel> GetCaseTypes(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<AssessmentType>>(json);
            var result = new List<AssessmentTypeModel>();
            foreach (AssessmentType type in dataContract)
            {
                var model = new AssessmentTypeModel(type);
                result.Add(model);
            }
            return result;
        }

        internal static AssessmentTypeModel GetCaseTypeByName(string name)
        {
            var types = AssessmentTypeModel.GetCaseTypes(new RestService("assessmenttypes"));
            return types.FirstOrDefault(x => x.TypeName == name);
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        #endregion
    }
}