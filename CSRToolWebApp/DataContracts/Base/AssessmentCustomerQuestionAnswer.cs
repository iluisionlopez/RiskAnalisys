using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "AssessmentCustomerQuestionAnswer", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmentcustomerquestionanswer/v1")]
    public class AssessmentCustomerQuestionAnswer
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }
        [DataMember(Order = 1)]
        public Guid AssessmentCustomerId { get; set; }
        [DataMember(Order = 2)]
        public Guid QuestionAnswerId { get; set; }
        [DataMember(Order = 3)]
        public string Comment { get; set; }
    }
}