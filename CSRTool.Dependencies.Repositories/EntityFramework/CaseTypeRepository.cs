using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class CaseTypeRepository : ICaseTypeRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<CaseType> _CaseType;

        public CaseTypeRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _CaseType = _dbContext.Set<CaseType>();
        }

        public List<Core.CaseType> GetCaseTypes()
        {
            var ret = Mapper.Map<List<CaseType>, List<Core.CaseType>>(GetDbCaseType().ToList());
            return ret;
        }

        public bool SaveCaseType(Core.CaseType CaseType)
        {
            var dbCaseType = _CaseType.FirstOrDefault(x => x.Id == CaseType.Id);

            if (dbCaseType != null)
            {
                //update

                dbCaseType.Name = CaseType.Name;

            }
            else
            {
                //create

                dbCaseType = _CaseType.Create();

                dbCaseType.Id = CaseType.Id;
                dbCaseType.Name = CaseType.Name;

                _CaseType.Add(dbCaseType);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<CaseType> GetDbCaseType()
        {
            return _CaseType;
        }


    }
}

