using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "AssessmentOfferType", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmentoffertype/v1")]
    public class AssessmentOfferType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }
        [DataMember(Order = 1)]
        public Guid AssessmentCustomerId { get; set; }
        [DataMember(Order = 2)]
        public Guid OfferTypeId { get; set; }

    }
}