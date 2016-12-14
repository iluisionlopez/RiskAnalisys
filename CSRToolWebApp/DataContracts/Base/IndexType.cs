using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "IndexType", Namespace = "http://xmlns.scania.com/csrtool/schema/indextype/v1")]
    public class IndexType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int SortOrder { get; set; }

    }

    
}