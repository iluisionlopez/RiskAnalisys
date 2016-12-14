using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using CSRTool.Dependencies.ApplicationServices;

namespace CSRTool.Dependencies.ApplicationServices
{
    public class AssessmentSupplierService : IAssessmentSupplierService
    {
        private readonly IAssessmentSupplierReposity _repository;

        public AssessmentSupplierService(IAssessmentSupplierReposity repository)
        {
            _repository = repository;
        }

        public void Delete(AssessmentSupplier entity)
        {
            _repository.Delete(entity);
        }

        public IList<AssessmentSupplier> FindBy(Expression<Func<AssessmentSupplier, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public IList<AssessmentSupplier> GetAll()
        {
            return _repository.GetAll();
        }

        public AssessmentSupplier GetSingle(Guid id)
        {
            return _repository.GetSingle(id);
        }

        public void RemoveBy(Expression<Func<AssessmentSupplier, bool>> predicate)
        {
            _repository.RemoveBy(predicate);
        }

        public object Save(AssessmentSupplier entity)
        {
            return _repository.Save(entity);
        }

        public object SaveRange(IList<AssessmentSupplier> entities)
        {
            return _repository.SaveRange(entities);
        }
    }
}
