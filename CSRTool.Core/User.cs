using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core
{
    public class User : ObjectInfo
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin { get; set; }
      


        public static User Create(string userName, string firstName, string lastName, string email, string phone, bool active)
        {
            var ret = new User();
            ret.Id = Guid.NewGuid();
            ret.UserName = userName;
            ret.FirstName = firstName;
            ret.LastName = lastName;
            ret.Email = email;
            ret.Phone = phone;
            ret.IsActive = active;
            
            return ret;
        }

        public bool Reminders { get; set; }

        
    }
}
