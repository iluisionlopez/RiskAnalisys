using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using CSRTool.Core;
using System.Linq.Expressions;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class QuestionAnswerRepository : IQuestionAnswerRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<QuestionAnswer> _questionAnswers;
        private readonly DbSet<AssessmentCustomerQuestionAnswer> _assessmentCustomerQuestionAnswers;
        private readonly DbSet<AssessmentSupplierQuestionAnswer> _assessmentSupplierQuestionAnswers;

        public QuestionAnswerRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _questionAnswers = _dbContext.Set<QuestionAnswer>();
            _assessmentCustomerQuestionAnswers = _dbContext.Set<AssessmentCustomerQuestionAnswer>();
            _assessmentSupplierQuestionAnswers = _dbContext.Set<AssessmentSupplierQuestionAnswer>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Core.QuestionAnswer> GetQuestionAnswers()
        {
            var ret = Mapper.Map<List<QuestionAnswer>, List<Core.QuestionAnswer>>(GetDbQuestionAnswers().ToList());
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<Core.QuestionAnswer> GetQuestionAnswers(Guid questionId)
        {
            var ret = Mapper.Map<List<QuestionAnswer>, List<Core.QuestionAnswer>>(GetDbQuestionAnswers(questionId).ToList());
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmentId"></param>
        /// <returns></returns>
        public List<Core.QuestionAnswer> GetQuestionAnswersByCustomerAssessment(Guid assessmentId)
        {
            var Ids = _assessmentCustomerQuestionAnswers.Where(x => x.AssessmentCustomerId == assessmentId).Select(q => q.QuestionAnswerId);
            var questionAnswers = _questionAnswers.Where(x => Ids.Contains(x.Id)).ToList();
            var ret = Mapper.Map<List<QuestionAnswer>, List<Core.QuestionAnswer>>(questionAnswers);
            return ret;
        }
        public List<Core.QuestionAnswer> GetQuestionAnswersBySupplierAssessment(Guid assessmentId)
        {
            var Ids = _assessmentSupplierQuestionAnswers.Where(x => x.AssessmentSupplierId == assessmentId).Select(q => q.QuestionAnswerId);
            var questionAnswers = _questionAnswers.Where(x => Ids.Contains(x.Id)).ToList();
            var ret = Mapper.Map<List<QuestionAnswer>, List<Core.QuestionAnswer>>(questionAnswers);
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Core.QuestionAnswer FindBy(Expression<Func<Core.QuestionAnswer, bool>> predicate)
        {
            var expression = Mapper.Map<Expression<Func<QuestionAnswer, bool>>>(predicate);
            var response = _questionAnswers.FirstOrDefault(expression);

            return Mapper.Map<QuestionAnswer, Core.QuestionAnswer>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionAnswer"></param>
        /// <returns></returns>
        public bool SaveQuestionAnswer(Core.QuestionAnswer questionAnswer)
        {
            var dbQuestionAnswer = _questionAnswers.FirstOrDefault(x => x.Id == questionAnswer.Id);

            if (dbQuestionAnswer != null)
            {
                //update

                dbQuestionAnswer.AnswerId = questionAnswer.AnswerId;
                dbQuestionAnswer.QuestionId = questionAnswer.QuestionId;
            }
            else
            {
                //create

                dbQuestionAnswer = _questionAnswers.Create();

                dbQuestionAnswer.Id = Guid.NewGuid();
                dbQuestionAnswer.AnswerId = questionAnswer.AnswerId;
                dbQuestionAnswer.QuestionId = questionAnswer.QuestionId;

                _questionAnswers.Add(dbQuestionAnswer);
            }

            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SaveAssessmentCustomerQuestionAnswer(Core.AssessmentCustomerQuestionAnswer entity)
        {
            var response = false;

            try
            {
                var dbAssessmentQuestionAnswer = _assessmentCustomerQuestionAnswers.FirstOrDefault(x => x.Id == entity.Id);

                if (dbAssessmentQuestionAnswer != null)
                {
                    //update

                    dbAssessmentQuestionAnswer.AssessmentCustomerId = entity.AssessmentCustomerId;
                    dbAssessmentQuestionAnswer.QuestionAnswerId = entity.QuestionAnswerId;
                }
                else
                {
                    //create

                    dbAssessmentQuestionAnswer = _assessmentCustomerQuestionAnswers.Create();

                    dbAssessmentQuestionAnswer.Id = Guid.NewGuid();
                    dbAssessmentQuestionAnswer.AssessmentCustomerId = entity.AssessmentCustomerId;
                    dbAssessmentQuestionAnswer.QuestionAnswerId = entity.QuestionAnswerId;

                    _assessmentCustomerQuestionAnswers.Add(dbAssessmentQuestionAnswer);
                }

                _dbContext.SaveChanges();

                response = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<Core.AssessmentCustomerQuestionAnswer> entities)
        {
            var response = false;
            try
            {
                if (DeleteCustomerQuestionAnswersByAssessmnetID(entities.FirstOrDefault().AssessmentCustomerId))
                {
                    var list = Mapper.Map<IEnumerable<Core.AssessmentCustomerQuestionAnswer>, IEnumerable<AssessmentCustomerQuestionAnswer>>(entities);
                    var dbAssessmentOfferType = _assessmentCustomerQuestionAnswers.AddRange(list);
                }
                _dbContext.SaveChanges();
                response = true;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmnetID"></param>
        /// <returns></returns>
        public bool DeleteCustomerQuestionAnswersByAssessmnetID(Guid assessmnetID)
        {
            var response = false;
            try
            {
                var dbAssessmentOfferType = _assessmentCustomerQuestionAnswers.Where(x => x.AssessmentCustomerId == assessmnetID);
                if (dbAssessmentOfferType != null)
                {
                    _assessmentCustomerQuestionAnswers.RemoveRange(dbAssessmentOfferType);
                    response = true;
                }
                else
                {
                    response = false;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return response;
        }

        public bool DeleteSupplierQuestionAnswersByAssessmnetID(Guid assessmnetID)
        {
            var response = false;
            try
            {
                var dbAssessmentQuestions = _assessmentSupplierQuestionAnswers.Where(x => x.AssessmentSupplierId == assessmnetID);
                if (dbAssessmentQuestions != null)
                {
                    _assessmentSupplierQuestionAnswers.RemoveRange(dbAssessmentQuestions);
                    response = true;
                }
                else
                {
                    response = false;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<QuestionAnswer> GetDbQuestionAnswers()
        {
            return _questionAnswers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        private IEnumerable<QuestionAnswer> GetDbQuestionAnswers(Guid questionId)
        {
            return _questionAnswers.Where(x => x.QuestionId == questionId).ToList();
        }

        public bool SaveAssessmentSupplierQuestionAnswerList(IEnumerable<Core.AssessmentSupplierQuestionAnswer> entities)
        {
            var response = false;
            try
            {
                if (DeleteSupplierQuestionAnswersByAssessmnetID(entities.FirstOrDefault().AssessmentSupplierId))
                {
                    var list = Mapper.Map<IEnumerable<Core.AssessmentSupplierQuestionAnswer>, IEnumerable<AssessmentSupplierQuestionAnswer>>(entities);
                    var dbAssessmentOfferType = _assessmentSupplierQuestionAnswers.AddRange(list);
                }
                _dbContext.SaveChanges();
                response = true;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }
    }
}

