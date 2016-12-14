using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IIndexTypeService
    {
        List<IndexType> GetIndexTypes();

        List<IndexType> GetIndexTypesByRiskCategory(Guid riskCategoryId);
    }
}