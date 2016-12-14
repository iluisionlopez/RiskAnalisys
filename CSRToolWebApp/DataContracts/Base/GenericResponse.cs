using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{
    /// <summary>Return message
    /// 
    /// </summary>
    [DataContract(Name = "PutResponse", Namespace = "http://xmlns.scania.com/csrtool/schema/putresponse/v1")]
    public class GenericResponse
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public int ResponseStatus { get; set; }

        [DataMember(Order = 2)]
        public string ResponseMessage { get; set; }

        [DataMember(Order = 3)]
        public string ErrorMessage { get; set; }
    }
}