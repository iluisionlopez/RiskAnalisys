using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSRTool.Core.Interfaces
{
    public interface IAssessmentSupplierReposity
    {

        AssessmentSupplier GetSingle(Guid id);
        IList<AssessmentSupplier> GetAll();
        IList<AssessmentSupplier> FindBy(Expression<Func<AssessmentSupplier, bool>> predicate);
        void Delete(AssessmentSupplier entity);
        void RemoveBy(Expression<Func<AssessmentSupplier, bool>> predicate);
        CSRToolNotifier Save(AssessmentSupplier entity);
        object SaveRange(IList<AssessmentSupplier> entities);
    }
}
