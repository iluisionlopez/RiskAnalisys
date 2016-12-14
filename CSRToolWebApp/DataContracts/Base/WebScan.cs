using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "WebScan", Namespace = "http://xmlns.scania.com/csrtool/schema/webscan/v1")]
    public class WebScan
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public System.Guid AssessmentId { get; set; }

        [DataMember(Order = 2)]
        public System.Guid WebScanTypeId { get; set; }

        [DataMember(Order = 3)]
        public string Name { get; set; }

        [DataMember(Order = 4)]
        public string SearchString { get; set; }

        [DataMember(Order = 5)]
        public string Comment { get; set; }
    }
}