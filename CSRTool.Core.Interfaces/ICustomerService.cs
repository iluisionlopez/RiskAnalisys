using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(Guid customerId);
        Customer GetCustomerByName(string customerName);
        CSRToolNotifier SaveCustomer(Customer coreCustomer);
    }
}