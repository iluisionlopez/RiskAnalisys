using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using WebScan = CSRTool.Core.WebScan;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class WebScanService : IWebScanService
    {
        private readonly IWebScanRepository _WebScanRepository;

        public WebScanService(IWebScanRepository WebScanRepository)
        {
            _WebScanRepository = WebScanRepository;
        }

        public List<WebScan> GetWebScans()
        {
            return _WebScanRepository.GetWebScans();
        }

        public List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            return _WebScanRepository.GetWebScansByAssessmentCustomerId(assessmentCustomerId);
        }

        public List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentId)
        {
            return _WebScanRepository.GetWebScansByAssessmentSupplierId(assessmentId);
        }

        public List<WebScanType> GetWebScanTypes()
        {
            return _WebScanRepository.GetWebScanTypes();
        }

        public bool SaveAssessmentCustomerWebScan(WebScan scan)
        {
            return _WebScanRepository.SaveCustomerWebScan(scan);
        }

        public bool SaveSupplierWebScan(WebScan scan)
        {
            return _WebScanRepository.SaveSupplierWebScan(scan);
        }
    }

}