using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public TransactionTypeService(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }

        public List<TransactionType> GetTransactionTypes()
        {
            return _transactionTypeRepository.GetTransactionTypes();
        }

        public List<TransactionType> GetTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            return _transactionTypeRepository.GetTransactionTypesByAssessmentCustomerId(assessmentCustomerId);
        }

        public CSRToolNotifier SaveAssessmentTransactionTypes(IEnumerable<AssessmentTransactionType> transactionstypes)
        {
            return _transactionTypeRepository.SaveAssessmentTransactionTypes(transactionstypes);
        }

        public bool DeleteAssessmentTransactionTypes(Guid assessmnetID)
        {
            return _transactionTypeRepository.DeleteAssessmentTransactionTypes(assessmnetID);
        }

    }

}