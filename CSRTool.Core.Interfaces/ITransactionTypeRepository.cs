using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ITransactionTypeRepository
    {
        List<TransactionType> GetTransactionTypes();        
        List<TransactionType> GetTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId);
        CSRToolNotifier SaveAssessmentTransactionTypes(IEnumerable<Core.AssessmentTransactionType> transactionstypes);
        bool DeleteAssessmentTransactionTypes(Guid assessmnetID);
    }
}
