using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAnswerRepository
    {
        List<Answer> GetAnswers(bool onlyActive = true);

        List<Answer> GetAnswersForQuestion(Guid questionId);

        bool SaveAnswer(Answer coreAnswer);

    }
}
