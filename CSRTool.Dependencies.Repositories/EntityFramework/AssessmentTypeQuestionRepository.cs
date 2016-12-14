using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class AssessmentTypeQuestionRepository : IAssessmentTypeQuestionRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<AssessmentTypeQuestion> _assessmentTypeQuestions;

        public AssessmentTypeQuestionRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _assessmentTypeQuestions = _dbContext.Set<AssessmentTypeQuestion>();
        }

        
        public List<Core.AssessmentTypeQuestion> GetAssessmentTypeQuestions()
        {
            var ret = Mapper.Map<List<AssessmentTypeQuestion>, List<Core.AssessmentTypeQuestion>>(GetDbAssessmentTypeQuestions().ToList());
            return ret;
        }

        public List<Core.AssessmentTypeQuestion> GetAssessmentTypeQuestions(Guid assessmentTypeId)
        {
            var ret = Mapper.Map<List<AssessmentTypeQuestion>, List<Core.AssessmentTypeQuestion>>(GetDbAssessmentTypeQuestions(assessmentTypeId).ToList());
            return ret;
        }


        public bool SaveAssessmentTypeQuestion(Core.AssessmentTypeQuestion assessmentTypeQuestion)
        {
            var dbAssessmentTypeQuestion = _assessmentTypeQuestions.FirstOrDefault(x => x.Id == assessmentTypeQuestion.Id);

            if (dbAssessmentTypeQuestion != null)
            {
                //update

                dbAssessmentTypeQuestion.AssessmentTypeId = assessmentTypeQuestion.AssessmentTypeId;
                dbAssessmentTypeQuestion.TransactionTypeId    = assessmentTypeQuestion.TransactionTypeId;
                dbAssessmentTypeQuestion.QuestionId  = assessmentTypeQuestion.QuestionId;
            }
            else
            {
                //create

                dbAssessmentTypeQuestion = _assessmentTypeQuestions.Create();

                dbAssessmentTypeQuestion.Id         = assessmentTypeQuestion.Id;
                dbAssessmentTypeQuestion.AssessmentTypeId = assessmentTypeQuestion.AssessmentTypeId;
                dbAssessmentTypeQuestion.TransactionTypeId = assessmentTypeQuestion.TransactionTypeId;
                dbAssessmentTypeQuestion.QuestionId = assessmentTypeQuestion.QuestionId;

                _assessmentTypeQuestions.Add(dbAssessmentTypeQuestion);
            }

            _dbContext.SaveChanges();

            return true;
        }


        private IEnumerable<AssessmentTypeQuestion> GetDbAssessmentTypeQuestions()
        {
            return _assessmentTypeQuestions;
        }

        private IEnumerable<AssessmentTypeQuestion> GetDbAssessmentTypeQuestions(Guid assessmentTypeId)
        {
            return _assessmentTypeQuestions.Where(x=>x.AssessmentTypeId == assessmentTypeId).ToList();
        }


    }
}

