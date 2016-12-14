using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAnswerService
    {
        List<Answer> GetAnswers();
        List<Answer> GetAnswersForQuestion(Guid questionId);
    }
}
