using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IQuestionRepository
    {
        List<Question> GetQuestions(bool onlyActive = true);

        Question GetQuestion(Guid questionId);

        bool SaveQuestion(Question coreQuestion);

    }
}
