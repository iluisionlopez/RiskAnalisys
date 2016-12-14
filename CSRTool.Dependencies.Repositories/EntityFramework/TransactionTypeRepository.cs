using AutoMapper;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    /// <summary>
    /// Use to handle CRUD operation for TransactionType DB Entity
    /// </summary>
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TransactionType> _transactionTypes;
        private readonly DbSet<AssessmentTransactionType> _assessmentTransactionType;

        public TransactionTypeRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _transactionTypes = _dbContext.Set<TransactionType>();
            _assessmentTransactionType = _dbContext.Set<AssessmentTransactionType>();
        }

        
        public List<Core.TransactionType> GetTransactionTypes(bool onlyActive = true)
        {
            var ret = Mapper.Map<List<TransactionType>, List<Core.TransactionType>>(GetDbTransactionTypes(onlyActive).ToList());
            return ret;
        }

        public List<Core.TransactionType> GetTransactionTypes()
        {
            var ret = Mapper.Map<List<TransactionType>, List<Core.TransactionType>>(GetDbTransactionTypes().ToList());
            return ret;
        }

        public List<Core.TransactionType> GetTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var ret = Mapper.Map<List<TransactionType>, List<Core.TransactionType>>(GetDbTransactionTypesByAssessmentCustomerId(assessmentCustomerId).ToList());
            return ret;
        }

        /// <summary>
        /// Save/Update, should be completed after
        /// </summary>
        /// <param name="TransactionType"></param>
        /// <returns></returns>
        public bool SaveTransactionType(Core.TransactionType TransactionType)
        {
            var dbTransactionType = _transactionTypes.FirstOrDefault(x => x.Id == TransactionType.Id);

            if (dbTransactionType != null)
            {
                //update

                dbTransactionType.Changed = DateTime.Now;
                dbTransactionType.ChangedBy = TransactionType.ChangedBy;
                dbTransactionType.IsActive = TransactionType.IsActive;
                dbTransactionType.Name = TransactionType.Name;
            }
            else
            {
                //create

                dbTransactionType = _transactionTypes.Create();

                dbTransactionType.Id = TransactionType.Id;
                dbTransactionType.Created = DateTime.Now;
                dbTransactionType.CreatedBy = TransactionType.CreatedBy;
                dbTransactionType.Changed = TransactionType.Changed;
                dbTransactionType.ChangedBy = TransactionType.ChangedBy;
                dbTransactionType.IsActive = TransactionType.IsActive;
                dbTransactionType.Name = TransactionType.Name;

                _transactionTypes.Add(dbTransactionType);
            }

            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionstypes"></param>
        /// <returns></returns>
        public CSRToolNotifier SaveAssessmentTransactionTypes(IEnumerable<Core.AssessmentTransactionType> transactionstypes)
        {
            var csrToolNotifier = new CSRToolNotifier();
            try
            {
                var dbAssessmentOfferType = _assessmentTransactionType.Where(aot => aot.AssessmentCustomerId == transactionstypes.FirstOrDefault().AssessmentCustomerId).AsEnumerable();
                if (dbAssessmentOfferType == null)
                {//                   
                    var transactions = Mapper.Map<IEnumerable<Core.AssessmentTransactionType>, IEnumerable<AssessmentTransactionType>>(transactionstypes);
                    dbAssessmentOfferType = _assessmentTransactionType.AddRange(transactions);
                    csrToolNotifier.NotificationType = NotificationType.Success;
                    csrToolNotifier.Message = "All offerTypes has been added";
                }
                else
                {
                    if (DeleteAssessmentTransactionTypes(transactionstypes.FirstOrDefault().AssessmentCustomerId))
                    {

                        var offerts = Mapper.Map<IEnumerable<Core.AssessmentTransactionType>, IEnumerable<AssessmentTransactionType>>(transactionstypes);
                        dbAssessmentOfferType = _assessmentTransactionType.AddRange(offerts);
                        csrToolNotifier.NotificationType = NotificationType.Success;
                        csrToolNotifier.Message = "All offerTypes has been added";
                    }
                    else
                    {
                        csrToolNotifier.NotificationType = NotificationType.Warning;
                        csrToolNotifier.Message = "Operation couldn't be committed";
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                csrToolNotifier.NotificationType = NotificationType.Error;
                csrToolNotifier.Message = string.Concat("Message: ", e.Message, Environment.NewLine,
                                                        "Trace: InnerException", e.InnerException, Environment.NewLine,
                                                        "Trace: ", e.StackTrace);
            }
            return csrToolNotifier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmnetID"></param>
        /// <returns></returns>
        public bool DeleteAssessmentTransactionTypes(Guid assessmnetID)
        {
            var response = false;
            try
            {
                var dbAssessmentOfferType = _assessmentTransactionType.Where(x => x.AssessmentCustomerId == assessmnetID);
                if (dbAssessmentOfferType != null)
                {
                    _assessmentTransactionType.RemoveRange(dbAssessmentOfferType);
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

        private IEnumerable<TransactionType> GetDbTransactionTypes(bool onlyActive)
        {
            return onlyActive ? _transactionTypes.Where(i => i.IsActive) : _transactionTypes;
        }


        private IEnumerable<TransactionType> GetDbTransactionTypes()
        {
            return _transactionTypes;
        }

        private IEnumerable<TransactionType> GetDbTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            return _transactionTypes.Where(x=>x.AssessmentTransactionType.Any(y=>y.AssessmentCustomerId==assessmentCustomerId));
        }
    }
}

