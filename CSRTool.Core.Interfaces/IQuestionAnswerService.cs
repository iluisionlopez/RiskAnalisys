using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSRTool.Core.Interfaces
{
    public interface IQuestionAnswerService
    {
        List<QuestionAnswer> GetQuestionAnswers();
        QuestionAnswer FindBy(Expression<Func<QuestionAnswer, bool>> predicate);
        List<QuestionAnswer> GetQuestionAnswersByCustomerAssessment(Guid assessmentId);
        List<QuestionAnswer> GetQuestionAnswersBySupplierAssessment(Guid assessmentId);
        bool SaveQuestionAnswer(QuestionAnswer coreQuestionAnswer);
        bool SaveAssessmentCustomerQuestionAnswer(AssessmentCustomerQuestionAnswer entity);
        bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<Core.AssessmentCustomerQuestionAnswer> entities);
        bool DeleteQuestionAnswersByAssessmnetID(Guid assessmnetID);
    }
}
