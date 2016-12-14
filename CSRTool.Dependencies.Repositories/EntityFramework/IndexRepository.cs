using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class IndexRepository : IIndexRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Index> _index;

        public IndexRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _index = _dbContext.Set<Index>();
        }
        
        public List<Core.Index> GetIndexes()
        {
            var ret = Mapper.Map<List<Index>, List<Core.Index>>(GetDbIndex().ToList());
            return ret;
        }

        public List<Core.Index> GetIndexesByIndexTypeId(Guid indexTypeId)
        {
            var ret = new List<Core.Index>();
            Mapper.CreateMap<Index, Core.Index>();
            Mapper.CreateMap<IndexType, Core.IndexType>();
            var indexList = _index.Where(x => x.IndexTypeId == indexTypeId).ToList();
            ret = Mapper.Map<List<Index>, List<Core.Index>>(indexList);
            return ret;
        }

        public List<Core.Index> GetIndexes(Guid territoryId, Guid versionId)
        {
            var ret = Mapper.Map<List<Index>, List<Core.Index>>(GetDbIndex(territoryId,versionId).ToList());
            return ret;
        }

        public bool SaveIndex(Core.Index index)
        {
            var dbIndex = _index.FirstOrDefault(x => x.Id == index.Id);

            if (dbIndex != null)
            {
                //update

                dbIndex.Value       = index.Value;
                dbIndex.IndexTypeId = index.IndexTypeId;
                dbIndex.VersionId   = index.VersionId;
                dbIndex.TerritoryId = index.TerritoryId;
            }
            else
            {
                //create

                dbIndex = _index.Create();

                dbIndex.Id          = index.Id;
                dbIndex.Value       = index.Value;
                dbIndex.IndexTypeId = index.IndexTypeId;
                dbIndex.VersionId   = index.VersionId;
                dbIndex.TerritoryId = index.TerritoryId;

                _index.Add(dbIndex);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<Index> GetDbIndex()
        {
            return _index;
        }


        private IEnumerable<Index> GetDbIndex(Guid territoryId, Guid versionId)
        {
            return _index.Where(x=> x.TerritoryId==territoryId && x.VersionId == versionId);
        }

    }
}

