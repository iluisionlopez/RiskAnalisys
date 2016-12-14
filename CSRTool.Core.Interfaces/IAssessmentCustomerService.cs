using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface IAssessmentCustomerService
    {
        List<AssessmentCustomer> GetAssessmentCustomers();
        List<AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId);
        AssessmentCustomer GetAssessmentCustomerById(Guid assessmentCustomerId);
        CSRToolNotifier SaveAssessmentCustomer(AssessmentCustomer assessmentCustomer);
    }
}
