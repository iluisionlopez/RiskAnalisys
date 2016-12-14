using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CSRTool.Common;
using CSRTool.Core;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<User> _user;

        public UserRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _user = _dbContext.Set<User>();
        }
        
        public List<Core.User> GetUsers()
        {
            var ret = Mapper.Map<List<User>, List<Core.User>>(GetDbUser().ToList());
            return ret;
        }

        public CSRToolNotifier SaveFirstTimeUser(Core.User coreUser)
        {
            var csrToolNotifier = new CSRToolNotifier();

            var dbUser =
                _dbContext.Set<User>()
                    .FirstOrDefault(x => x.Id == coreUser.Id);

            if (dbUser != null)
            {
                // User exists - update

                dbUser.UserName     = coreUser.UserName;
                dbUser.LastName     = coreUser.LastName;
                dbUser.FirstName    = coreUser.FirstName;
                dbUser.Email        = coreUser.Email;
                dbUser.Phone        = coreUser.Phone;
                dbUser.Changed      = DateTime.Now;
                dbUser.ChangedBy    = coreUser.Id;
                dbUser.IsActive     = coreUser.IsActive;

            }
            else
            {
                // New user - create
                dbUser = _user.Create();
                
                //Set fields
                dbUser.Id           = Guid.NewGuid(); //Needs to be set here otherwise an empty guid
                dbUser.UserName     = coreUser.UserName;
                dbUser.LastName     = coreUser.LastName;
                dbUser.FirstName    = coreUser.FirstName;
                dbUser.Email        = coreUser.Email;
                dbUser.Phone        = coreUser.Phone;
                dbUser.Created      = DateTime.Now;
                dbUser.CreatedBy    = Constants.CSRTool;
                dbUser.Changed      = DateTime.Now;
                dbUser.ChangedBy    = Constants.CSRTool;
                dbUser.IsActive     = coreUser.IsActive;

                _user.Add(dbUser);
            }

            _dbContext.SaveChanges();

            csrToolNotifier.NotificationType = NotificationType.Success;

            return csrToolNotifier;
        }

        public bool SaveUser(Core.User user)
        {
            var dbUser = _user.FirstOrDefault(x => x.Id == user.Id);

            if (dbUser != null)
            {
                //update

                dbUser.UserName = user.UserName;
              
            }
            else
            {
                //create

                dbUser = _user.Create();

                dbUser.Id          = user.Id;
                dbUser.UserName = user.UserName;

                _user.Add(dbUser);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<User> GetDbUser()
        {
            return _user;
        }


    }
}

