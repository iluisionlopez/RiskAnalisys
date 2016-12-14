using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class QuestionAnswerService : IQuestionAnswerService
    {
        private readonly IQuestionAnswerRepository _repository;

        public QuestionAnswerService(IQuestionAnswerRepository respository)
        {
            _repository = respository;
        }

        public bool DeleteQuestionAnswersByAssessmnetID(Guid assessmnetID)
        {
            return _repository.DeleteCustomerQuestionAnswersByAssessmnetID(assessmnetID);
        }

        public QuestionAnswer FindBy(Expression<Func<QuestionAnswer, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public List<QuestionAnswer> GetQuestionAnswers()
        {
            return _repository.GetQuestionAnswers();
        }

        public List<QuestionAnswer> GetQuestionAnswersByCustomerAssessment(Guid assessmentId)
        {
            return _repository.GetQuestionAnswersByCustomerAssessment(assessmentId);
        }

        public List<QuestionAnswer> GetQuestionAnswersBySupplierAssessment(Guid assessmentId)
        {
            return _repository.GetQuestionAnswersBySupplierAssessment(assessmentId);
        }

        public bool SaveAssessmentCustomerQuestionAnswer(AssessmentCustomerQuestionAnswer entity)
        {
            return _repository.SaveAssessmentCustomerQuestionAnswer(entity);
        }

        public bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<AssessmentCustomerQuestionAnswer> entities)
        {
            return _repository.SaveAssessmentCustomerQuestionAnswerList(entities);
        }

        public bool SaveQuestionAnswer(QuestionAnswer entity)
        {
            return _repository.SaveQuestionAnswer(entity);
        }
    }
}