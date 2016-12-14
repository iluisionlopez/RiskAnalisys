using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        CSRToolNotifier SaveFirstTimeUser(User coreUser);
    }
}