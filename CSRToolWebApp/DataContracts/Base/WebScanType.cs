using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CSRToolWebApp.DataContracts.Base
{
    [DataContract(Name = "WebScanType", Namespace = "http://xmlns.scania.com/csrtool/schema/webscantype/v1")]
    public class WebScanType
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }      

        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}