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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Supplier> _supplier;

        private IEnumerable<Supplier> GetDbCustomer()
        {
            return _supplier;
        }

        public SupplierRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _supplier = _dbContext.Set<Supplier>();
        }

        public List<Core.Supplier> GetSuppliers()
        {
            var ret = Mapper.Map<List<Supplier>, List<Core.Supplier>>(_supplier.ToList());
            return ret;
        }
        

        public Core.Supplier GetSupplier(Guid id)
        {
            var ret = Mapper.Map<Supplier, Core.Supplier>(_supplier.FirstOrDefault(x => x.Id == id));
            return ret;
        }

        public Core.Supplier GetSupplierByName(string customerName)
        {
            var ret = Mapper.Map<Supplier, Core.Supplier>(_supplier.FirstOrDefault(x => x.Name == customerName));
            return ret;
        }

        public CSRToolNotifier SaveSupplier(Core.Supplier entity)
        {
            var csrToolNotifier = new CSRToolNotifier();

            var dbSupplier = _dbContext.Set<Supplier>().FirstOrDefault(x => x.Id == entity.Id);
            try
            {
                if (dbSupplier != null)
                {//update
                    dbSupplier.Changed = DateTime.Now;
                    dbSupplier.ChangedBy = entity.ChangedBy;
                    dbSupplier.Name = entity.Name;
                    dbSupplier.DUNS = entity.DUNS;
                }
                else
                {//create
                    dbSupplier = _supplier.Create();
                    dbSupplier.Id = Guid.NewGuid();
                    dbSupplier.Name = entity.Name;
                    dbSupplier.DUNS = entity.DUNS;
                    dbSupplier.IsActive = true;
                    dbSupplier.Created = DateTime.Now;
                    dbSupplier.Changed = DateTime.Now;
                    dbSupplier.ChangedBy = Constants.CSRTool;
                    dbSupplier.CreatedBy = Constants.CSRTool;
                    _supplier.Add(dbSupplier);                
                }

                _dbContext.SaveChanges();

                csrToolNotifier.NotificationType = NotificationType.Success;
                csrToolNotifier.Message = dbSupplier.Id.ToString();
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