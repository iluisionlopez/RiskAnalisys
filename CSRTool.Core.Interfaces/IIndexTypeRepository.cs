using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IIndexTypeRepository
    {
        List<IndexType> GetIndexTypes();
        List<IndexType> GetIndexTypesByRiskCategory(Guid riskCategoryId);
    }
}