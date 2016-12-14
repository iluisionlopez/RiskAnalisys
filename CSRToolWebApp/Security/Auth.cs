using CSRTool.Core;

namespace CSRToolWebApp.Security
{
    public class Auth : IAuth
    {
        public User User
        {
            get
            {
                var ret = GetUserFromUsername("ahawpd");
                return ret;
            }
        }

        public User AdminUser {
            get
            {
                var ret = GetUserFromUsername("ahawpd");
                return ret;
            }
        }

        private User GetUserFromUsername(string username)
        {
            var ret = User.Create(username, "Anna","Häll Rivera", "anna.hall.rivera@gmail.com","0723879087",true);
            return ret;
        }

        public bool IsValidUser()
        {
            var ret = false;

            if (User != null)
                ret = true;
            return ret;
        }
    }
}