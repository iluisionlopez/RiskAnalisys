using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class AssessmentTypeQuestionService : IAssessmentTypeQuestionService
    {
        private readonly IAssessmentTypeQuestionRepository _assessmentTypeQuestionRepository;

        public AssessmentTypeQuestionService(IAssessmentTypeQuestionRepository assessmentTypeQuestionRepository)
        {
            _assessmentTypeQuestionRepository = assessmentTypeQuestionRepository;
        }

        public List<AssessmentTypeQuestion> GetAssessmentTypeQuestions()
        {
            return _assessmentTypeQuestionRepository.GetAssessmentTypeQuestions();
        }

        public List<AssessmentTypeQuestion> GetQuestionsForAssessment(Guid assessmentTypeId)
        {
            return _assessmentTypeQuestionRepository.GetAssessmentTypeQuestions(assessmentTypeId);
        }
    }

}