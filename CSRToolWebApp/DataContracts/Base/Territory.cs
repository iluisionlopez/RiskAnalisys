using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "Territory", Namespace = "http://xmlns.scania.com/csrtool/schema/territory/v1")]
    public class Territory
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string ShortName { get; set; }

        [DataMember(Order = 3)]
        public bool IsActive { get; set; }

        [DataMember(Order = 4)]
        public Guid TerritoryTypeId { get; set; }
    }

    [DataContract(Name = "SimpleTerritory", Namespace = "http://xmlns.scania.com/csrtool/schema/simpleterritory/v1")]
    public class SimpleTerritory
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}