using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "SupplyChain", Namespace = "http://xmlns.scania.com/csrtool/schema/supplychain/v1")]
    public class SupplyChain
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int Value { get; set; }

        [DataMember(Order = 3)]
        public int SortOrder { get; set; }
    }
}