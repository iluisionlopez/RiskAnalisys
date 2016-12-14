using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "Supplier", Namespace = "http://xmlns.scania.com/csrtool/schema/supplier/v1")]
    public class Supplier
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int DUNS { get; set; }
    }
}