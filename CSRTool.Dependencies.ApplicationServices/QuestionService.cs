using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Question GetQuestion(Guid questionId)
        {
            return _questionRepository.GetQuestion(questionId);
        }

        public List<Question> GetQuestions()
        {
            return _questionRepository.GetQuestions();
        }


    }

}