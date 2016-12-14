using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "RiskCategory", Namespace = "http://xmlns.scania.com/csrtool/schema/riskcategory/v1")]
    public class RiskCategory
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}