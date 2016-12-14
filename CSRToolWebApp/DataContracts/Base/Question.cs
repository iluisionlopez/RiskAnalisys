using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "Question", Namespace = "http://xmlns.scania.com/csrtool/schema/question/v1")]
    public class Question
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string QuestionText { get; set; }

        [DataMember(Order = 3)]
        public string Comment { get; set; }

        [DataMember(Order = 4)]
        public int SortOrder { get; set; }

    }

}