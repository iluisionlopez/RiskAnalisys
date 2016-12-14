using System;
using System.Runtime.Serialization;

namespace CSRToolWebApp.DataContracts.Base
{

    [DataContract(Name = "User", Namespace = "http://xmlns.scania.com/csrtool/schema/user/v1")]
    public class User
    {
        [DataMember(Order = 0)]
        public Guid Id { get; set; }

        [DataMember(Order = 1)]
        public string UserName { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public string Phone { get; set; }

        [DataMember(Order = 5)]
        public string Email { get; set; }
    }
}