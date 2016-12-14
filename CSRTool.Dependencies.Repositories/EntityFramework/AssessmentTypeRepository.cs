using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class AssessmentTypeRepository : IAssessmentTypeRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<AssessmentType> _assessmentTypes;

        public AssessmentTypeRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _assessmentTypes = _dbContext.Set<AssessmentType>();
        }

        
        public List<Core.AssessmentType> GetAssessmentTypes()
        {
            var ret = Mapper.Map<List<AssessmentType>, List<Core.AssessmentType>>(GetDbAssessmentTypes().ToList());
            return ret;
        }

        public bool SaveAssessmentType(Core.AssessmentType assessmentType)
        {
            var dbAssessmentType = _assessmentTypes.FirstOrDefault(x => x.Id == assessmentType.Id);

            if (dbAssessmentType != null)
            {
                //update

                dbAssessmentType.Name = assessmentType.Name;
                dbAssessmentType.Changed = assessmentType.Changed;
                dbAssessmentType.Changed = DateTime.Now;
                dbAssessmentType.ChangedBy = assessmentType.ChangedBy;
                dbAssessmentType.IsActive = assessmentType.IsActive;
 
            }
            else
            {
                //create

                dbAssessmentType = _assessmentTypes.Create();

                dbAssessmentType.Id         = assessmentType.Id;
                dbAssessmentType.Created = DateTime.Now;
                dbAssessmentType.CreatedBy = assessmentType.CreatedBy;
                dbAssessmentType.Changed = assessmentType.Changed;
                dbAssessmentType.ChangedBy = assessmentType.ChangedBy;
                dbAssessmentType.IsActive = assessmentType.IsActive;
                dbAssessmentType.Name = assessmentType.Name;

                _assessmentTypes.Add(dbAssessmentType);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<AssessmentType> GetDbAssessmentTypes()
        {
            return _assessmentTypes;
        }


    }
}

