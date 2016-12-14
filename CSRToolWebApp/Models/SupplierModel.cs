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
    public class SupplierModel
    {
        #region Properties
        public String Id { get; set; }
        public string Name { get; set; }
        public int DUNS { get; set; }
        #endregion

        #region Constructors
        public SupplierModel()
        {

        }

        public SupplierModel(Supplier supplier)
        {
            Id = supplier.Id.ToString();
            Name = supplier.Name;
            DUNS = supplier.DUNS;
        }
        #endregion

        internal static IEnumerable<SupplierModel> GetSuppliers(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<List<Supplier>>(json);
            var suppliers = new List<SupplierModel>();
            foreach (var supplier in dataContract)
            {
                var item = new SupplierModel(supplier);
                suppliers.Add(item);
            }
            return suppliers;
        }

        internal static SupplierModel GetSupplier(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContract = JsonConvert.DeserializeObject<Supplier>(json);
            if (dataContract != null)
                return new SupplierModel(dataContract);
            else
                return null;
        }

        internal static SupplierModel Create(string name, int DUNS)
        {
            var service = new RestService("supplier");
            var coreSupplier = new Supplier();
            coreSupplier.Name = name;
            coreSupplier.DUNS = DUNS;
            var response = service.Post(JsonConvert.SerializeObject(coreSupplier));
            return new SupplierModel(JsonConvert.DeserializeObject<Supplier>(response));
        }
    }
}