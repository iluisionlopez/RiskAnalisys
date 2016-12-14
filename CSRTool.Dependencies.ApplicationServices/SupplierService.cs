using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerRepository"></param>
        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public Supplier GetSupplier(Guid id)
        {
            return _repository.GetSupplier(id);
        }

        public Supplier GetSupplierByName(string name)
        {
            return _repository.GetSupplierByName(name);
        }

        public List<Supplier> GetSuppliers()
        {
            return _repository.GetSuppliers();
        }

        public CSRToolNotifier SaveSupplier(Supplier entity)
        {
            return _repository.SaveSupplier(entity);
        }
    }

}