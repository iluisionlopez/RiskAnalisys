using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ISupplierService
    {
        List<Supplier> GetSuppliers();
        Supplier GetSupplier(Guid id);
        Supplier GetSupplierByName(string name);
        CSRToolNotifier SaveSupplier(Supplier entity);
    }
}