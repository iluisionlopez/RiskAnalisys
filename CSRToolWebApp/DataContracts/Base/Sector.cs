using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "Sector", Namespace = "http://xmlns.scania.com/csrtool/schema/sector/v1")]
    public class Sector
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}