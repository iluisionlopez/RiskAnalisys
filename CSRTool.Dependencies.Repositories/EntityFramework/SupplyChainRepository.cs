using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRTool.Core;
using System.Linq.Expressions;
using AutoMapper;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class SupplyChainRepository : ISupplyChainRespository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<SupplyChain> _repository;

        public SupplyChainRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _repository = _dbContext.Set<SupplyChain>();
        }

        public bool Delete(Core.AssessmentSupplier entity)
        {
            throw new NotImplementedException();
        }

        public IList<Core.SupplyChain> FindBy(Expression<Func<Core.SupplyChain, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IList<Core.SupplyChain> GetAll()
        {
            return Mapper.Map<List<Core.SupplyChain>>(_repository);
        }

        public Core.SupplyChain GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveBy(Expression<Func<Core.SupplyChain, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public object Save(Core.SupplyChain entity)
        {
            throw new NotImplementedException();
        }

        public void SaveRange(IList<Core.SupplyChain> entities)
        {
            throw new NotImplementedException();
        }
    }
}
