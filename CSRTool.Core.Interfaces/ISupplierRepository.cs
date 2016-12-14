using System;
using System.Collections.Generic;

namespace CSRTool.Core.Interfaces
{
    public interface ISupplierRepository
    {
        List<Supplier> GetSuppliers();
        Supplier GetSupplier(Guid id);
        Supplier GetSupplierByName(string name);
        CSRToolNotifier SaveSupplier(Supplier entity);
    }
}