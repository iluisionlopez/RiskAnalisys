using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class TerritoryRepository : ITerritoryRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Territory> _territories;

        public TerritoryRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _territories = _dbContext.Set<Territory>();
        }

        
        public List<Core.Territory> GetTerritories(bool onlyActive = true)
        {
            var ret = Mapper.Map<List<Territory>, List<Core.Territory>>(GetDbTerritories(onlyActive).ToList());
            return ret;
        }

        public bool SaveTerritory(Core.Territory territory)
        {
            var dbTerritory = _territories.FirstOrDefault(x => x.Id == territory.Id);

            if (dbTerritory != null)
            {
                //update

                dbTerritory.Changed = DateTime.Now;
                dbTerritory.ChangedBy = territory.ChangedBy;
                dbTerritory.IsActive = territory.IsActive;
                dbTerritory.Name = territory.Name;
                dbTerritory.TerritoryTypeId = territory.TerritoryTypeId;
            }
            else
            {
                //create

                dbTerritory = _territories.Create();

                dbTerritory.Id = territory.Id;
                dbTerritory.Created = DateTime.Now;
                dbTerritory.CreatedBy = territory.CreatedBy;
                dbTerritory.Changed = territory.Changed;
                dbTerritory.ChangedBy = territory.ChangedBy;
                dbTerritory.IsActive = territory.IsActive;
                dbTerritory.Name = territory.Name;
                dbTerritory.TerritoryTypeId = territory.TerritoryTypeId;

                _territories.Add(dbTerritory);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<Territory> GetDbTerritories(bool onlyActive)
        {
            return onlyActive ? _territories.Where(i => i.IsActive) : _territories;
        }
    }
}

