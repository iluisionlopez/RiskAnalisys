using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "AssessmentType", Namespace = "http://xmlns.scania.com/csrtool/schema/assessmenttype/v1")]
    public class AssessmentType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}