using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Answer> _answers;

        public AnswerRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _answers = _dbContext.Set<Answer>();
        }

        
        public List<Core.Answer> GetAnswers(bool onlyActive = true)
        {
            var ret = Mapper.Map<List<Answer>, List<Core.Answer>>(GetDbAnswers(onlyActive).OrderBy(x => x.SortOrder).ToList());
            return ret;
        }

        public List<Core.Answer> GetAnswersForQuestion(Guid questionId)
        {
            var ret = Mapper.Map<List<Answer>, List<Core.Answer>>(GetDbAnswers(questionId).OrderBy(x => x.SortOrder).ToList());
            return ret;
        }

        public bool SaveAnswer(Core.Answer answer)
        {
            var dbAnswer = _answers.FirstOrDefault(x => x.Id == answer.Id);

            if (dbAnswer != null)
            {
                //update

                dbAnswer.Changed    = DateTime.Now;
                dbAnswer.ChangedBy  = answer.ChangedBy;
                dbAnswer.IsActive   = answer.IsActive;
                dbAnswer.Name       = answer.Name;
                dbAnswer.AnswerText = answer.AnswerText;
            }
            else
            {
                //create

                dbAnswer = _answers.Create();

                dbAnswer.Id         = answer.Id;
                dbAnswer.Created    = DateTime.Now;
                dbAnswer.CreatedBy  = answer.CreatedBy;
                dbAnswer.Changed    = answer.Changed;
                dbAnswer.ChangedBy  = answer.ChangedBy;
                dbAnswer.IsActive   = answer.IsActive;
                dbAnswer.Name       = answer.Name;
                dbAnswer.AnswerText = answer.AnswerText;

                _answers.Add(dbAnswer);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<Answer> GetDbAnswers(bool onlyActive)
        {
            return onlyActive ? _answers.Where(i => i.IsActive) : _answers;
        }

        private IEnumerable<Answer> GetDbAnswers(Guid questionId)
        {
            var answer = _answers.Where( x => x.QuestionAnswer.Any(y=>y.QuestionId==questionId));
            return answer;
        }
    }
}

