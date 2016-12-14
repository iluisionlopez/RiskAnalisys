using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository questionRepository)
        {
            _answerRepository = questionRepository;
        }

        public List<Answer> GetAnswersForQuestion(Guid questionId)
        {
            return _answerRepository.GetAnswersForQuestion(questionId);
        }

        public List<Answer> GetAnswers()
        {
            return _answerRepository.GetAnswers();
        }


    }

}