using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Question> _questions;

        public QuestionRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _questions = _dbContext.Set<Question>();
        }
        
        public List<Core.Question> GetQuestions(bool onlyActive = true)
        {
            var ret = Mapper.Map<List<Question>, List<Core.Question>>(GetDbQuestion(onlyActive).OrderBy(x => x.SortOrder).ToList());
            return ret;
        }

        public Core.Question GetQuestion(Guid id)
        {
            Core.Question ret = null;

            var dbQuestion = _dbContext.Set<Question>().FirstOrDefault(x => x.Id == id);
            if (dbQuestion != null)
                ret = Mapper.Map<Core.Question>(dbQuestion);

            return ret;
        }



        public bool SaveQuestion(Core.Question question)
        {
            var dbQuestion = _questions.FirstOrDefault(x => x.Id == question.Id);

            if (dbQuestion != null)
            {
                //update

                dbQuestion.Name = question.Name;
                dbQuestion.QuestionText = question.QuestionText;
            }
            else
            {
                //create

                dbQuestion = _questions.Create();

                dbQuestion.Id          = question.Id;
                dbQuestion.Name       = question.Name;
                dbQuestion.QuestionText = question.QuestionText;


                _questions.Add(dbQuestion);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<Question> GetDbQuestion()
        {
            return _questions;
        }

        

        private IEnumerable<Question> GetDbQuestion(bool onlyActive)
        {
            return onlyActive ? _questions.Where(i => i.IsActive) : _questions;
        }


    }
}

