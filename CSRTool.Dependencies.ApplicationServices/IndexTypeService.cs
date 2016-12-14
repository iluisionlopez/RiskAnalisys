using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class IndexTypeService : IIndexTypeService
    {
        private readonly IIndexTypeRepository _indexTypeRepository;

        public IndexTypeService(IIndexTypeRepository indexTypeRepository)
        {
            _indexTypeRepository = indexTypeRepository;
        }

        public List<IndexType> GetIndexTypes()
        {
            return _indexTypeRepository.GetIndexTypes();
        }

        public List<IndexType> GetIndexTypesByRiskCategory(Guid riskCategoryId)
        {
            return _indexTypeRepository.GetIndexTypesByRiskCategory(riskCategoryId);
        }
    }

}