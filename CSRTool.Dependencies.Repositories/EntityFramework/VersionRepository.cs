using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class VersionRepository : IVersionRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Version> _version;

        public VersionRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _version = _dbContext.Set<Version>();
        }
        
        public List<Core.Version> GetVersions()
        {
            var ret = Mapper.Map<List<Version>, List<Core.Version>>(GetDbVersion().ToList());
            return ret;
        }

        public bool SaveVersion(Core.Version version)
        {
            var dbVersion = _version.FirstOrDefault(x => x.Id == version.Id);

            if (dbVersion != null)
            {
                //update

                dbVersion.Name = version.Name;
              
            }
            else
            {
                //create

                dbVersion = _version.Create();

                dbVersion.Id          = version.Id;
                dbVersion.Name       = version.Name;

                _version.Add(dbVersion);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<Version> GetDbVersion()
        {
            return _version;
        }


    }
}

