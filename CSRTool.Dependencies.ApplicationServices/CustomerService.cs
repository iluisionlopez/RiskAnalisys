using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerRepository"></param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(Guid id)
        {
            return _customerRepository.GetCustomer(id);
        }

        public Customer GetCustomerByName(string customerName)
        {
            return _customerRepository.GetCustomerByName(customerName);
        }

        public CSRToolNotifier SaveCustomer(Customer coreCustomer)
        {
            throw new NotImplementedException();
        }
    }

}