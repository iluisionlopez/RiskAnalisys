using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "OfferType", Namespace = "http://xmlns.scania.com/csrtool/schema/offertype/v1")]
    public class OfferType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Comment { get; set; }
    }
}