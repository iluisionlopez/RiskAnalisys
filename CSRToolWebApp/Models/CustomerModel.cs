using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerModel
    {
        #region Properties
        public String Id { get; set; }        
        public string Name { get; set; }
        #endregion

        #region Constructors
        public CustomerModel()
        {
                
        }

        public CustomerModel(Customer customer)
        {
            Id = customer.Id.ToString();
            Name = customer.Name;
        }
        #endregion

        internal static IEnumerable<CustomerModel> GetCustommers(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<Customer>>(json);
            var customers = new List<CustomerModel>();
            foreach(var cus in dataContract)
            {
                var item = new CustomerModel(cus);
                customers.Add(item);
            }            
            return customers;
        }

        internal static CustomerModel GetCustommer(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<Customer>(json);
            if (dataContract != null)
                return new CustomerModel(dataContract);
            else
                return null;
        }
    }
}