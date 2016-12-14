using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class WebScanTypeRepository : IWebScanTypeRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<WebScanType> _WebScanType;

        public WebScanTypeRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _WebScanType = _dbContext.Set<WebScanType>();
        }
        
        public List<Core.WebScanType> GetWebScanTypes()
        {
            var ret = Mapper.Map<List<WebScanType>, List<Core.WebScanType>>(GetDbWebScanType().ToList());
            return ret;
        }

        public bool SaveWebScanType(Core.WebScanType WebScanType)
        {
            var dbWebScanType = _WebScanType.FirstOrDefault(x => x.Id == WebScanType.Id);

            if (dbWebScanType != null)
            {
                //update

                dbWebScanType.Name = WebScanType.Name;
              
            }
            else
            {
                //create

                dbWebScanType = _WebScanType.Create();

                dbWebScanType.Id          = WebScanType.Id;
                dbWebScanType.Name       = WebScanType.Name;

                _WebScanType.Add(dbWebScanType);
            }

            _dbContext.SaveChanges();

            return true;
        }

        private IEnumerable<WebScanType> GetDbWebScanType()
        {
            return _WebScanType;
        }


    }
}

