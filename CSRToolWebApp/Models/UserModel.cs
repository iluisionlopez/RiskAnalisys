using CSRToolWebApp.Common;
using CSRToolWebApp.DataContracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AutoMapper;

namespace CSRToolWebApp.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }       
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public List<string> Roles { get; set; }

        public string DisplayName
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public UserModel()
        {
            Roles = new List<string>();
        }

        public UserModel(User user)
        {
            Roles = new List<string>();
            UserName = user.UserName;
            Id = user.Id.ToString();
        }
        //TODO: Remove Default User
        internal UserModel GetInLoggedUser()
        {
            if (SessionHandler.LoggedInUser == null)
            {
                var user = new UserModel
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "ahawpd",
                    FirstName = "Anna",
                    LastName = "Häll Rivera",
                    Phone = "0723-879087"
                };
                SessionHandler.LoggedInUser = user;
            }

            return SessionHandler.LoggedInUser;
        }

        internal static IEnumerable<UserModel> GetUsers()
        {
            IRestService service = new RestService("users");
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContractUsers = JsonConvert.DeserializeObject<List<User>>(json);

            Mapper.CreateMap<User,UserModel>();
            var result = Mapper.Map<List<User>, List<UserModel>>(dataContractUsers);
            return result;
        }

        public static UserModel GetFirstUser()
        {
            return GetUsers().FirstOrDefault();
        }

        public static UserModel GetUserByName(string userName)
        {
            return GetUsers().FirstOrDefault(x => x.UserName == userName);
        }

        public static UserModel GetUserByID(string Id)
        {
            return GetUsers().FirstOrDefault(x => x.Id == Id);
        }

        public static GenericResponse SaveFirstTimeUser(UserModel user)
        {
            Mapper.CreateMap<UserModel, User>();
            var dataContractUser = Mapper.Map<User>(user);
            var restService = new RestService("user");
            var jsonUser = JsonConvert.SerializeObject(dataContractUser);
            var jsonResponse = restService.Post(jsonUser);
            return JsonConvert.DeserializeObject<GenericResponse>(jsonResponse);
           
        }
    }
}