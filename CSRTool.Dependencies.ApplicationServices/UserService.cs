using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }


    }

}