using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAssessmentCustomerRepository
    {
        List<AssessmentCustomer> GetAssessmentCustomers();
        List<AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId);
        AssessmentCustomer GetAssessmentCustomerById(Guid assessmentCustomerId);
        CSRToolNotifier SaveAssessmentCustomer(AssessmentCustomer assessmentCustomer);

        void Delete(Core.AssessmentCustomer entity);
    }
}
