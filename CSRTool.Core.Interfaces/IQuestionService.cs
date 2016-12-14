using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IQuestionService
    {
        Question GetQuestion(Guid questionId);
        List<Question> GetQuestions();
    }
}
