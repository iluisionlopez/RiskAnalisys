using System;
using AutoMapper;
using CSRTool.Core.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class RiskCategoryRepository : IRiskCategoryRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<RiskCategory> _riskCategory;

        public RiskCategoryRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _riskCategory = _dbContext.Set<RiskCategory>();
        }

        public List<Core.RiskCategory> GetRiskCategories()
        {
            var ret = Mapper.Map<List<RiskCategory>, List<Core.RiskCategory>>(GetDbRiskCategory().ToList());
            return ret;
        }

        public List<Core.RiskCategory> GetRiskCategoriesByIndexType(Guid indexTypeId)
        {
            var ret = Mapper.Map<List<RiskCategory>, List<Core.RiskCategory>>(GetDbRiskCategoryByIndex(indexTypeId).ToList());
            return ret;
        }

        public List<Core.RiskCategory> GetRiskCategoriesByQuestion(Guid questionId)
        {
            var ret = Mapper.Map<List<RiskCategory>, List<Core.RiskCategory>>(GetDbRiskCategoryByQuestion(questionId).ToList());
            return ret;
        }

        public bool SaveRiskCategory(Core.RiskCategory riskCategory)
        {
            var dbRiskCategory = _riskCategory.FirstOrDefault(x => x.Id == riskCategory.Id);

            if (dbRiskCategory != null)
            {
                //update

                dbRiskCategory.Name = riskCategory.Name;
              
            }
            else
            {
                //create

                dbRiskCategory = _riskCategory.Create();

                dbRiskCategory.Id          = riskCategory.Id;
                dbRiskCategory.Name       = riskCategory.Name;

                _riskCategory.Add(dbRiskCategory);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<RiskCategory> GetDbRiskCategory()
        {
            return _riskCategory;
        }

        private IEnumerable<RiskCategory> GetDbRiskCategoryByIndex(Guid indexTypeId)
        {
            return _riskCategory.Where(rc => rc.RiskCategoryIndexType.Any(x => x.IndexTypeId == indexTypeId));
        }

        private IEnumerable<RiskCategory> GetDbRiskCategoryByQuestion(Guid questionId)
        {
            return _riskCategory.Where(rc => rc.RiskCategoryQuestion.Any(x => x.QuestionId == questionId));
        }


    }
}

