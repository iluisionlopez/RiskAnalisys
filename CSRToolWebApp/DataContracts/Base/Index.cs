using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "Index", Namespace = "http://xmlns.scania.com/csrtool/schema/index/v1")]
    public class Index
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public decimal Value { get; set; }

        [DataMember(Order = 2)]
        public string IndexTypeName { get; set; }

        [DataMember(Order = 3)]
        public string IndexTypeId { get; set; }

        [DataMember(Order = 4)]
        public string VersionId { get; set; }

    }

    [DataContract(Name = "SimpleIndex", Namespace = "http://xmlns.scania.com/csrtool/schema/simpleindex/v1")]
    public class SimpleIndex
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public int Value { get; set; }
    }
}