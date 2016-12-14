using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "Answer", Namespace = "http://xmlns.scania.com/csrtool/schema/answer/v1")]
    public class Answer
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public int Value { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string AnswerText { get; set; }

        [DataMember(Order = 4)]
        public string Comment { get; set; }

        [DataMember(Order = 5)]
        public int SortOrder { get; set; }

    }

}