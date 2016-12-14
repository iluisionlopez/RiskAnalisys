using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "TransactionType", Namespace = "http://xmlns.scania.com/csrtool/schema/transactiontype/v1")]
    public class TransactionType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Comment { get; set; }
    }
}