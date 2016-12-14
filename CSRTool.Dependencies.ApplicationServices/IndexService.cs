using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class IndexService : IIndexService
    {
        private readonly IIndexRepository _indexRepository;

        public IndexService(IIndexRepository indexRepository)
        {
            _indexRepository = indexRepository;
        }

        public List<Index> GetIndexes()
        {
            return _indexRepository.GetIndexes();
        }

        public List<Index> GetIndexes(Guid territoryId, Guid versionId)
        {
            return _indexRepository.GetIndexes(territoryId,versionId);
        }

        public List<IndexType> GetIndexTypesByRiskCategory(Guid riskCategoryId)
        {
            throw new NotImplementedException();
        }

        public List<RiskCategory> GetRiskCategoriesByIndexType(Guid indexTypeId)
        {
            throw new NotImplementedException();
        }
    }

}