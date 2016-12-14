using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "Version", Namespace = "http://xmlns.scania.com/csrtool/schema/version/v1")]
    public class Version
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}