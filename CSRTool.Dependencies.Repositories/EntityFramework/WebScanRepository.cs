using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CSRTool.Core;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class WebScanRepository : IWebScanRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<AssessmentCustomerWebScan> _CustomerWebScan;
        private readonly DbSet<AssessmentSupplierWebScan> _SupplierWebScan;
        private readonly DbSet<WebScanType> _WebScanType;

        public WebScanRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _CustomerWebScan = _dbContext.Set<AssessmentCustomerWebScan>();
            _SupplierWebScan = _dbContext.Set<AssessmentSupplierWebScan>();
            _WebScanType = _dbContext.Set<WebScanType>();
        }
        
        public List<Core.WebScan> GetWebScans()
        {
            var ret = Mapper.Map<List<AssessmentCustomerWebScan>, List<Core.WebScan>>(GetDbWebScan().ToList());
            return ret;
        }

        public List<Core.WebScanType> GetWebScanTypes()
        {
            var ret = Mapper.Map<List<WebScanType>, List<Core.WebScanType>>(_WebScanType.ToList());
            return ret;
        }

        public List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var ret = Mapper.Map<List<AssessmentCustomerWebScan>, List<Core.WebScan>>(GetDbWebScanByAssessmentCustomerId(assessmentCustomerId).ToList());
            return ret;
        }


        public bool SaveCustomerWebScan(Core.WebScan scan)
        {
            var response = false;
            try
            {
                var dbWebScan = _CustomerWebScan.FirstOrDefault(x => x.AssessmentId == scan.AssessmentId && x.WebScanTypeId == scan.WebScanTypeId);

                if (dbWebScan != null)
                {//Update
                    dbWebScan.SearchString = scan.SearchString;
                    dbWebScan.Comment = scan.Comment;
                }
                else
                {//Cretae
                    dbWebScan = _CustomerWebScan.Create();
                    dbWebScan.Id = Guid.NewGuid();
                    dbWebScan.SearchString = scan.SearchString;
                    dbWebScan.AssessmentId = scan.AssessmentId;
                    dbWebScan.Comment = scan.Comment;
                    dbWebScan.WebScanType = _WebScanType.FirstOrDefault(t => t.Id == scan.WebScanTypeId);
                    dbWebScan.NegativeInfoFound = false;

                    _CustomerWebScan.Add(dbWebScan);
                }

                _dbContext.SaveChanges();
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return response;
        }

        public bool SaveSupplierWebScan(Core.WebScan scan)
        {
            var response = false;
            try
            {
                var dbWebScan = _SupplierWebScan.FirstOrDefault(x => x.AssessmentId == scan.AssessmentId && x.WebScanId == scan.WebScanTypeId);

                if (dbWebScan != null)
                {//Update
                    
                    dbWebScan.Comment = scan.Comment;
                }
                else
                {//Cretae
                    dbWebScan = _SupplierWebScan.Create();
                    dbWebScan.Id = Guid.NewGuid();
                    dbWebScan.AssessmentId = scan.AssessmentId;
                    dbWebScan.Comment = scan.Comment;
                    dbWebScan.WebScanType = _WebScanType.FirstOrDefault(t => t.Id == scan.WebScanTypeId);
                    dbWebScan.NegativeInfo = false;

                    _SupplierWebScan.Add(dbWebScan);
                }

                _dbContext.SaveChanges();
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return response;
        }

        private IEnumerable<AssessmentCustomerWebScan> GetDbWebScan()
        {
            return _CustomerWebScan;
        }


        private IEnumerable<AssessmentCustomerWebScan> GetDbWebScanByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            return _CustomerWebScan.Where(x=>x.AssessmentCustomer.Id == assessmentCustomerId);
        }

        private IEnumerable<AssessmentSupplierWebScan> GetDbWebScanByAssessmentSupplierId(Guid assessmentId)
        {
            return _SupplierWebScan.Where(x => x.AssessmentSupplier.Id == assessmentId);
        }

        public List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentId)
        {
            var ret = Mapper.Map<List<AssessmentSupplierWebScan>, List<Core.WebScan>>(GetDbWebScanByAssessmentSupplierId(assessmentId).ToList());
            return ret;
        }
    }
}

