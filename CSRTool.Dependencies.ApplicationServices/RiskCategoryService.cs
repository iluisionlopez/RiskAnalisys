using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using RiskCategory = CSRTool.Core.RiskCategory;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class RiskCategoryService : IRiskCategoryService
    {
        private readonly IRiskCategoryRepository _riskCategoryRepository;

        public RiskCategoryService(IRiskCategoryRepository riskCategoryRepository)
        {
            _riskCategoryRepository = riskCategoryRepository;
        }

        public List<RiskCategory> GetRiskCategories()
        {
            return _riskCategoryRepository.GetRiskCategories();
        }

        public List<RiskCategory> GetRiskCategoriesByIndexType(Guid indexTypeId)
        {
            return _riskCategoryRepository.GetRiskCategoriesByIndexType(indexTypeId);
        }

        public List<RiskCategory> GetRiskCategoriesForQuestion(Guid questionId)
        {
            return _riskCategoryRepository.GetRiskCategoriesByQuestion(questionId);
        }
    }

}