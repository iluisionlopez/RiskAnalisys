using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;

namespace CSRTool.Dependencies.ApplicationServices
{
    public class AssessmentCustomerService : IAssessmentCustomerService
    {
        private readonly IAssessmentCustomerRepository _assessmentCustomerRepository;

        public AssessmentCustomerService(IAssessmentCustomerRepository assessmentCustomerRepository)
        {
            _assessmentCustomerRepository = assessmentCustomerRepository;
        }

        public List<AssessmentCustomer> GetAssessmentCustomers()
        {
            return _assessmentCustomerRepository.GetAssessmentCustomers();
        }

        public List<AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId)
        {
            return _assessmentCustomerRepository.GetAssessmentCustomersForAssessor(userId);
        }

        public AssessmentCustomer GetAssessmentCustomerById(Guid assessmentCustomerId)
        {
            return _assessmentCustomerRepository.GetAssessmentCustomerById(assessmentCustomerId);
        }

        public CSRToolNotifier SaveAssessmentCustomer(AssessmentCustomer assessmentCustomer)
        {
            return _assessmentCustomerRepository.SaveAssessmentCustomer(assessmentCustomer);
        }
    }

}