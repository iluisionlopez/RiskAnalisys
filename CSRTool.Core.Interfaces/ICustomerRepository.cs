using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(Guid id);
        Customer GetCustomerByName(string customerName);
        CSRToolNotifier SaveCustomer(Customer coreCustomer);
    }
}