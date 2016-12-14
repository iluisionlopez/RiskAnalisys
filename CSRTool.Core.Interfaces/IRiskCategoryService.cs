using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IRiskCategoryService
    {
        List<RiskCategory> GetRiskCategories();
        List<RiskCategory> GetRiskCategoriesByIndexType(Guid indexTypeId);
        List<RiskCategory> GetRiskCategoriesForQuestion(Guid questionId);
    }
}