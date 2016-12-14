using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "Customer", Namespace = "http://xmlns.scania.com/csrtool/schema/customer/v1")]
    public class Customer
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}