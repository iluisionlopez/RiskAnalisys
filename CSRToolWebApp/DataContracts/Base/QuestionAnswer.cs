using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "QuestionAnswer", Namespace = "http://xmlns.scania.com/csrtool/schema/questionanswer/v1")]
    public class QuestionAnswer
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }
        [DataMember(Order = 1)]
        public Guid QuestionId { get; set; }
        [DataMember(Order = 2)]
        public Guid AnswerId { get; set; }
    }
}