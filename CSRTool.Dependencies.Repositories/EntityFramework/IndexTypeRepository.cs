using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class IndexTypeRepository : IIndexTypeRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<IndexType> _indexType;

        public IndexTypeRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _indexType = _dbContext.Set<IndexType>();
        }
        
        public List<Core.IndexType> GetIndexTypes()
        {
            var ret = Mapper.Map<List<IndexType>, List<Core.IndexType>>(GetDbIndexType().OrderBy(x => x.SortOrder).ToList());
            return ret;
        }

        public List<Core.IndexType> GetIndexTypesByRiskCategory(Guid riskCategoryId)
        {
            var ret = Mapper.Map<List<IndexType>, List<Core.IndexType>>(GetDbIndexType(riskCategoryId).ToList());
            return ret;
        }

        public bool SaveIndexType(Core.IndexType indexType)
        {
            var dbIndexType = _indexType.FirstOrDefault(x => x.Id == indexType.Id);

            if (dbIndexType != null)
            {
                //update
                dbIndexType.Name       = indexType.Name;
                dbIndexType.SortOrder = indexType.SortOrder;
            }
            else
            {
                //create
                dbIndexType = _indexType.Create();

                dbIndexType.Id        = indexType.Id;
                dbIndexType.Name      = indexType.Name;
                dbIndexType.SortOrder = indexType.SortOrder;

                _indexType.Add(dbIndexType);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<IndexType> GetDbIndexType()
        {
            return _indexType;
        }

        //Get all IndexTypes for RiskCategory
        private IEnumerable<IndexType> GetDbIndexType(Guid riskCategoryId)
        {
            return _indexType.Where( it => it.RiskCategoryIndexType.Any(x=>x.RiskCategoryId==riskCategoryId));
        }


    }
}

