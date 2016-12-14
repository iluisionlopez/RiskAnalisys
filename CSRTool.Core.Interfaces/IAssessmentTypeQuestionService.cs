using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAssessmentTypeQuestionService
    {
        List<AssessmentTypeQuestion> GetAssessmentTypeQuestions();
        List<AssessmentTypeQuestion> GetQuestionsForAssessment(Guid assessmentTypeId);
    }
}
