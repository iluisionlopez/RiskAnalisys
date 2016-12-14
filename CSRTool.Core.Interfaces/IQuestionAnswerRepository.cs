using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSRTool.Core.Interfaces
{
    public interface IQuestionAnswerRepository
    {
        List<QuestionAnswer> GetQuestionAnswers();

        QuestionAnswer FindBy(Expression<Func<QuestionAnswer, bool>> predicate);

        List<QuestionAnswer> GetQuestionAnswersByCustomerAssessment(Guid assessmentId);

        List<Core.QuestionAnswer> GetQuestionAnswersBySupplierAssessment(Guid assessmentId);



        bool SaveQuestionAnswer(QuestionAnswer coreQuestionAnswer);

        bool SaveAssessmentCustomerQuestionAnswer(AssessmentCustomerQuestionAnswer entity);

        bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<AssessmentCustomerQuestionAnswer> entities);

        bool DeleteCustomerQuestionAnswersByAssessmnetID(Guid assessmnetID);

        bool DeleteSupplierQuestionAnswersByAssessmnetID(Guid assessmnetID);

        bool SaveAssessmentSupplierQuestionAnswerList(IEnumerable<AssessmentSupplierQuestionAnswer> list);
    }
}
