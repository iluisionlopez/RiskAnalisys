using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IRiskCategoryRepository
    {
        List<RiskCategory> GetRiskCategories();
        List<RiskCategory> GetRiskCategoriesByIndexType(Guid indexTypeId);
        List<RiskCategory> GetRiskCategoriesByQuestion(Guid questionId);
    }
}