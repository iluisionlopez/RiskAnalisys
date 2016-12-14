using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TransactionType = CSRToolWebApp.DataContracts.Base.TransactionType;

namespace CSRToolWebApp.Models
{
    public class TransactionModel
    {
        #region Properties
        public string TransactionID { get; set; }
        public string TransactionName { get; set; }
        public string Comment { get; set; }
        public IRestService Service { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public TransactionModel()
        {
            Service = new RestService("transactiontypes");
        }
        /// <summary>
        /// Create an Instance of OfferModel from a business Entity OfferType
        /// </summary>
        /// <param name="offer">Business Entity OfferType</param>
        public TransactionModel(TransactionType type)
        {
            TransactionID = type.Id.ToString();
            TransactionName = type.Name;
            Comment = type.Comment;
        }
        #endregion

        #region Internal Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<CheckBoxListItem> GetCaseTypesList()
        {
            var types = new List<CheckBoxListItem>();
            foreach (var type in GetCaseTypes(new RestService("transactiontypes")))
            {
                var item = new CheckBoxListItem();
                item.ID = type.TransactionID;
                item.Name = type.TransactionName;
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
        internal static IEnumerable<TransactionModel> GetCaseTypes(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<TransactionType>>(json);
            var result = new List<TransactionModel>();
            foreach (TransactionType type in dataContract)
            {
                var model = new TransactionModel(type);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmenID"></param>
        /// <param name="selectedOffers"></param>
        internal static void SaveSelectedTransactionTypes(string assessmenID, IEnumerable<string> selectedTransactions)
        {
            var transactions = new List<AssessmentTransactionType>();
            //Create the new selected offertypes
            foreach (var transactionType in selectedTransactions)
            {
                var newType = new AssessmentTransactionType();
                newType.Id = Guid.NewGuid();
                newType.TransactionTypeId = Guid.Parse(transactionType);
                newType.AssessmentCustomerId = Guid.Parse(assessmenID);
                transactions.Add(newType);
            }
            var service = new RestService("assessmenttransactiontypes");
            var response = service.Post(JsonConvert.SerializeObject(transactions));
            var res = JsonConvert.DeserializeObject<GenericResponse>(response);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        #endregion
    }
}