using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "AssessmentTransactionType", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmenttransactiontype/v1")]
    public class AssessmentTransactionType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }
        [DataMember(Order = 1)]
        public Guid AssessmentCustomerId { get; set; }
        [DataMember(Order = 2)]
        public Guid TransactionTypeId { get; set; }
    }
}