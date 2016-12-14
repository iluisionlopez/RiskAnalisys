using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "AssessmentTypeQuestion", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmenttypequestion/v1")]
    public class AssessmentTypeQuestion
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public Guid QuestionId { get; set; }
        
        [DataMember(Order = 2)]
        public Guid TransactionTypeId { get; set; }

        [DataMember(Order = 3)]
        public Guid AssessmentTypeId { get; set; }
    }

}