using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class SectorRepository : ISectorRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<Sector> _sector;

        public SectorRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _sector = _dbContext.Set<Sector>();
        }
        
        public List<Core.Sector> GetSectors()
        {
            var ret = Mapper.Map<List<Sector>, List<Core.Sector>>(GetDbSector().ToList());
            return ret;
        }

        public bool SaveSector(Core.Sector sector)
        {
            var dbSector = _sector.FirstOrDefault(x => x.Id == sector.Id);

            if (dbSector != null)
            {
                //update

                dbSector.Name = sector.Name;
              
            }
            else
            {
                //create

                dbSector = _sector.Create();

                dbSector.Id          = sector.Id;
                dbSector.Name       = sector.Name;

                _sector.Add(dbSector);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<Sector> GetDbSector()
        {
            return _sector;
        }


    }
}

