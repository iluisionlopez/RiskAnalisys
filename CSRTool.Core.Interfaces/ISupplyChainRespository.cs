using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core.Interfaces
{
    public interface ISupplyChainRespository 
    {
        SupplyChain GetSingle(Guid id);
        IList<SupplyChain> GetAll();
        IList<SupplyChain> FindBy(Expression<Func<SupplyChain, bool>> predicate);
        bool Delete(AssessmentSupplier entity);
        void RemoveBy(Expression<Func<SupplyChain, bool>> predicate);
        object Save(SupplyChain entity);
        void SaveRange(IList<SupplyChain> entities);
    }
}
