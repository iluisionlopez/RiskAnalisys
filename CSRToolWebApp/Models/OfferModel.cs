using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OfferType = CSRToolWebApp.DataContracts.Base.OfferType;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OfferModel
    {
        #region Properties
        public string OfferID { get; set; }
        public string OfferName { get; set; }
        public string Comment { get; set; }
        public IRestService Service { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public OfferModel()
        {
            Service = new RestService("offertypes");
        }
        /// <summary>
        /// Create an Instance of OfferModel from a business Entity OfferType
        /// </summary>
        /// <param name="offer">Business Entity OfferType</param>
        public OfferModel(OfferType offer)
        {
            OfferID = offer.Id.ToString();
            OfferName = offer.Name;
            Comment = offer.Comment;
        }
        #endregion

        #region Internal Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<CheckBoxListItem> GetOfferTypesList()
        {
            var types = new List<CheckBoxListItem>();
            foreach (var type in GetOfferTypes(new RestService("offertypes")))
            {
                var item = new CheckBoxListItem();
                item.ID = type.OfferID;
                item.Name = type.OfferName;
                item.Comment = type.Comment;
                types.Add(item);
            }
            return types;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal static IEnumerable<OfferModel> GetOfferTypes(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<OfferType>>(json);
            var result = new List<OfferModel>();
            foreach (OfferType offer in dataContract)
            {
                var model = new OfferModel(offer);
                result.Add(model);
            }
            return result;
        }

        internal static void SaveSelectedOfferTypes(string assessmenID, IEnumerable<string> selectedOffers)
        {
            var offers = new List<AssessmentOfferType>();
            //Create the new selected offertypes
            foreach (var offerType in selectedOffers)
            {
                var newType = new AssessmentOfferType();
                newType.Id = Guid.NewGuid();
                newType.OfferTypeId = Guid.Parse(offerType);
                newType.AssessmentCustomerId = Guid.Parse(assessmenID);
                offers.Add(newType);
            }
            var service = new RestService("assessmentoffertypes");
            var response = service.Post(JsonConvert.SerializeObject(offers));
            var res = JsonConvert.DeserializeObject<GenericResponse>(response);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        #endregion
    }
}