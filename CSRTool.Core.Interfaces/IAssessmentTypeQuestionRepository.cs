using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAssessmentTypeQuestionRepository
    {
        List<AssessmentTypeQuestion> GetAssessmentTypeQuestions();

        bool SaveAssessmentTypeQuestion(AssessmentTypeQuestion assessmentTypeQuestion);

        List<AssessmentTypeQuestion> GetAssessmentTypeQuestions(Guid assessmentTypeId);
    }
}
