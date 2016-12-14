using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CSRTool.Core;
using CSRTool.Common;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Customer> _customer;

        private IEnumerable<Customer> GetDbCustomer()
        {
            return _customer;
        }

        public CustomerRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _customer = _dbContext.Set<Customer>();
        }

        public List<Core.Customer> GetCustomers()
        {
            var ret = Mapper.Map<List<Customer>, List<Core.Customer>>(GetDbCustomer().ToList());
            return ret;
        }

        public bool SaveVersion(Core.Customer customer)
        {
            var dbCustomer = _customer.FirstOrDefault(x => x.Id == customer.Id);

            if (dbCustomer != null)
            {
                //update

                dbCustomer.Name = customer.Name;

            }
            else
            {
                //create

                dbCustomer = _customer.Create();

                dbCustomer.Id = customer.Id;
                dbCustomer.Name = customer.Name;

                _customer.Add(dbCustomer);
            }

            _dbContext.SaveChanges();

            return true;
        }

        public Core.Customer GetCustomer(Guid id)
        {
            var ret = Mapper.Map<Customer, Core.Customer>(GetDbCustomer().FirstOrDefault(x => x.Id == id));
            return ret;
        }

        public Core.Customer GetCustomerByName(string customerName)
        {
            var ret = Mapper.Map<Customer, Core.Customer>(GetDbCustomer().FirstOrDefault(x => x.Name == customerName));
            return ret;
        }

        public CSRToolNotifier SaveCustomer(Core.Customer coreCustomer)
        {
            var csrToolNotifier = new CSRToolNotifier();

            var dbCustomer = _dbContext.Set<Customer>().FirstOrDefault(x => x.Id == coreCustomer.Id);
            try
            {
                if (dbCustomer != null)
                {//update
                    dbCustomer.Changed = DateTime.Now;
                    dbCustomer.ChangedBy = coreCustomer.ChangedBy;
                    dbCustomer.Name = coreCustomer.Name;
                }
                else
                {//create
                    dbCustomer = _customer.Create();
                    dbCustomer.Id = Guid.NewGuid();
                    dbCustomer.Name = coreCustomer.Name;
                    dbCustomer.IsActive = true;
                    dbCustomer.Created = DateTime.Now;
                    dbCustomer.Changed = DateTime.Now;
                    dbCustomer.ChangedBy = Constants.CSRTool;
                    dbCustomer.CreatedBy = Constants.CSRTool;
                    _customer.Add(dbCustomer);                
                }

                _dbContext.SaveChanges();

                csrToolNotifier.NotificationType = NotificationType.Success;
                csrToolNotifier.Message = dbCustomer.Id.ToString();
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
    }
}